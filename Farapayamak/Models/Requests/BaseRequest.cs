using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak.Models.Requests
{
    public class BaseRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string from { get; set; }
    }
}
