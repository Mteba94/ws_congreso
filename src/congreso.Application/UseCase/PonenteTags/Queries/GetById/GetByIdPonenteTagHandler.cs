using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.PonenteTags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.PonenteTags.Queries.GetById;

internal sealed class GetByIdPonenteTagHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdPonenteTagQuery, PonenteTagsByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<PonenteTagsByIdResponseDto>> Handle(GetByIdPonenteTagQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<PonenteTagsByIdResponseDto>();

        try
        {
            var ponenteTag = await _unitOfWork.PonenteTag.GetByIdAsync(query.PonenteTagsId);

            if (ponenteTag is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = ponenteTag.Adapt<PonenteTagsByIdResponseDto>();
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
