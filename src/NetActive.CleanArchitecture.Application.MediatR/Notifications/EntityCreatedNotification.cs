namespace NetActive.CleanArchitecture.Application.MediatR.Notifications
{
    using MediatR.Enums;

    /// <summary>
    /// Notification to be sent when an entity is created.
    /// Subscribe with MediatR to receive this notification.
    /// </summary>
    public class EntityCreatedNotification : EntityChangedNotification
    {
        public EntityCreatedNotification(object id)
            : base(id, EntityChangeType.Created)
        {
        }
    }
}