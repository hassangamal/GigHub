using AutoMapper;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Persistence;
using GigHub.Core.Dtos;

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

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notification = _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
            notification.ForEach(n => n.Read());
            _context.SaveChanges();
            return Ok();
        }

    }
}