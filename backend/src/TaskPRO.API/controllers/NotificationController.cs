using TaskPRO.Application.features.Notifications.DTOs;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskPRO.Application.common.interfaces;
using TaskPRO.Domain.entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace TaskPRO.API.controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(string userId, CancellationToken ct)
        {
            var notifications = await _notificationService.GetNotificationsAsync(userId, ct);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationResponse notificationDTO, CancellationToken ct)
        {
            var notification = await _notificationService.CreateNotificationAsync(notificationDTO);
            return CreatedAtAction(nameof(GetNotifications), new { userId = notification.UserId }, notification);
        }
        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnreadNotifications(string userId, CancellationToken ct)
        {
            var unreadNotifications = await _notificationService.GetUnreadNotificationsAsync(userId, ct);
            return Ok(unreadNotifications);
        }
        [HttpPost("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(string notificationId, CancellationToken ct)
        {
            await _notificationService.MarkAsReadAsync(notificationId, ct);
            return Ok();
        }
        [HttpGet("UnreadCount/{userId}")]
        public async Task<IActionResult> GetUnreadCount(string userId, CancellationToken ct)
        {
            var unreadCount = await _notificationService.GetUnreadNotificationsCountAsync(userId, ct);
            return Ok(new { UnreadCount = unreadCount });
        }
    }
}