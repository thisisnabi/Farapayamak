using System.Reflection;

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

        public static string ToPersian(this Enum val)
        {
            var fieldInfo = val.GetType().GetField(val.ToString());

            if (fieldInfo is null)
                return string.Empty;

            var attributes = (PersianTitleAttribute[])fieldInfo.GetCustomAttributes(typeof(PersianTitleAttribute), false);
            return attributes.Length > 0 ? attributes[0].Title : string.Empty;
        } 

        public static StringContent ConvertToStringContent(this object dataObject)
        {
            var json = JsonSerializer.Serialize(dataObject);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        public static async Task<TModel> ToJsonModelResponseAsync<TModel>(this HttpResponseMessage httpResponseMessage) where TModel : class
        {
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TModel>(result) ?? null;
        }
         
    }
}
