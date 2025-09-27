using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.NivelesAcademicos;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.NivelesAcademicos.Queries.GetAll;

internal sealed class GetAllNivelAcademicoHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllNivelAcademicoQuery, IEnumerable<NivelAcademicoResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    public async Task<BaseResponse<IEnumerable<NivelAcademicoResponseDto>>> Handle(GetAllNivelAcademicoQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<NivelAcademicoResponseDto>>();

        try
        {
            var nivelAcademico = _unitOfWork.NivelAcademico
                .GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        //users = users.Where(u => u.Pnombre.Contains(query.TextFilter));
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                nivelAcademico = nivelAcademico.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, nivelAcademico)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await nivelAcademico.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<NivelAcademicoResponseDto>>();
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
