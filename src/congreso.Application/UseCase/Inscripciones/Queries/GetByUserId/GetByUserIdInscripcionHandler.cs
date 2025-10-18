using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Inscripciones;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Inscripciones.Queries.GetByUserId;

internal sealed class GetByUserIdInscripcionHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByUserIdInscripcionQuery, IEnumerable<InscripcionesByUserDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<InscripcionesByUserDTO>>> Handle(GetByUserIdInscripcionQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<InscripcionesByUserDTO>>();

        try
        {
            var inscripciones = await _unitOfWork.Inscripcion.IncsripcionesByUserId(query.UserId);

            var inscripcionesList = inscripciones.ToList();

            if (inscripciones is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = inscripciones.Adapt<IEnumerable<InscripcionesByUserDTO>>();
            response.TotalRecords = inscripcionesList.Count;
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
