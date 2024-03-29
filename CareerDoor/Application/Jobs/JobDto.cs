﻿using Application.Profiles;
using System;
using System.Collections.Generic;


namespace Application.Jobs
{
    public class JobDto
    {        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Functionality { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Creation { get; set; }
        public string EmployeeCount { get; set; }
        public string EmployeerUsername { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<Profile> Candidates { get; set; }
    }
}
