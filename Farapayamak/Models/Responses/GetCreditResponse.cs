namespace Farapayamak.Models.Responses;

public class GetCreditResponse : BaseResponse
{
    public override string Response => string.IsNullOrEmpty(Value) ? "0" : Value;
}
 