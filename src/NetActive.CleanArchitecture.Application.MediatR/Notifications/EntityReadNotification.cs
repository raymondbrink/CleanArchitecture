namespace NetActive.CleanArchitecture.Application.MediatR.Notifications
{
    /// <summary>
    /// Notification to be sent when at least one entity is read.
    /// Subscribe with MediatR to receive this notification.
    /// </summary>
    public class EntityReadNotification : BaseEntityNotification
    {
        public EntityReadNotification(string notification)
        {
            Notification = notification;
        }

        public string Notification { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{OccurredAtUTC:s}: {Notification}";
        }
    }
}