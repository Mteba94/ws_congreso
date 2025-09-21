using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.PonenteTags;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.PonenteTags.Queries.GetByIdPonente;

internal class GetByIdPonentePonenteTagHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdPonentePonenteTagQuery, IEnumerable<PonenteTagsByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<PonenteTagsByIdResponseDto>>> Handle(GetByIdPonentePonenteTagQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PonenteTagsByIdResponseDto>>();

        try
        {
            var ponenteTag = await _unitOfWork.PonenteTag.GetTagPonentesByPonenteId(query.PonenteId);

            if (ponenteTag is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = ponenteTag.Adapt<IEnumerable<PonenteTagsByIdResponseDto>>();
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
