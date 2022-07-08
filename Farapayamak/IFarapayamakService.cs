namespace Farapayamak;

public interface IFarapayamakService
{
    Task<(bool IsSuccess, string Response,long RecivedId)> SendSMSAsync(string toNumber, string message);
    Task<(bool IsSuccess, string Response, long RecivedId)> SendSMSAsync(string fromNumber, string toNumber, string message);

    Task<(bool IsSuccess, string Response)> GetMessageStatusAsync(long reciveId);

    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetInboxMessagesAsync(int index = 0);
    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetInboxMessagesAsync(string number, int index = 0);
    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(int index = 0);
    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(string number, int index = 0);






    List<string> GetPhoneNumbers();
}
