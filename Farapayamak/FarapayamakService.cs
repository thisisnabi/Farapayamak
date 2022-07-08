
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

    public async Task<(bool IsSuccess, string Response, long RecivedId)> SendAsync(string toNumber, string message)
        => await SendAsync(_options.DefaultNumber, toNumber, message);

    public async Task<(bool IsSuccess, string Response, long RecivedId)> SendAsync(string fromNumber, string toNumber, string message)
    {
        if (string.IsNullOrEmpty(fromNumber))
            throw new ArgumentNullException(fromNumber);

        if (string.IsNullOrEmpty(toNumber))
            throw new ArgumentNullException(toNumber);

        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException(message);

        var requestModel = new SendMessageRequest
        {
            username = _options.Username,
            password = _options.Password,
            from = fromNumber,
            text = message,
            to = toNumber,
            isFlash = _options.UseDefaultIsFlash
        };

        var result = await SendPostRequestAsync<SendMessageResponse>(Constants.Routes.SendMessage, requestModel);

        if (result != null)
        {
            return (result.IsSuccess, result.Response, result.RecivedId);
        }

        return (false, Constants.Messages.AnUnknownErrorHasOccurred, -1);
    }


    public async Task<(bool IsSuccess, List<(string number, string response, long RecivedId)>? Status)> SendRangeAsync(List<string> toNumber, string message)
         => await SendRangeAsync(_options.DefaultNumber, toNumber, message);

    public async Task<(bool IsSuccess, List<(string number, string response, long RecivedId)>? Status)> SendRangeAsync(string fromNumber, List<string> toNumber, string message)
    {
        if (string.IsNullOrEmpty(fromNumber))
            throw new ArgumentNullException(fromNumber);

        if (toNumber == null || toNumber.Count == 0)
            throw new ArgumentNullException(nameof(toNumber));

        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException(message);

        var requestModel = new SendMessageRequest
        {
            username = _options.Username,
            password = _options.Password,
            from = fromNumber,
            text = message,
            to = string.Join(",", toNumber.ToArray()),
            isFlash = _options.UseDefaultIsFlash
        };

        var result = await SendPostRequestAsync<SendRangeMessageResponse>(Constants.Routes.SendMessage, requestModel);

        if (result != null)
        {
            var resultRange = result.GetRangeResponse();
            var response = new List<(string number, string response, long RecivedId)>();

            for (int i = 0; i < resultRange.Count; i++)
            {
                response.Add((toNumber[i], resultRange[i].Response, resultRange[i].RecivedId));
            }

            return (result.IsSuccess, response);
        }

        return (false, null);
    }

    public async Task<(bool IsSuccess, string Response)> GetMessageStatusAsync(long reciveId)
    {

        var requestModel = new GetDeliveriesRequest
        {
            username = _options.Username,
            password = _options.Password,
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
            username = _options.Username,
            password = _options.Password,
            from = number,
            count = _options.MaxReciveMessageCount,
            index = index,
            location = 1 // recived
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

    public async Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(int index = 0)
         => await GetOutboxMessagesAsync(string.Empty, index); // returm all user numbers messages

    public async Task<(bool IsSuccess, string Response, List<MessageItem>? Messages)> GetOutboxMessagesAsync(string number, int index = 0)
    {
        var requestModel = new GetMessageRequest
        {
            username = _options.Username,
            password = _options.Password,
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



    public async Task<(bool IsSuccess, decimal Credit)> GetCredit()
    {
        var requestModel = new GetCreditRequest
        {
            username = _options.Username,
            password = _options.Password,
        };

        var result = await SendPostRequestAsync<GetCreditResponse>(Constants.Routes.GetCredit, requestModel);

        if (result != null)
        {
            return (result.IsSuccess, decimal.TryParse(result.Response, out decimal val) ? val : 0);
        }

        return (false, 0);
    }

    public async Task<(bool IsSuccess, decimal BasePrice)> GetBasePrice()
    {
        var requestModel = new GetBasePriceRequest
        {
            username = _options.Username,
            password = _options.Password,
        };

        var result = await SendPostRequestAsync<GetCreditResponse>(Constants.Routes.GetBasePrice, requestModel);

        if (result != null)
        {
            return (result.IsSuccess, decimal.TryParse(result.Response, out decimal val) ? val : 0);
        }

        return (false, 0);
    }

    public async Task<(bool IsSuccess, decimal Balance)> GetAccountBalance()
    {
        var creditResult = await GetCredit();
        var basePriceResult = await GetBasePrice();

        return (creditResult.IsSuccess && basePriceResult.IsSuccess,
            basePriceResult.BasePrice * creditResult.Credit);
    }
     
    public async Task<(bool IsSuccess, string Response,List<string>? Numbers)> GetUserNumbers()
    {
        var requestModel = new GetUserNumbersRequest
        {
            username = _options.Username,
            password = _options.Password,
        };

        var result = await SendPostRequestAsync<GetUserNumberResponse>(Constants.Routes.GetUserNumbers, requestModel);

        if (result != null)
        {
            return (result.MyBase.IsSuccess, 
                result.MyBase.Response,
                result.Data.Select(f => f.Number).ToList());
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
