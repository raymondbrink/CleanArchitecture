namespace NetActive.CleanArchitecture.Application.MediatR.Interfaces
{
    using global::MediatR;

    /// <summary>
    /// Marker interface to represent a query.
    /// </summary>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
