namespace Farapayamak;

public static class Constants
{
    public const string HttpClientName = "farapayamak_client";
    public const string BaseAddress = "https://rest.payamak-panel.com/api/SendSMS/";

    public static class Routes
    {
        public const string SendMessage = "SendSMS";
        public const string GetDeliveries = "GetDeliveries2";
        public const string GetMessages = "GetMessages";
        public const string GetCredit = "GetCredit";
        public const string GetBasePrice = "GetBasePrice";
        public const string GetUserNumbers = "GetUserNumbers";
    }

    public static class Messages 
    { 
        public const string AnUnknownErrorHasOccurred = "خطای ناشناخته ای رخ داده است.";

    }
}
