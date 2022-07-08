namespace Farapayamak.Models.Responses;
public class GetUserNumberResponse
{
    public BaseResponse MyBase  { get; set; }

    public List<GetUserNumberDataResponse> Data { get; set; }
 
}

public class GetUserNumberDataResponse
{
    public string Number { get; set; }
}

 