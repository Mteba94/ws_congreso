using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Ponentes;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Ponentes.Queries.GetById;

internal sealed class GetByIdPonenteHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdPonenteQuery, PonenteByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<PonenteByIdResponseDto>> Handle(GetByIdPonenteQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<PonenteByIdResponseDto>();

        try
        {
            var ponente = await _unitOfWork.Ponente.GetByIdAsync(query.PonenteId);

            if (ponente is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = ponente.Adapt<PonenteByIdResponseDto>();
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
