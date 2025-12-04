using NewsApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class ClientRequest
    {
        public MessageType MessageType { get; set; }
        public UserType UserType { get; set; }
        public object Data { get; set; }
    }
}
