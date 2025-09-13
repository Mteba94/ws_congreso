using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Permisos;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;

namespace congreso.Application.UseCase.Permisos.Queries.GetPermissionsByRoleId;

internal sealed class GetPermissionsByRoleIdHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetPermissionsByRoleIdQuery, IEnumerable<PermissionsByRoleResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>> Handle(GetPermissionsByRoleIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>();
        var permissionsResult = new List<PermissionsByRoleResponseDto>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetPermissionsByRoleId", "0", query);
        }
        catch(Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetPermissionsByRoleId", "1", response, ex.Message);
        }
        return response;
    }
}
