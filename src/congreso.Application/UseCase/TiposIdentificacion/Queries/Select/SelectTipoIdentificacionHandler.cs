using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.TiposIdentificacion.Queries.Select;

internal sealed class SelectTipoIdentificacionHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<SelectTipoIdentificacionQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(SelectTipoIdentificacionQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "SelectTipoIdentificacion", "0", query);

            var tipoIdentificacion = await _unitOfWork.TipoIdentificacion.GetAllAsync();

            if(tipoIdentificacion == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "SelectTipoIdentificacion", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = tipoIdentificacion.Adapt<IEnumerable<SelectResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "SelectTipoIdentificacion", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;

            _fileLogger.Log("ws_congreso", "SelectTipoIdentificacion", "1", response, ex.Message);
        }

        return response;
    }
}
