 namespace Farapayamak.Models.Responses;

public class BaseResponse
{
    public string Value { get; set; }
    public int RetStatus { get; set; }

    public string StrRetStatus { get; set; }
     
    public bool IsSuccess => RetStatus == 1;
 
    public virtual string Response => IsSuccess ? "عملیات با موفقیت انجام شد." : "خطا در انجام عملیات.";
}
 