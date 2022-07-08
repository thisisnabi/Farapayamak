using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak
{
    public static class Constants
    {
        public const string HttpClientName = "farapayamak_client";
        public const string BaseAddress = "https://rest.payamak-panel.com/api/SendSMS/";


        public static class Routes
        {
            public const string SendMessageRoute = "SendSMS";
        }
    }
}
