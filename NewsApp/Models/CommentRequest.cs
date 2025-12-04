using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class CommentRequest
    {
        public string CommenterName { get; set; }
        public string CommentContent { get; set; }
        public string CommentTime { get; set; }
    }
}
