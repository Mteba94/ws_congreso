using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.TiposActividad.Queries.Select;

internal sealed class SelectTipoActividadHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<SelectTipoActividadQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(SelectTipoActividadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "SelectTipoActividad", "0", query);

            var tipoActividad = await _unitOfWork.TipoActividad.GetAllAsync();

            if(tipoActividad == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "SelectTipoActividad", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = tipoActividad.Adapt<IEnumerable<SelectResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "SelectTipoActividad", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;

            _fileLogger.Log("ws_congreso", "SelectTipoActividad", "1", response, ex.Message);
        }

        return response;
    }
}
