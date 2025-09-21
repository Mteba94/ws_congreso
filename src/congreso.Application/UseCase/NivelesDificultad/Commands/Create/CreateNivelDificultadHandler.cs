using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.NivelesDificultad.Commands.Create;

internal sealed class CreateNivelDificultadHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateNivelDificultadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(CreateNivelDificultadCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateNivelAsync(command, cancellationToken), cancellationToken);
    }

    public async Task<BaseResponse<bool>> CreateNivelAsync(CreateNivelDificultadCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var nivel = command.Adapt<NivelDificultad>();

            nivel.Estado = (int)TipoEstado.Activo;

            await _unitOfWork.NivelDificultad.CreateAsync(nivel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
