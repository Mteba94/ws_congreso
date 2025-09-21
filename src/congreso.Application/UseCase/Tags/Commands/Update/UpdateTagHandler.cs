using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Tags.Commands.Update;

internal sealed class UpdateTagHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<UpdateTagCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdateTagAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdateTagAsync(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var validTag = await _unitOfWork.Tag.GetByIdAsync(command.TagId);

            if (validTag is not null)
            {
                command.Adapt(validTag);

                _unitOfWork.Tag.Update(validTag);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
