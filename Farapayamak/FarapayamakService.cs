
namespace Farapayamak;

public sealed class FarapayamakService : IFarapayamakService
{

    private readonly FarapayamakOptions _options;
    private readonly HttpClient _httpClient;

    public FarapayamakService(IOptions<FarapayamakOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options.Value;
        _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
    }

    public List<string> GetPhoneNumbers() => new List<string>();

    public async Task<(bool IsSuccess, string Response, long RecivedId)> SendSMSAsync(string toNumber, string message)
        => await SendSMSAsync(_options.DefaultNumber, toNumber, message);

    public async Task<(bool IsSuccess, string Response, long RecivedId)> SendSMSAsync(string fromNumber, string toNumber, string message)
    {
        if (string.IsNullOrEmpty(fromNumber))
            throw new ArgumentNullException(fromNumber);

        if (string.IsNullOrEmpty(toNumber))
            throw new ArgumentNullException(toNumber);

        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException(message);

        var requestModel = new SendMessageRequest
        {
            from = fromNumber,
            isFlash = _options.UseDefaultIsFlash,
            password = _options.Password,
            username = _options.Username,
            text = message,
            to = toNumber
        };

        var result = await SendPostRequestAsync<SendMessageResponse>(Constants.Routes.SendMessage, requestModel);

        if (result != null)
        {
            return (result.IsSuccess, result.Response, result.RecivedId);
        }

        return (false, Constants.Messages.AnUnknownErrorHasOccurred, -1);
    }

    public async Task<(bool IsSuccess, string Response)> GetMessageStatusAsync(long reciveId)
    {

        var requestModel = new GetDeliveriesRequest
        {
            password = _options.Password,
            username = _options.Username,
            RecId = reciveId
        };

        var result = await SendPostRequestAsync<GetDeliveriesResponse>(Constants.Routes.GetDeliveries, requestModel);

        if (result != null)
        {
            return (result.IsSuccess, result.Response);
        }

        return (false, Constants.Messages.AnUnknownErrorHasOccurred);
    }

    public async Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetInboxMessagesAsync(int index = 0)
        => await GetInboxMessagesAsync(string.Empty, index); // returm all user numbers messages

    public async Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetInboxMessagesAsync(string number, int index = 0)
    {
        var requestModel = new GetMessageRequest
        {
            password = _options.Password,
            username = _options.Username,
            from = number,
            count = _options.MaxReciveMessageCount,
            index = index,
            location = 1 // recived
        };

        var result = await SendPostRequestAsync<GetMessageResponse>(Constants.Routes.GetMessages, requestModel);

        if (result != null)
        {
            return (result.MyBase.IsSuccess, result.MyBase.Response, result.Data.Select(m => new MessageItem { 
                Body = m.Body,
                CurrentLocation = m.CurrentLocation,
                FirstLocation  = m.FirstLocation,
                IsUnicode = m.IsUnicode,
                MsgID = m.MsgID,
                Parts = m.Parts,
                RecCount = m.RecCount,
                Receiver = m.Receiver,
                RecFailed = m.RecFailed,
                RecSuccess = m.RecSuccess,
                SendDate = m.SendDate,
                Sender = m.Sender
            }).ToList());
        }

        return (false, Constants.Messages.AnUnknownErrorHasOccurred, null);
    }

    public async Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(int index = 0)
         => await GetOutboxMessagesAsync(string.Empty, index); // returm all user numbers messages

    public async Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(string number, int index = 0)
    {
        var requestModel = new GetMessageRequest
        {
            password = _options.Password,
            username = _options.Username,
            from = number,
            count = _options.MaxReciveMessageCount,
            index = index,
            location = 2 // recived
        };

        var result = await SendPostRequestAsync<GetMessageResponse>(Constants.Routes.GetMessages, requestModel);

        if (result != null)
        {
            return (result.MyBase.IsSuccess, result.MyBase.Response, result.Data.Select(m => new MessageItem
            {
                Body = m.Body,
                CurrentLocation = m.CurrentLocation,
                FirstLocation = m.FirstLocation,
                IsUnicode = m.IsUnicode,
                MsgID = m.MsgID,
                Parts = m.Parts,
                RecCount = m.RecCount,
                Receiver = m.Receiver,
                RecFailed = m.RecFailed,
                RecSuccess = m.RecSuccess,
                SendDate = m.SendDate,
                Sender = m.Sender
            }).ToList());
        }

        return (false, Constants.Messages.AnUnknownErrorHasOccurred, null);
    }





    #region Helpers




    private async Task<TResponse?> SendPostRequestAsync<TResponse>(string address, object requestModel) where TResponse : class
    {
        try
        {
            var requestContent = requestModel.ConvertToStringContent();
            var response = await _httpClient.PostAsync(address, requestContent);

            response.EnsureSuccessStatusCode();

            return await response.ToJsonModelResponseAsync<TResponse>();
        }
        catch
        {
            return default(TResponse);
        }
    }



    #endregion


}
