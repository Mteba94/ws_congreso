using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Congreso;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.Congresos.Queries.GetById;

internal sealed class GetCongresoByIdHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetCongresoByIdQuery, CongresoByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<CongresoByIdResponseDto>> Handle(GetCongresoByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CongresoByIdResponseDto>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdCongreso", "0", query);

            var congreso = await _unitOfWork.Congreso.GetByIdAsync(query.CongresoId);

            if (congreso is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdCongreso", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = congreso.Adapt<CongresoByIdResponseDto>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdCongreso", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetByIdCongreso", "1", response, ex.Message);
        }

        return response;
    }
}
