namespace NetActive.CleanArchitecture.Application.MediatR.Notifications
{
    using MediatR.Enums;

    /// <summary>
    /// Notification to be sent when an entity is changed (created, updated or deleted).
    /// Subscribe with MediatR to receive this notification.
    /// </summary>
    public class EntityChangedNotification : BaseEntityNotification
    {
        public EntityChangedNotification(object id, EntityChangeType changeType)
        {
            Id = id;
            ChangeType = changeType;
        }

        /// <summary>
        /// Gets the Id of the entity that was changed.
        /// </summary>
        public object Id { get; }

        /// <summary>
        /// Gets the type of change.
        /// </summary>
        public EntityChangeType ChangeType { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{OccurredAtUTC:s}: Entity with id '{Id}' was {ChangeType}";
        }
    }
}