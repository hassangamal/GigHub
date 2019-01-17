using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpGet]
        public IEnumerable<NotificationDto> GitNewNotification()
        {
            var userId = User.Identity.GetUserId();
            var notification = _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead).Select(un => un.Notification)
                .Include(n => n.Gig.Artist).ToList();

            return notification.Select(Mapper.Map<Notification, NotificationDto>);

        }

    }
}