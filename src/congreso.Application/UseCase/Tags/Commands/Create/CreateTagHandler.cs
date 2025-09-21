using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Tags.Commands.Create;

internal sealed class CreateTagHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateTagCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(CreateTagCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateTagAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateTagAsync(CreateTagCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var tag = command.Adapt<Tag>();

            tag.Estado = (int)TipoEstado.Activo;

            await _unitOfWork.Tag.CreateAsync(tag);
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
