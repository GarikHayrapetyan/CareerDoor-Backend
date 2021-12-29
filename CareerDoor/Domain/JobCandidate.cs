using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class JobCandidate
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
        public bool IsEmployer{ get; set; }
    }
}
