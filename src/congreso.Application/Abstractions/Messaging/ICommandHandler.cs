using congreso.Application.Commons.Bases;

namespace congreso.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<BaseResponse<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
