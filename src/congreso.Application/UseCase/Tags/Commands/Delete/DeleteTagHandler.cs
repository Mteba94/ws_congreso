using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Tags.Commands.Delete;

internal sealed class DeleteTagHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteTagCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(DeleteTagCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsTag = await _unitOfWork.Tag.GetByIdAsync(command.TagId);

            if (existsTag is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            existsTag.Estado = (int)TipoEstado.Inactivo;

            _unitOfWork.Tag.Update(existsTag);
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
