using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Followers
{
    public class FollowParams: PagingParams
    {
        public string Predicate { get; set; }
        public string Username { get; set; }
    }
}
