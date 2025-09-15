using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IRefreshTokenRepository
{
    void CreateToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);
    Task<bool> RevokeRefreshTokenAsync(int userId);
}
