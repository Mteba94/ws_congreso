namespace congreso.Application.Behaviours
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(T request, CancellationToken cancellationToken = default);
    }
}
