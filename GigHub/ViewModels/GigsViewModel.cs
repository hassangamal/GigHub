﻿using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcompingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}