using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Congresos.Commands.Update;

internal sealed class UpdateCongresoHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<UpdateCongresoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(UpdateCongresoCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdateCongresoAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdateCongresoAsync(UpdateCongresoCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "UpdateCongreso", "0", command);

            var validCongreso = await _unitOfWork.Congreso.GetByIdAsync(command.congresoId);

            if (validCongreso is not null)
            {
                command.Adapt(validCongreso);

                _unitOfWork.Congreso.Update(validCongreso);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

                _fileLogger.Log("ws_congreso", "UpdateCongreso", "1", response);

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            _fileLogger.Log("ws_congreso", "UpdateCongreso", "1", response);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "UpdateCongreso", "1", response, ex.Message);
        }

        return response;
    }
}
