namespace NetActive.CleanArchitecture.Application.MediatR.Interfaces
{
    using global::MediatR;


    /// <summary>
    /// Marker interface to represent a void command.
    /// </summary>
    public interface ICommand : ICommand<Unit>
    {
    }

    /// <summary>
    /// Marker interface to represent a command with a response.
    /// </summary>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}
