using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Dtos.TipoParticipante;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.TiposParticipante.Queries.GetById;

internal sealed class GetByIdTipoParticipanteHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetByIdTipoParticipanteQuery, TipoParticipanteByIdResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<TipoParticipanteByIdResponseDTO>> Handle(GetByIdTipoParticipanteQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<TipoParticipanteByIdResponseDTO>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdTipoParticipante", "0", query);

            var tipoParticipante = await _unitOfWork.TipoParticipante.GetByIdAsync(query.tipoParticipanteId);

            if (tipoParticipante == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdTipoParticipante", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = tipoParticipante.Adapt<TipoParticipanteByIdResponseDTO>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdTipoParticipante", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetByIdTipoParticipante", "1", response, ex.Message);
        }

        return response;
    }
}
