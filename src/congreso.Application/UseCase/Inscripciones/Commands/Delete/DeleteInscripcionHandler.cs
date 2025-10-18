using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Inscripciones.Commands.Delete;

internal sealed class DeleteInscripcionHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteInscripcionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(DeleteInscripcionCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsInscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(command.InscripcionId);

            if (existsInscripcion is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            existsInscripcion.Estado = (int)TipoEstado.Inactivo;

            _unitOfWork.Inscripcion.Update(existsInscripcion);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
