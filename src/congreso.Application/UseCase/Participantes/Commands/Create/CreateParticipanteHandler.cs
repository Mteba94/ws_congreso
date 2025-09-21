using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.ExternalWS;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Participantes.Commands.Create;

internal sealed class CreateParticipanteHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger, ISendEmailAPI sendEmailAPI) : ICommandHandler<CreateParticipanteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;
    private readonly ISendEmailAPI _sendEmailAPI = sendEmailAPI;
    public async Task<BaseResponse<bool>> Handle(CreateParticipanteCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateParticipanteAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateParticipanteAsync(CreateParticipanteCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            _fileLogger.Log("ws_congreso", "CreateUser", "0", command);

            var user = command.Adapt<User>();

            if (command.TipoIdentificacionId == 0)
            {
                user.TipoIdentificacionId = 1;
            }

            user.Password = BC.HashPassword(command.Password);
            user.EmailConfirmed = false;
            user.AccessFailedCount = 0;
            user.SecurityStamp = Guid.NewGuid().ToString();

            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            RoleUsuario userRol = new RoleUsuario();

            userRol.UserId = user.Id;
            userRol.RoleId = 3;
            userRol.Estado = 1;

            var userRole = userRol.Adapt<RoleUsuario>();

            await _unitOfWork.RoleUsuario.CreateAsync(userRole);
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
            response.Message = ex.Message;

            _fileLogger.Log("ws_congreso", "CreateUser", "1", response, ex.Message);
        }

        return response;
    }
}
