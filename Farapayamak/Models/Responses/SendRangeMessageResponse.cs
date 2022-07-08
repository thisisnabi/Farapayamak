namespace Farapayamak.Models.Responses;

public class SendRangeMessageResponse : BaseResponse
{

    public List<RangeReciverIdMessage> GetRangeResponse()
    {
        List<RangeReciverIdMessage> result = new();

        var vals = Value.Split(',');

        foreach (var val in vals)
        {
            var response = val switch
            {
                "0" => SendMessageEnum.InvalidUsernameOrPassword.ToPersian(),
                "1" => SendMessageEnum.TheRequestWasMadeSuccessfully.ToPersian(),
                "2" => SendMessageEnum.NotEnoughCredit.ToPersian(),
                "3" => SendMessageEnum.DailySubmissionLimit.ToPersian(),
                "4" => SendMessageEnum.LimitationOnSendingVolume.ToPersian(),
                "5" => SendMessageEnum.TheSenderNumberIsNotValid.ToPersian(),
                "6" => SendMessageEnum.TheSystemIsBeingUpdated.ToPersian(),
                "7" => SendMessageEnum.TheTextContainsTheFilteredWord.ToPersian(),
                "11" => SendMessageEnum.FailedToSend.ToPersian(),
                _ => SendMessageEnum.TheRequestWasMadeSuccessfully.ToPersian()
            };

            result.Add(new RangeReciverIdMessage
            {
                Response = response,
                RecivedId = long.TryParse(val, out long conVal) ? conVal == 11 ? -1 : conVal : -1
            });
        }

        return result;
    }



    public long RecivedId =>
        long.TryParse(Value, out long val) ? val : -1;

}


public class RangeReciverIdMessage
{
    public string Response { get; set; }
    public long RecivedId { get; set; }
}