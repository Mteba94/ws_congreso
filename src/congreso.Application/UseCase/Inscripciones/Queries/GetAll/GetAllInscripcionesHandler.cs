using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Inscripciones;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Inscripciones.Queries.GetAll;

internal sealed class GetAllInscripcionesHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAllInscripcionesQuery, IEnumerable<InscripcionesResponseDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<InscripcionesResponseDTO>>> Handle(GetAllInscripcionesQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<InscripcionesResponseDTO>>();

        try
        {
            var inscripciones = await _unitOfWork.Inscripcion.GetAllAsync();

            if (inscripciones is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = inscripciones.Adapt<IEnumerable<InscripcionesResponseDTO>>();
            response.TotalRecords = inscripciones.Count();
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