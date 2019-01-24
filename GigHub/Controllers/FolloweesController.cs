using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using GigHub.Persistence;
namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var follow = _context.Followings
                .Where(a => a.FollowerId == userId)
                .Include(a => a.Followee)
                .ToList();
            return View(follow);
        }
    }
}