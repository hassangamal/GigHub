using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Persistence;
using GigHub.Core.Repositories;
namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(a => a.Attendances.Select(g => g.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }
        public Gig GetGigWithGenreandAritst(int gigId)
        {
            return _context.Gigs
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }
        public IEnumerable<Gig> GetGigUserAttending(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Gig).Include(g => g.Artist).Include(g => g.Genre).ToList();
        }
        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId)
        {
            return _context.Gigs.Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCanceled).Include(g => g.Genre).ToList(); ;
        }
        public Gig GetGig(int gigId)
        {
            return _context.Gigs.Include(g => g.Artist).Include(g => g.Genre).SingleOrDefault(g => g.Id == gigId);
        }
        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }

}