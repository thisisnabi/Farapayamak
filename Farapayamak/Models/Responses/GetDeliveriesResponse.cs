namespace Farapayamak.Models.Responses;

public class GetDeliveriesResponse : BaseResponse
{
    public string Response => Value switch
    {
        "0" => GetDeliveriesEnum.PostedToTelecommunications.ToPersian(),
        "1" => GetDeliveriesEnum.Delivered.ToPersian(),
        "2" => GetDeliveriesEnum.NotReachingToThePhone.ToPersian(),
        "3" => GetDeliveriesEnum.CommunicationError.ToPersian(),
        "5" => GetDeliveriesEnum.UnknownError.ToPersian(),
        "8" => GetDeliveriesEnum.ReceivedToTelecommunications.ToPersian(),
        "16" => GetDeliveriesEnum.NotReachingToTelecommunications.ToPersian(),
        "35" => GetDeliveriesEnum.BlackList.ToPersian(),
        "100" => GetDeliveriesEnum.Unknown.ToPersian(),
        "200" => GetDeliveriesEnum.Posted.ToPersian(),
        "300" => GetDeliveriesEnum.Filtered.ToPersian(),
        "400" => GetDeliveriesEnum.OnHoldList.ToPersian(),
        "500" => GetDeliveriesEnum.ServerError.ToPersian(),
        _ => GetDeliveriesEnum.UnknownError.ToPersian()
    };
}

public enum GetDeliveriesEnum
{
    [PersianTitle("ارسال شده به مخابرات.")]
    PostedToTelecommunications = 0,

    [PersianTitle("رسیده به گوشی.")]
    Delivered,

    [PersianTitle("نرسیده به گوشی.")]
    NotReachingToThePhone,

    [PersianTitle("خطای مخابراتی.")]
    CommunicationError,

    [PersianTitle("خطای نامشخص.")]
    UnknownError = 5,

    [PersianTitle("رسیده به مخابرات.")]
    ReceivedToTelecommunications = 8,

    [PersianTitle("نرسیده به مخابرات.")]
    NotReachingToTelecommunications = 16,

    [PersianTitle("لیست سیاه.")]
    BlackList = 35,

    [PersianTitle("نامشخص.")]
    Unknown = 100,

    [PersianTitle("ارسال شده.")]
    Posted = 200,

    [PersianTitle("فیلتر شده.")]
    Filtered = 300,

    [PersianTitle("در لیست ارسال.")]
    OnHoldList = 400,

    [PersianTitle("عدم پذیرش اگر وضعیت از سمت اپراتور مشخص نشده باشد.")]
    ServerError = 500,
}

