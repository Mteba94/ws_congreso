using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TipoParticipante;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using logging.Service;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.TiposParticipante.Queries.GetAllTipoParticipante
{
    internal sealed class GetAllTipoParticipanteHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery, IFileLogger fileLogger) : IQueryHandler<GetAllTipoParticipanteQuery, IEnumerable<TipoParticipanteResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IOrderingQuery _orderingQuery = orderingQuery;
        private readonly IFileLogger _fileLogger = fileLogger;
        public async Task<BaseResponse<IEnumerable<TipoParticipanteResponseDTO>>> Handle(GetAllTipoParticipanteQuery query, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<TipoParticipanteResponseDTO>>();

            try
            {
                _fileLogger.Log("ws_congreso", "GetAllTipoParticipante", "0", JsonSerializer.Serialize(query));

                var tipoParticipante = _unitOfWork.TipoParticipante.GetAllQueryable();

                if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
                {
                    switch (query.NumFilter)
                    {
                        case 1:
                            //tiposIdentificacion = tiposIdentificacion.Where(t => t.Nombre.Contains(query.TextFilter));
                            break;
                        case 2:
                            //tiposIdentificacion = tiposIdentificacion.
                            break;
                    }
                }

                if (query.StateFilter is not null)
                {
                    var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                    //tiposIdentificacion = tiposIdentificacion.Where(t => stateFilter.Contains(t.Estado.ToString()));
                }
                query.Sort ??= "Id";
                var items = await _orderingQuery.Ordering(query, tipoParticipante).ToListAsync(cancellationToken);

                response.IsSuccess = true;
                response.TotalRecords = tipoParticipante.Count();
                response.Data = items.Adapt<IEnumerable<TipoParticipanteResponseDTO>>();
                response.Message = ReplyMessage.MESSAGE_QUERY;

                _fileLogger.Log("ws_congreso", "GetAllTipoParticipante", "1", JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                _fileLogger.Log("ws_congreso", "GetAllTipoParticipante", "1", JsonSerializer.Serialize(response), ex.Message);
            }

            return response;
        }
    }
}
