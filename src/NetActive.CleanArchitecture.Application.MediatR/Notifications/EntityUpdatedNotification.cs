﻿namespace NetActive.CleanArchitecture.Application.MediatR.Notifications
{
    using MediatR.Enums;

    /// <summary>
    /// Notification to be sent when an entity is updated.
    /// Subscribe with MediatR to receive this notification.
    /// </summary>
    public class EntityUpdatedNotification : EntityChangedNotification
    {
        public EntityUpdatedNotification(object id)
            : base(id, EntityChangeType.Updated)
        {
        }
    }
}