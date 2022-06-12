using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class JobParams : PagingParams
    {
        public bool IsCandidate { get; set; }
        public bool IsEmployer { get; set; }
       // public DateTime ExpirationDate { get; set; } = DateTime.UtcNow;
    }
} 
