using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using GigHub.Persistence;

namespace GigHub.Controllers.API
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
        [HttpDelete]
        public IHttpActionResult Unfollow(string Id)
        {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings.SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == Id);
            if (following == null)
                return NotFound();
            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok(Id);
        }

    }
}
