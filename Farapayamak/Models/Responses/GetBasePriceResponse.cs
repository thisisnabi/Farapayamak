namespace Farapayamak.Models.Responses;

public class GetBasePriceResponse : BaseResponse
{
    public override string Response => string.IsNullOrEmpty(Value) ? "0" : Value;
}
 