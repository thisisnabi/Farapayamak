namespace Farapayamak.Models.Responses
{
    public abstract class BaseResponse
    {
        public string Value { get; set; }
        public int RetStatus { get; set; }

        public string StrRetStatus { get; set; }
         
        public bool IsSuccess => RetStatus == 1;
    }
}
