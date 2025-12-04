using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Keywords { get; set; }
        public string Category { get; set; }
    }
}

