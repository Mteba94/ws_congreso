using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Ponentes;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Ponentes.Queries.GetPopular;

internal sealed class GetPopularPonenteHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPopularPonenteQuery, IEnumerable<PonenteResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<PonenteResponseDto>>> Handle(GetPopularPonenteQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PonenteResponseDto>>();

        try
        {
            var ponentesPopular = await _unitOfWork.Ponente.GetPopularPonentes();

            if (ponentesPopular is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.IsSuccess = true;
            response.Data = ponentesPopular.Adapt<IEnumerable<PonenteResponseDto>>();
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
