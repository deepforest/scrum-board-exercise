using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string Hash { get; internal set; }

        /// <summary>
        /// Salted password.
        /// </summary>
        public string Salt { get; set; }

        public byte[] Image { get; set; }
    }
}
