using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System.Text.Json;

namespace congreso.Application.UseCase.TiposIdentificacion.Queries.GetById;

internal sealed class GetByIdTipoIdentificacionHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetByIdTipoIdentificacionQuery, TipoIdentificacionByIdResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<TipoIdentificacionByIdResponseDTO>> Handle(GetByIdTipoIdentificacionQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<TipoIdentificacionByIdResponseDTO>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdTipoIdentificacion", "0", JsonSerializer.Serialize(query));

            var tipoIdentificacion = await _unitOfWork.TipoIdentificacion.GetByIdAsync(query.TipoIdentificacionId);

            if (tipoIdentificacion is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdTipoIdentificacion", "1", JsonSerializer.Serialize(response));

                return response;
            }

            response.IsSuccess = true;
            response.Data = tipoIdentificacion.Adapt<TipoIdentificacionByIdResponseDTO>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdTipoIdentificacion", "1", JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            _fileLogger.Log("ws_congreso", "GetByIdTipoIdentificacion", "1", JsonSerializer.Serialize(response), ex.Message);
        }

        return response;
    }
}
