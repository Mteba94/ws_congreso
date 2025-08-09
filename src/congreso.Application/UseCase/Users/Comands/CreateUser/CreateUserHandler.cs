using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Users.Comands.CreateUser;

internal sealed class CreateUserHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateUserAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = command.Adapt<User>();
            user.Password = BC.HashPassword(command.Password);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.Estado = (int)TipoEstado.Activo;
            user.fechaCreacion = DateTime.Now;
            user.usuarioCreacion = 1;

            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
