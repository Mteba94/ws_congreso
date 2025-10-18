using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Users.Queries.GetUserCertificateCount;

internal sealed class GetUserCertificateCountHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetUserCertificateCountQuery, UserCertificateCountDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<UserCertificateCountDto>> Handle(GetUserCertificateCountQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetUserCertificateCountAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<UserCertificateCountDto>> GetUserCertificateCountAsync(GetUserCertificateCountQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserCertificateCountDto>();

        try
        {
            var diplomas = await _unitOfWork.Diploma.GetAllAsync();

            if (diplomas == null || !diplomas.Any())
            {
                response.IsSuccess = true;
                response.Data = new UserCertificateCountDto { UserId = query.UserId, CertificateCount = 0 };
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Assuming Diploma entity has a UserId property or can be linked via Inscripcion
            // For now, let's assume Diploma has an InscripcionId, and Inscripcion has a UserId
            var userInscriptions = await _unitOfWork.Inscripcion.GetAllAsync();

            var certificateCount = (from d in diplomas
                                    join i in userInscriptions on d.InscripcionId equals i.Id
                                    where i.UserId == query.UserId
                                    select d).Count();

            response.IsSuccess = true;
            response.Data = new UserCertificateCountDto { UserId = query.UserId, CertificateCount = certificateCount };
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
