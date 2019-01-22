using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using GigHub.Controllers;
using GigHub.Core.Models;
namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [VaildTime]
        public string Time { get; set; }
        [Required]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }
        public string Action {
            get
            {
                Expression<System.Func<GigsController, ActionResult>> edit = (c => c.Edit(this));
                Expression<System.Func<GigsController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? edit : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}