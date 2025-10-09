using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Actividades;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Actividades.Queries.GetById;

internal sealed class GetByIdActividadHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdActividadQuery, ActividadByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<ActividadByIdResponseDto>> Handle(GetByIdActividadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ActividadByIdResponseDto>();

        try
        {
            var actividad = await _unitOfWork.Actividad.GetByIdAsync(query.ActividadId);

            if (actividad is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = actividad.Adapt<ActividadByIdResponseDto>();
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
