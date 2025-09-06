using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.School;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.Schools.Queries.GetById;

internal sealed class GetByIdSchoolHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetByIdSchoolQuery, SchoolByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<SchoolByIdResponseDto>> Handle(GetByIdSchoolQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<SchoolByIdResponseDto>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdSchool", "0", query);

            var school = await _unitOfWork.School.GetByIdAsync(query.schoolId);

            if (school == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdSchool", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = school.Adapt<SchoolByIdResponseDto>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdSchool", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }

        return response;
    }
}
