using congreso.Application.Commons.Bases;

namespace congreso.Application.Abstractions.Messaging;
public interface IDispatcher
{
    Task<BaseResponse<TResponse>> Dispatch<TRequest, TResponse>(
        TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
}
