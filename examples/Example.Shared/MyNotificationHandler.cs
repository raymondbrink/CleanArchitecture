namespace Example.Shared
{
    using MediatR;
    using NetActive.CleanArchitecture.Application.MediatR.Notifications;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The notification handler is registered in AutofacConfig.cs to handle all commands (CUD) and queries (R) on Manufacturer.
    /// </summary>
    public class MyNotificationHandler : 
        INotificationHandler<EntityReadNotification>, 
        INotificationHandler<EntityChangedNotification>
    {
        public Task Handle(EntityReadNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Received read notification: {0}", notification);

            return Task.CompletedTask;
        }

        public Task Handle(EntityChangedNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Received change notification: {0}", notification);
            
            return Task.CompletedTask;
        }
    }
}
