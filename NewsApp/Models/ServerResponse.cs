using NewsApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class ServerResponse<T>
    {
        public MessageType Type { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
