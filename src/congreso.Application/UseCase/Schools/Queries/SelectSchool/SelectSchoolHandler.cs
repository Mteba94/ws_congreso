using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.Schools.Queries.SelectSchool;

internal sealed class SelectSchoolHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<SelectSchoolQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(SelectSchoolQuery query, CancellationToken cancellationToken)
    {
        query ??= new SelectSchoolQuery();

        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "SelectSchool", "0", query);

            var school = await _unitOfWork.School.GetAllAsync();

            if(school == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "SelectSchool", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = school.Adapt<IEnumerable<SelectResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "SelectSchool", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;

            _fileLogger.Log("ws_congreso", "SelectSchool", "1", response, ex.Message);
        }

        return response;
    }
}
