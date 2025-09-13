using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Authentication;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Users.Queries.LoginAdmin;

internal sealed class LoginAdminHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, IFileLogger fileLogger) : IQueryHandler<LoginAdminQuery, string>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<string>> Handle(LoginAdminQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            _fileLogger.Log("ws_congreso", "Login", "0", query);

            var user = await _unitOfWork.User.UserByEmailAsync(query.Email);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;

                _fileLogger.Log("ws_congreso", "Login", "1", response);

                return response;
            }

            if (!BC.Verify(query.Password, user.Password))
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;

                _fileLogger.Log("ws_congreso", "Login", "1", response);

                return response;
            }

            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_BLOCKED;

                _fileLogger.Log("ws_congreso", "Login", "1", response);

                return response;
            }

            if (user.EmailConfirmed == false)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EMAIL_NOT_CONFIRMED;

                _fileLogger.Log("ws_congreso", "Login", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = _jwtTokenGenerator.GenerateToken(user);
            response.Message = ReplyMessage.MESSAGE_TOKEN;

            _fileLogger.Log("ws_congreso", "Login", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            _fileLogger.Log("ws_congreso", "Login", "1", response, ex.Message);
        }

        return response;
    }
}
