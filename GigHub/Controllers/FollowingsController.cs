using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();

            var exists = _context.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == followingDto.FolloweeId);
            if (exists)
                return BadRequest("Following already exist");
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}
