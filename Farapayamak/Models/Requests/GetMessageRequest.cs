using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak.Models.Requests
{
    public class GetMessageRequest : BaseRequest
    {
        public int location { get; set; }
        public string from { get; set; }
        public int index { get; set; }
        public int count { get; set; }
    }
}
