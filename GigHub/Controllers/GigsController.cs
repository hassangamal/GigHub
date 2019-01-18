using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig",

            };
            return View("GigForm", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            // we make that beacuse it translated into sql query and sql didn't know what identity and getuserId()
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");

        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
            if (gig == null)
                return HttpNotFound();
            var viewModel = new GigDetailsViewModel { Gig = gig };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _context.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == userId);
                viewModel.IsAttending = _context.Followings.Any(a => a.FolloweeId == gig.ArtistId && a.FollowerId == userId);

            }
            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:MM"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            // we make that beacuse it translated into sql query and sql didn't know what identity and getuserId()
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);
            gig.Modify(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");

        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre).ToList();
            return View(gigs);
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Gig).Include(g => g.Artist).Include(g => g.Genre).ToList();
            var attendaces = _context.Attendances.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList().ToLookup(a => a.GigId);
            var viewModel = new GigsViewModel
            {
                UpcompingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendaces
            };
            return View("Gigs", viewModel);
        }

    }
}