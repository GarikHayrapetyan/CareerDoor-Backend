using System;
using System.Collections.Generic;

namespace Domain
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Functionality { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string Location{ get; set; }
        public DateTime Date { get; set; }
        public string EmployeeCount { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<JobCandidate> Candidates { get; set; } = new List<JobCandidate>();
    }
}
