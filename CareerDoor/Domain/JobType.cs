using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class JobType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
