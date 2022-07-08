using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak
{
    public interface IFarapayamakService
    {
        Task<(bool IsSuccess,string Message)> SendSMSAsync(string toNumber, string message);
        Task<(bool IsSuccess, string Message)> SendSMSAsync(string fromNumber,string toNumber, string message);
        List<string> GetPhoneNumbers();
    }
}
