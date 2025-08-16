using congreso.Application.Behaviours;
using congreso.Application.Commons.Bases;
using congreso.Application.Commons.Exceptions;
using Microsoft.Extensions.Logging;

namespace congreso.Application.Abstractions.Messaging;
public class HandlerExecutor(IValidationService validationService, ILogger<HandlerExecutor> logger)
{
    private readonly IValidationService _validationService = validationService;
    private readonly ILogger<HandlerExecutor> _logger = logger;

    public async Task<BaseResponse<T>> ExecuteAsync<TRequest, T>(TRequest request, Func<Task<BaseResponse<T>>> action, CancellationToken cancellationToken)
    {
        try
        {
            await _validationService.ValidateAsync(request, cancellationToken);

            return await action();
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation failed for request {@Request}. Errors {@Errors}", request, ex.Errors);
            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = "Error de validación",
                Errors = ex.Errors
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing request {@Request}", request);

            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = "Ocurrió un error al procesar la solicitud",
                Errors =
                [
                    new() { PropertyName = "Exepcion", ErrorMessage = ex.Message }
                ]
            };
        }
    }
}
