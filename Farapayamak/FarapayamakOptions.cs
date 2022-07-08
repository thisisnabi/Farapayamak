using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak
{
    public class FarapayamakOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<FarapayamakPhoneOptions> Phones { get; set; }
        public bool UseDefaultIsFlash { get; set; } = false;
    }
     
    public class FarapayamakPhoneOptions 
    {
        public string Number { get; set; }
        public bool IsDefault { get; set; }
    }
}
