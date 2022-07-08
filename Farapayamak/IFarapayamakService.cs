namespace Farapayamak;

public interface IFarapayamakService
{
    Task<(bool IsSuccess, string Response, long RecivedId)> SendAsync(string toNumber, string message);
    Task<(bool IsSuccess, string Response, long RecivedId)> SendAsync(string fromNumber, string toNumber, string message);
     
    Task<(bool IsSuccess, List<(string number, string response, long RecivedId)>? Status)> SendRangeAsync(List<string> toNumber, string message);
    Task<(bool IsSuccess, List<(string number, string response, long RecivedId)>? Status)> SendRangeAsync(string fromNumber, List<string> toNumber, string message);
      
    Task<(bool IsSuccess, string Response)> GetMessageStatusAsync(long reciveId);

    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetInboxMessagesAsync(int index = 0);
    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetInboxMessagesAsync(string number, int index = 0);
    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(int index = 0);
    Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(string number, int index = 0);
     
    Task<(bool IsSuccess, decimal Credit)> GetCredit();
    Task<(bool IsSuccess, decimal BasePrice)> GetBasePrice();

    Task<(bool IsSuccess, decimal Balance)> GetAccountBalance();

    Task<(bool IsSuccess, string Response, List<string>? Numbers)> GetUserNumbers();
      
}
