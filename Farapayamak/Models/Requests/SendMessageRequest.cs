using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak.Models.Requests
{
    public class SendMessageRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
        public bool isFlash { get; set; }
    }
}
