using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Tags.Queries.GetSelect;

internal sealed class GetSelectTagHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSelectTagQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetSelectTagQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var tags = await _unitOfWork.Tag
                .GetAllAsync();

            if (tags is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = tags.Adapt<IEnumerable<SelectResponseDto>>();
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
