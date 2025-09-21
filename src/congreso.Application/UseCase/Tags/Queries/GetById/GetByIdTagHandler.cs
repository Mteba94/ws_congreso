using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Tags.Queries.GetById;

internal sealed class GetByIdTagHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdTagQuery, TagByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<TagByIdResponseDto>> Handle(GetByIdTagQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<TagByIdResponseDto>();

        try
        {
            var tag = await _unitOfWork.Tag.GetByIdAsync(query.TagId);

            if (tag is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = tag.Adapt<TagByIdResponseDto>();
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
