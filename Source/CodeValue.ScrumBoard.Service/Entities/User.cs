using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Secret { get; set; }

        public byte[] Image { get; set; }
    }
}
