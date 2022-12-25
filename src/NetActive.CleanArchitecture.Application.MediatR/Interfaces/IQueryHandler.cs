namespace NetActive.CleanArchitecture.Application.MediatR.Interfaces
{
    using global::MediatR;

    /// <summary>
    /// Defines a query handler.
    /// </summary>
    /// <typeparam name="TQuery">Query to handle.</typeparam>
    /// <typeparam name="TResponse">Response to return.</typeparam>
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
