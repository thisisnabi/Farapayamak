﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak
{
    public interface IFarapayamakService
    {
        Task SendSMSAsync(string toNumber, string message);
        List<string> GetPhoneNumbers();
    }
}
