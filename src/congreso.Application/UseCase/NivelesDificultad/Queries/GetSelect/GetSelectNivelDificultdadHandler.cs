using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.NivelesDificultad.Queries.GetSelect;

internal sealed class GetSelectNivelDificultdadHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSelectNivelDificultdadQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetSelectNivelDificultdadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var niveles = await _unitOfWork.NivelDificultad
                .GetAllAsync();

            if (niveles is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = niveles.Adapt<IEnumerable<SelectResponseDto>>();
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
