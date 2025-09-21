using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Ponentes.Commands.Delete;

internal sealed class DeletePonenteHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeletePonenteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(DeletePonenteCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var existsPonente = await _unitOfWork.Ponente.GetByIdAsync(command.PonenteId);

            if (existsPonente is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Ponente.DeleteAsync(command.PonenteId);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.PonenteTag.EliminarPonenteTagsByPonenteId(command.PonenteId);
            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
