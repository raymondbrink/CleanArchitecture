namespace NetActive.CleanArchitecture.Application.MediatR.Notifications
{
    using MediatR.Enums;

    /// <summary>
    /// Notification to be sent when an entity is deleted.
    /// Subscribe with MediatR to receive this notification.
    /// </summary>
    public class EntityDeletedNotification : EntityChangedNotification
    {
        public EntityDeletedNotification(object id)
            : base(id, EntityChangeType.Deleted)
        {
        }
    }
}