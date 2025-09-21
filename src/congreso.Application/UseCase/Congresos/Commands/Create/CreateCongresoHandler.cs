using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace congreso.Application.UseCase.Congresos.Commands.Create;

internal sealed class CreateCongresoHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<CreateCongresoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(CreateCongresoCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateCongresoAsync(command, cancellationToken), cancellationToken);
    }

    public async Task<BaseResponse<bool>> CreateCongresoAsync(CreateCongresoCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "CreateCongreso", "0", command);

            var congreso = command.Adapt<Congreso>();

            await _unitOfWork.Congreso.CreateAsync(congreso);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;

            _fileLogger.Log("ws_congreso", "CreateCongreso", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            _fileLogger.Log("ws_congreso", "CreateCongreso", "1", response, ex.Message);
        }

        return response;
    }
    
}
