using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }

        public string Error { get; set; }
    }
}
