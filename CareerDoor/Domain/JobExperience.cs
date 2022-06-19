using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class JobExperience
    {
        public Guid Id { get; set; }
        public string Experience { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
