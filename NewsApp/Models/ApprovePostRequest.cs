using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class ApprovePostRequest
    {
        public int PostId { get; set; }
        public bool IsApproved { get; set; }
        public string Note { get; set; }
        public string ApprovalTime { get; set; }
    }
}

