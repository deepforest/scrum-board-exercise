using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Models
{
    public class UserModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public byte[] Image { get; set; }
    }
}
