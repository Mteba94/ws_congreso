using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.ExternalWS;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Inscripciones.Commands.GenerateDiploma;

internal sealed class GenerateDiplomaHandler(IUnitOfWork unitOfWork, IPdfGeneratorService pdfGeneratorService, ISendEmailAPI sendEmailAPI, HandlerExecutor executor) : ICommandHandler<GenerateDiplomaCommand, string>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPdfGeneratorService _pdfGeneratorService = pdfGeneratorService;
    private readonly ISendEmailAPI _sendEmailAPI = sendEmailAPI;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<string>> Handle(GenerateDiplomaCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => GenerateDiplomaAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<string>> GenerateDiplomaAsync(GenerateDiplomaCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            // 1. Retrieve Inscripcion (with User and Actividad)
            var inscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(command.InscripcionId);

            if (inscripcion is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Ensure User and Actividad are loaded
            if (inscripcion.User == null) // Assuming User is a navigation property that might not be eagerly loaded by GetByIdAsync
            {
                inscripcion.User = await _unitOfWork.User.GetByIdAsync(inscripcion.UserId);
            }
            if (inscripcion.Actividad == null) // Assuming Actividad is a navigation property
            {
                inscripcion.Actividad = await _unitOfWork.Actividad.GetByIdAsync(inscripcion.ActividadId);
            }

            if (inscripcion.User == null || inscripcion.Actividad == null)
            {
                response.IsSuccess = false;
                response.Message = "No se pudo obtener la información completa de la inscripción (usuario o actividad).";
                return response;
            }

            // 2. Validate attendance
            var hasAttendance = await _unitOfWork.Asistencia.HasAttendanceForInscripcion(command.InscripcionId);
            if (!hasAttendance)
            {
                response.IsSuccess = false;
                response.Message = "El participante no tiene registro de asistencia para esta actividad.";
                return response;
            }

            // 3. Check if a diploma already exists for this inscription
            var existingDiploma = (await _unitOfWork.Diploma.GetAllAsync()).FirstOrDefault(d => d.InscripcionId == command.InscripcionId);

            // 4. Generate Unique Code (if new diploma or regenerating)
            string uniqueCode = existingDiploma?.CodigoUnico ?? Guid.NewGuid().ToString();

            var nombreP = inscripcion.User.Pnombre + " " + inscripcion.User.Snombre + " " + inscripcion.User.Papellido + " " + inscripcion.User.Sapellido;

            // Determine the participant name for the diploma
            string finalParticipantName = string.IsNullOrWhiteSpace(command.NombrePersonalizado) ? nombreP : command.NombrePersonalizado;

            // 4. Prepare Data for PDF
            string activityTitle = inscripcion.Actividad.Titulo;
            DateTime issueDate = DateTime.UtcNow;

            // 5. Generate PDF
            var pdfResult = await _pdfGeneratorService.GenerateDiplomaPdfAsync(finalParticipantName, activityTitle, issueDate, uniqueCode, finalParticipantName);

            // 6. Create/Update Diploma Entity
            Diploma diplomaToUpdate;
            if (existingDiploma == null)
            {
                var newDiploma = new Diploma
                {
                    InscripcionId = command.InscripcionId,
                    ActividadId = inscripcion.ActividadId,
                    IdTipoDiploma = 1, // Assuming a default type for now
                    FechaEmision = issueDate,
                    CodigoUnico = uniqueCode,
                    NombrePersonalizado = finalParticipantName,
                    NombreArchivo = pdfResult.FilePath, // This is the local path, will be replaced by Azure URL
                    Estado = (int)TipoEstado.Activo // Assuming active by default
                };
                await _unitOfWork.Diploma.CreateAsync(newDiploma);
                diplomaToUpdate = newDiploma;
            }
            else
            {
                existingDiploma.NombrePersonalizado = finalParticipantName;
                existingDiploma.NombreArchivo = pdfResult.FilePath; // This is the local path, will be replaced by Azure URL
                existingDiploma.FechaEmision = issueDate; // Update emission date on regeneration
                _unitOfWork.Diploma.Update(existingDiploma);
                diplomaToUpdate = existingDiploma;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Upload PDF to Azure Blob Storage
            byte[] pdfBytes = Convert.FromBase64String(pdfResult.Base64Content);
            string azureFileName = $"diplomas/{Guid.NewGuid()}.pdf";
            string diplomaUrl = await _unitOfWork.azureStorage.SaveFileAsync("diplomas", azureFileName, pdfBytes, "application/pdf");

            // Update the Diploma entity with the Azure URL
            diplomaToUpdate.NombreArchivo = diplomaUrl;
            _unitOfWork.Diploma.Update(diplomaToUpdate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 7. Send Diploma via Email
            if (!string.IsNullOrWhiteSpace(inscripcion.User.Email))
            {
                var emailPayload = new
                {
                    plantilla = "diplomaEmailTemplate.html", // Assuming a template for diploma emails exists
                    //to = inscripcion.User.Email,
                    to = "tebalandonis@gmail.com",
                    subject = "Tu Diploma del Congreso",
                    body = new { _0 = finalParticipantName, _1 = activityTitle }, // Assuming placeholders _0 and _1
                    attachments = diplomaUrl // Send the Azure URL as attachment
                };

                try
                {
                    var emailResponse = await _sendEmailAPI.PostDataAsync<dynamic>("SendEmail", emailPayload);
                    // Optionally log emailResponse for debugging
                }
                catch (Exception emailEx)
                {
                    // Log email sending failure, but don't fail the diploma generation process
                    // Consider using a dedicated logger here if available
                }
            }

            response.IsSuccess = true;
            response.Data = pdfResult.Base64Content;
            response.Message = "Diploma generado y enviado por correo exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}