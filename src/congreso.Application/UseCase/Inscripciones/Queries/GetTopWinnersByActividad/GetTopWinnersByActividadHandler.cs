using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Inscripciones;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Inscripciones.Queries.GetTopWinnersByActividad;

internal sealed class GetTopWinnersByActividadHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTopWinnersByActividadQuery, IEnumerable<InscripcionesByUserDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<InscripcionesByUserDTO>>> Handle(GetTopWinnersByActividadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<InscripcionesByUserDTO>>();

        try
        {
            var inscripciones = await _unitOfWork.Inscripcion.GetInscripcionesByActividadId(query.ActividadId);

            if (inscripciones is null || !inscripciones.Any())
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var topWinners = inscripciones
                .Where(i => i.EsGanador == true && i.Puntaje.HasValue)
                .OrderByDescending(i => i.Puntaje)
                .Take(query.TopN)
                .Adapt<IEnumerable<InscripcionesByUserDTO>>();

            response.IsSuccess = true;
            response.Data = topWinners;
            response.TotalRecords = topWinners.Count();
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}