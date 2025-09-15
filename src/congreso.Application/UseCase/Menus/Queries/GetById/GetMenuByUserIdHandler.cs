using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Menus;
using congreso.Application.Interfaces.Services;
using Mapster;

namespace congreso.Application.UseCase.Menus.Queries.GetById;

internal sealed class GetMenuByUserIdHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetMenuByUserIdQuery, IEnumerable<MenuResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<MenuResponseDto>>> Handle(GetMenuByUserIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<MenuResponseDto>>();

        try
        {
            var menus = await _unitOfWork.Menus.GetMenuByUserIdAsync(query.UserId);

            if (menus is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = menus.Adapt<IEnumerable<MenuResponseDto>>();
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
