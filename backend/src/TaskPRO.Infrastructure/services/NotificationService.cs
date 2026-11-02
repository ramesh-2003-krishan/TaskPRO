using TaskPRO.Application.common.interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Application.features.Notifications.DTOs;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskPRO.Infrastructure.services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDBContext _dbContext;

        public NotificationService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task NotifyAsync(string userId, string message, CancellationToken ct = default)
        {
            var notification = new Notification
            {
                UserId = Guid.Parse(userId),
                Title = "Notification",
                Message = message,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Notifications.AddAsync(notification, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<NotificationResponse> CreateNotificationAsync(NotificationResponse notificationDTO)
        {
            var notification = new Notification
            {
                UserId = notificationDTO.UserId,
                Title = notificationDTO.Title,
                Message = notificationDTO.Message,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();

            
            notificationDTO.Id = notification.Id;
            notificationDTO.CreatedAt = notification.CreatedAt;
            return notificationDTO;
        }
        public async Task<IEnumerable<NotificationResponse>> GetNotificationsAsync(string userId, CancellationToken ct = default)
        {
            var parsedUserId = Guid.Parse(userId);
            var notifications = await _dbContext.Notifications
                .Where(n => n.UserId == parsedUserId)
                .Select(n => new NotificationResponse
                {
                    Id = n.Id,
                    Title = n.Title,
                    UserId = n.UserId,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync(ct);

            return notifications;
        }
        public async Task MarkAsReadAsync(string notificationId, CancellationToken ct = default)
        {
            var parsedNotificationId = int.Parse(notificationId);
            var notification = await _dbContext.Notifications.FindAsync(new object[] { parsedNotificationId }, ct);
            if (notification != null)
            {
                notification.IsRead = true;
                await _dbContext.SaveChangesAsync(ct);
            }
        }
        public async Task DeleteNotificationAsync(string notificationId)
        {
            var notification = await _dbContext.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _dbContext.Notifications.Remove(notification);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAllNotificationsForUserAsync(Guid userId)
        {
            var notifications = _dbContext.Notifications.Where(n => n.UserId == userId);
            _dbContext.Notifications.RemoveRange(notifications);
            await _dbContext.SaveChangesAsync();
        }
        public async Task MarkAllNotificationsAsReadForUserAsync(Guid userId)
        {
            var notifications = await _dbContext.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<NotificationResponse>> GetUnreadNotificationsAsync(string userId, CancellationToken ct = default)
        {
            var parsedUserId = Guid.Parse(userId);
            var unreadNotifications = await _dbContext.Notifications
                .Where(n => n.UserId == parsedUserId && !n.IsRead)
                .Select(n => new NotificationResponse
                {
                    Id = n.Id,
                    Title = n.Title,
                    UserId = n.UserId,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync(ct);

            return unreadNotifications;
        }
        public async Task<int> GetUnreadNotificationsCountAsync(string userId, CancellationToken ct = default)
        {
            var parsedUserId = Guid.Parse(userId);
            var unreadCount = await _dbContext.Notifications
                .CountAsync(n => n.UserId == parsedUserId && !n.IsRead, ct);

            return unreadCount;
        }
    }
}