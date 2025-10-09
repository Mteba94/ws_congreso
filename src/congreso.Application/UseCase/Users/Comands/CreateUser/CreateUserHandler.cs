using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.ExternalWS;
using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.UserRoles.Commands.Create;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Users.Comands.CreateUser;

internal sealed class CreateUserHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger, ISendEmailAPI sendEmailAPI) : ICommandHandler<CreateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;
    private readonly ISendEmailAPI _sendEmailAPI = sendEmailAPI;

    public async Task<BaseResponse<bool>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateUserAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            _fileLogger.Log("ws_congreso", "CreateUser", "0", command);

            var user = command.Adapt<User>();

            if(command.TipoIdentificacionId == 0)
            {
                user.TipoIdentificacionId = 1;
            }

            user.Password = BC.HashPassword(command.Password);
            user.EmailConfirmed = false;
            user.AccessFailedCount = 0;
            user.SecurityStamp = Guid.NewGuid().ToString();

            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var roleUser = new RoleUsuario
            {
                RoleId = 3,
                UserId = user.Id,
                Estado = (int)TipoEstado.Activo
            };

            await _unitOfWork.RoleUsuario.CreateAsync(roleUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Commit();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;

            _fileLogger.Log("ws_congreso", "CreateUser", "1", response);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "CreateUser", "1", response, ex.Message);
        }

        return response;
    }
}
