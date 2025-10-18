using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Actividades.Commands.Update;

internal sealed class UpdateActividadHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<UpdateActividadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(UpdateActividadCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => UpdateActividadAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> UpdateActividadAsync(UpdateActividadCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var actividad = await _unitOfWork.Actividad.GetByIdAsync(command.ActividadId);

            if (actividad == null)
            {
                response.IsSuccess = false;
                response.Message = "Actividad no encontrada.";
                return response;
            }

            // Update main properties
            actividad.Titulo = command.Titulo;
            actividad.Descripcion = command.Descripcion;
            actividad.DescripcionTotal = command.DescripcionTotal;
            actividad.TipoActividadId = command.TipoActividadId;
            actividad.FechaActividad = command.FechaActividad;
            actividad.HoraInicio = command.HoraInicio;
            actividad.HoraFin = command.HoraFin;
            actividad.CuposTotales = command.CuposTotales;
            actividad.Ubicacion = command.Ubicacion;
            actividad.RequisitosPrevios = command.RequisitosPrevios;
            actividad.NivelDificultadId = command.NivelDificultadId;
            actividad.Imagen = command.Imagen;
            actividad.Orden = command.Orden;
            actividad.permitirInscripcion = command.permitirInscripcion;
            actividad.Estado = command.Estado; // Allow updating status

            // Handle related entities

            // 1. Update ActividadPonentes (Many-to-Many)
            if (command.Ponentes != null)
            {
                var existingPonentes = (await _unitOfWork.ActividadPonente.GetAllAsync()).Where(ap => ap.ActividadId == actividad.Id).ToList();
                var existingPonenteIds = existingPonentes.Select(ap => ap.PonenteId).ToList();

                // Ponentes to remove
                var ponentesToRemove = existingPonentes.Where(ap => !command.Ponentes.Contains(ap.PonenteId)).ToList();
                foreach (var ap in ponentesToRemove)
                {
                    _unitOfWork.ActividadPonente.DeleteAsync(ap.Id);
                }

                // Ponentes to add
                var ponentesToAdd = command.Ponentes.Where(pId => !existingPonenteIds.Contains(pId)).ToList();
                foreach (var pId in ponentesToAdd)
                {
                    await _unitOfWork.ActividadPonente.CreateAsync(new Domain.Entities.ActividadPonente { ActividadId = actividad.Id, PonenteId = pId });
                }
            }

            // 2. Update MaterialesActividades (Many-to-Many, by description)
            if (command.Materiales != null)
            {
                var existingMateriales = (await _unitOfWork.MaterialActividad.GetAllAsync()).Where(ma => ma.ActividadId == actividad.Id).ToList();
                var existingMaterialDescriptions = existingMateriales.Select(ma => ma.MaterialDesc).ToList();

                // Materiales to remove
                var materialesToRemove = existingMateriales.Where(ma => !command.Materiales.Contains(ma.MaterialDesc)).ToList();
                foreach (var ma in materialesToRemove)
                {
                    _unitOfWork.MaterialActividad.DeleteAsync(ma.Id);
                }

                // Materiales to add
                var materialesToAdd = command.Materiales.Where(mDesc => !existingMaterialDescriptions.Contains(mDesc)).ToList();
                foreach (var mDesc in materialesToAdd)
                {
                    await _unitOfWork.MaterialActividad.CreateAsync(new Domain.Entities.MaterialActividad { ActividadId = actividad.Id, MaterialDesc = mDesc });
                }
            }

            // 3. Update ObjetivosActividades (One-to-Many, replace all)
            if (command.Objetivos != null)
            {
                // Remove all existing objectives
                var existingObjetivos = (await _unitOfWork.ObjetivoActividad.GetAllAsync()).Where(oa => oa.ActividadId == actividad.Id).ToList();
                foreach (var oa in existingObjetivos)
                {
                    _unitOfWork.ObjetivoActividad.DeleteAsync(oa.Id);
                }

                // Add new objectives
                foreach (var oDesc in command.Objetivos)
                {
                    await _unitOfWork.ObjetivoActividad.CreateAsync(new Domain.Entities.ObjetivoActividad { ActividadId = actividad.Id, ObjetivoDesc = oDesc });
                }
            }

            _unitOfWork.Actividad.Update(actividad);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Actividad actualizada exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}