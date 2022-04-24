using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public static class EmailResetOTP
    {

        public static string GenerateOTP() {

            Random otp = new Random();

            return otp.Next(100000, 999999).ToString();

        }
    }
}
