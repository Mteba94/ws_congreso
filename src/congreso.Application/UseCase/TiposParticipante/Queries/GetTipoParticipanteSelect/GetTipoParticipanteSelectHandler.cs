using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System.Text.Json;

namespace congreso.Application.UseCase.TiposParticipante.Queries.GetTipoParticipanteSelect;

internal sealed class GetTipoParticipanteSelectHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetTipoParticipanteSelectQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetTipoParticipanteSelectQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetTipoParticipanteSelectHandler", "0", query);

            var tipoParticipante = await _unitOfWork.TipoParticipante.GetAllAsync();

            if (tipoParticipante is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetTipoParticipanteSelectHandler", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = tipoParticipante.Adapt<IEnumerable<SelectResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetTipoParticipanteSelectHandler", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;

            _fileLogger.Log("ws_congreso", "GetTipoParticipanteSelectHandler", "1", response, ex.Message);
        }

        return response;
    }

}
