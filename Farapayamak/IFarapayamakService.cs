namespace Farapayamak;

public interface IFarapayamakService
{
    Task<(bool IsSuccess, string Response)> SendSMSAsync(string toNumber, string message);
    Task<(bool IsSuccess, string Response)> SendSMSAsync(string fromNumber, string toNumber, string message);
    List<string> GetPhoneNumbers();
}
