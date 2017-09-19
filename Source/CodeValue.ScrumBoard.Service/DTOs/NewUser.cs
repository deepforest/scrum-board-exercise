using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class NewUser
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public byte[] Image { get; set; }
    }
}
