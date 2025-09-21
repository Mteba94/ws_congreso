using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Ponentes.Commands.Update;

internal sealed class UpdatePonenteHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdatePonenteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(UpdatePonenteCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var validPontente = await _unitOfWork.Ponente
                .GetByIdAsync(command.PonenteId);

            if(validPontente == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            //var ponente = command.Adapt<Ponente>();

            //ponente.Id = command.PonenteId;
            //ponente.Estado = validPontente.Estado;
            //ponente.usuarioCreacion = validPontente.usuarioCreacion;
            //ponente.fechaCreacion = validPontente.fechaCreacion;
            var ponente = command.Adapt(validPontente);

            _unitOfWork.Ponente.Update(ponente);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var existeTags = await _unitOfWork.PonenteTag
                .GetTagPonentesByPonenteId(command.PonenteId);

            var newTags = command.PonenteTags
                .Where(p => p.Seleccionado && !existeTags.Any(ep => ep.TagId == p.TagId))
                .Select(p => new PonenteTag
                {
                    TagId = p.TagId,
                    PonenteId = ponente.Id,
                });

            await _unitOfWork.PonenteTag.RegistrarPonenteTags(newTags);

            var tagsEliminar = existeTags
                .Where(ep => !command.PonenteTags.Any(p => p.Seleccionado && p.TagId == ep.TagId))
                .ToList();

            await _unitOfWork.PonenteTag.EliminarPonenteTags(tagsEliminar);

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
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
