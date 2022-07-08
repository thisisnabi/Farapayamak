
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
