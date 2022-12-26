namespace NetActive.CleanArchitecture.Application.MediatR.Notifications
{
    using global::MediatR;

    /// <summary>
    /// Base for notifications to be sent when an event related to an entity occurs.
    /// </summary>
    public abstract class BaseEntityNotification : INotification
    {
        /// <summary>
        /// Date/time (in UTC) the event occurred.
        /// </summary>
        public DateTime OccurredAtUTC { get; protected set; } = DateTime.UtcNow;
    }
}
