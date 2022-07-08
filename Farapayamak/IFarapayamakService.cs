namespace Farapayamak;

public interface IFarapayamakService
{
    Task<(bool IsSuccess, string Response,long RecivedId)> SendSMSAsync(string toNumber, string message);
    Task<(bool IsSuccess, string Response, long RecivedId)> SendSMSAsync(string fromNumber, string toNumber, string message);

    Task<(bool IsSuccess, string Response)> GetMessageStatusAsync(long reciveId);


    List<string> GetPhoneNumbers();
}
