using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class GetPostRequest
    {
        public int PageCount { get; set; }
        public int ItemsPerPage { get; set; }
        public PostFilter Filter { get; set; }
    }

    public class PostFilter
    {
        public string Category { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
    }
}
