using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Participantes;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.Participantes.Queries.GetById;

internal sealed class GetByIdParticipanteHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetByIdParticipanteQuery, ParticipanteByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<ParticipanteByIdResponseDto>> Handle(GetByIdParticipanteQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ParticipanteByIdResponseDto>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdParticipante", "0", query);

            var user = await _unitOfWork.User.GetByIdAsync(query.ParticipanteId);

            if (user is null || user.TipoParticipanteId == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdParticipante", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = user.Adapt<ParticipanteByIdResponseDto>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdParticipante", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetByIdParticipante", "1", response, ex.Message);
        }

        return response;
    }
}
