using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Ponentes.Queries.GetSelect;

internal sealed class GetSelectPonenteHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSelectPonenteQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetSelectPonenteQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var ponente = await _unitOfWork.Ponente
                .GetAllAsync();

            if (ponente is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = ponente.Adapt<IEnumerable<SelectResponseDto>>();
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
