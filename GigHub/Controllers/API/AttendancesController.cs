using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using GigHub.Dtos;
namespace GigHub.Controllers.API
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();

            var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == attendanceDto.GigId);
            if (exists)
                return BadRequest("this attendance already exist");
            var attendance = new Attendance
            {
                GigId = attendanceDto.GigId,
                AttendeeId = userId,
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

    }
}
