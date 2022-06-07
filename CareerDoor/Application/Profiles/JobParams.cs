using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class JobParams:PagingParams
    {
        public string Username { get; set; }
        public string Predicate { get; set; }
    }
}
