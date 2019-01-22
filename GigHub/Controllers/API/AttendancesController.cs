using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using GigHub.Core.Dtos;
using GigHub.Persistence;

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

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);
            if (attendance == null)
                return NotFound();
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return Ok(id);
        }

    }
}
