using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid JobTypeId { get; set; }
        public JobType JobType { get; set; }
        public string Description { get; set; }
        public string Functionality { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string Location{ get; set; }
        public string Experience { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Creation { get; set; }
        public string EmployeeCount { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<JobCandidate> Candidates { get; set; } = new List<JobCandidate>();
    }
}
