using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.NivelesDificultad.Queries.GetById;

internal sealed class GetByIdNivelDificultadHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetByIdNivelDificultadQuery, NivelDificultdadByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<NivelDificultdadByIdResponseDto>> Handle(GetByIdNivelDificultadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<NivelDificultdadByIdResponseDto>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdNivelDificultad", "0", query);

            var nivel = await _unitOfWork.NivelDificultad.GetByIdAsync(query.NivelId);

            if (nivel is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdNivelDificultad", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = nivel.Adapt<NivelDificultdadByIdResponseDto>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdNivelDificultad", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetByIdNivelDificultad", "1", response, ex.Message);
        }

        return response;
    }
}
