using Application.Profiles;
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
        public string Function { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeCount { get; set; }
        public string EmployeerUsername { get; set; }
        public ICollection<Profile> Profiles { get; set; } = new List<Profile>();
    }
}
