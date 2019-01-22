using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Persistence;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Following GetFollowing(string ArtistId, string userId)
        {
            return _context.Followings.SingleOrDefault(a => a.FolloweeId == ArtistId && a.FollowerId == userId);
        }
    }
}