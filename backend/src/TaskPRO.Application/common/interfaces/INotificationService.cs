using TaskPRO.Application.features.Notifications.DTOs;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.common.interfaces
{
    public interface INotificationService
    {
       Task NotifyAsync(string userId, string message, CancellationToken ct = default);
       Task<IEnumerable<NotificationResponse>> GetNotificationsAsync(string userId, CancellationToken ct = default);
         Task MarkAsReadAsync(string notificationId, CancellationToken ct = default);
         Task<IEnumerable<NotificationResponse>> GetUnreadNotificationsAsync(string userId, CancellationToken ct = default);
    }
}