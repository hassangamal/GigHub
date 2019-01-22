using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        Gig GetGigWithGenreandAritst(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
    }
}