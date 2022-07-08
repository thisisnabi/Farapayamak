﻿
 
namespace Farapayamak
{
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

        public async Task<(bool IsSuccess, string Message)> SendSMSAsync(string toNumber, string message)
        {
            if (string.IsNullOrEmpty(toNumber))
                throw new ArgumentNullException(toNumber);

            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(message);

            var requestModel = new SendMessageRequest
            {
                from = _options.DefaultNumber ?? throw new ArgumentNullException("fromNumber"),
                isFlash = _options.UseDefaultIsFlash,
                password = _options.Password,
                username = _options.Username,
                text = message,
                to = toNumber
            };

            var result = await SendPostRequest<SendMessageResponse>(Constants.Routes.SendMessageRoute, requestModel);

            return (result.IsSuccess, result.Message);
        }

        public async Task<(bool IsSuccess, string Message)> SendSMSAsync(string fromNumber ,string toNumber, string message)
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

            var result = await SendPostRequest<SendMessageResponse>(Constants.Routes.SendMessageRoute, requestModel);

            return (result.IsSuccess, result.Message);
        }






        #region Helpers

        private async Task<TResponse> SendPostRequest<TResponse>(string address, object requestModel) where TResponse : class
        {
            try
            {
                var requestContent = requestModel.ConvertToStringContent();
                var response = await _httpClient.PostAsync(address, requestContent);

                response.EnsureSuccessStatusCode();

                return await response.ToJsonModelResponseAsync<TResponse>();

            }
            catch (Exception ex)
            {
                return 
            }
        }
        #endregion


    }
}
