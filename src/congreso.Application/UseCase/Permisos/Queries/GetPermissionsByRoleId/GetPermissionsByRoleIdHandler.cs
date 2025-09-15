using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Permisos;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

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
            var menus = await _unitOfWork.Menus.GetMenuPermissionAsync();

            if(menus is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            foreach (var menu in menus)
            {
                var permissions = await _unitOfWork.Permisos.GetPermissionsByMenuId(menu.Id);

                var dto = new PermissionsByRoleResponseDto
                {
                    MenuId = menu.Id,
                    FatherId = menu.FatherId,
                    Menu = menu.Name!,
                    Icon = menu.Icon!,
                    Permissions = permissions.Adapt<List<PermissionsResponseDto>>()
                };

                if (query.RoleId.HasValue)
                {
                    var rolePermissions = await _unitOfWork.Permisos.GetRolePermissionsByMenuId(query.RoleId.Value, menu.Id);
                    foreach (var permission in dto.Permissions)
                    {
                        permission.Selected = rolePermissions.Any(rp => rp.Id == permission.PermissionId);
                    }
                }

                permissionsResult.Add(dto);
            }

            response.IsSuccess = true;
            response.Data = permissionsResult;
            response.Message = ReplyMessage.MESSAGE_QUERY;
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
