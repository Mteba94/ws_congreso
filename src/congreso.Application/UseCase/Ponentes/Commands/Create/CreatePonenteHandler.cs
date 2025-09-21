using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Ponentes.Commands.Create
{
    internal sealed class CreatePonenteHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreatePonenteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<BaseResponse<bool>> Handle(CreatePonenteCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var ponente = command.Adapt<Ponente>();
                await _unitOfWork.Ponente.CreateAsync(ponente);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var ponenteTag = command.PonenteTags
                    .Select(ponenteTags => new PonenteTag
                    {
                        TagId = ponenteTags.TagId,
                        PonenteId = ponente.Id
                    })
                    .ToList();

                await _unitOfWork.PonenteTag.RegistrarPonenteTags(ponenteTag);

                transaction.Commit();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}
