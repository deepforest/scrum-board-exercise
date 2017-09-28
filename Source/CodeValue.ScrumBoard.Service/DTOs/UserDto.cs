using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }
    }

    public class UserCredentialsDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class UserRegistrationDto
    {
        public UserCredentialsDto Credentials { get; set; }

        public byte[] Image { get; set; }
    }
}
