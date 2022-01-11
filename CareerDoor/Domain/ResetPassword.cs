using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ResetPassword
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string OTP { get; set; }
        public DateTime InsertDateTimeUTC { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
