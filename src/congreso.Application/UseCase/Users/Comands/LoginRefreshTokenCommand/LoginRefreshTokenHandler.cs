using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Authentication;
using congreso.Application.Interfaces.Services;

namespace congreso.Application.UseCase.Users.Comands.LoginRefreshTokenCommand;

internal sealed class LoginRefreshTokenHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator) : ICommandHandler<LoginRefreshTokenCommand, string>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<BaseResponse<string>> Handle(LoginRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            var refreshToken = await _unitOfWork.RefreshToken
                .GetRefreshTokenAsync(command.RefreshToken!);

            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                response.IsSuccess = false;
                response.Message = "El token de actualización ha expirado.";
                return response;
            }

            string accessToken = _jwtTokenGenerator.GenerateToken(refreshToken.User);
            refreshToken.Token = _jwtTokenGenerator.GenerateRefreshToken();
            refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken.Token;
            response.Message = "Token de actualización creado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
