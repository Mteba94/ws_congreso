using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Participantes.Queries.GetSelect
{
    internal sealed class GetSelectParticipanteHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSelectParticipanteQuery, IEnumerable<SelectResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetSelectParticipanteQuery query, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

            try
            {
                var users = await _unitOfWork.User
                    .GetAllAsync();

                if (users is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var userNormal = users.Where(u => u.TipoParticipanteId != null);

                response.IsSuccess = true;
                response.Data = userNormal.Adapt<IEnumerable<SelectResponseDto>>();
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
