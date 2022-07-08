namespace Farapayamak
{
    public static class Extensions
    {
        public static IServiceCollection AddFarapayamakSMSProvider(this IServiceCollection services, Action<FarapayamakOptions> options)
        {
            services.AddHttpClient(Constants.HttpClientName, options =>
            {
                options.DefaultRequestHeaders.Clear();

                options.BaseAddress = new Uri(Constants.BaseAddress);
                options.Timeout = TimeSpan.FromMinutes(5);

                options.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.Accept), MediaTypeNames.Application.Json);
                options.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.ContentType), MediaTypeNames.Application.Json);
            });

            // services
            services.Configure(options);
            services.AddSingleton<IFarapayamakService, FarapayamakService>();

            return services;
        }
         
        public static StringContent ConvertToStringContent(this object dataObject)
        {
            var json = JsonSerializer.Serialize(dataObject);
            return new StringContent(json, Encoding.UTF8);
        }

        public static async Task<TModel?> ToJsonModelResponseAsync<TModel>(this HttpResponseMessage httpResponseMessage) where TModel : class
        {
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TModel>(result);
        }
    }
}
