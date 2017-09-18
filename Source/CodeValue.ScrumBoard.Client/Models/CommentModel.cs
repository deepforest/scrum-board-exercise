    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Models
{
    public class CommentModel 
    {
        public Guid Id { get; set; }
        public DateTime CreationTimeUtc { get; set; }   
        public string Content { get; set; }
        public string CommentBy { get; set; }
    }
}
