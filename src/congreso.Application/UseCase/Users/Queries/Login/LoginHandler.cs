using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Authentication;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Users.Queries.Login;

internal sealed class LoginHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, IFileLogger fileLogger) : IQueryHandler<LoginQuery, string>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            _fileLogger.Log("ws_congreso", "Login", "0", JsonSerializer.Serialize(query));

            var user = await _unitOfWork.User.UserByEmailAsync(query.Email);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;

                _fileLogger.Log("ws_congreso", "Login", "1", response);

                return response;
            }

            if(!BC.Verify(query.Password, user.Password))
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
            response.AccessToken = _jwtTokenGenerator.GenerateToken(user);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = _jwtTokenGenerator.GenerateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            _unitOfWork.RefreshToken.CreateToken(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            response.RefreshToken = refreshToken.Token;
            response.Message = "Token generado correctamente";

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
