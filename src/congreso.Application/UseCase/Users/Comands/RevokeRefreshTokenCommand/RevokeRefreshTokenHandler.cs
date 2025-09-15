using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Users.Comands.RevokeRefreshTokenCommand;

internal sealed class RevokeRefreshTokenHandler(IUnitOfWork unitOfWork) : ICommandHandler<RevokeRefreshTokenCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            await _unitOfWork.RefreshToken.RevokeRefreshTokenAsync(command.UserId);
            response.IsSuccess = true;
            response.Message = "Revocar el token de actualización con éxito.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
