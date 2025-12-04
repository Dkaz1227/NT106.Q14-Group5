using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class PostInfo
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CreatedTime { get; set; }
    }
}

