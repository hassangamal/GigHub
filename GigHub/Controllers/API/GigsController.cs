using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using GigHub.Core.Dtos;
using System.Data.Entity;
using GigHub.Persistence;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Include(g => g.Attendances.Select(a => a.Attendee)).Single(g => g.Id == id && g.ArtistId == userId);
            if (gig.IsCanceled)
                return NotFound();
            gig.Cancel();
            _context.SaveChanges();
            return Ok();
        }
    }
}
