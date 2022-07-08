using Microsoft.AspNetCore.Mvc;

namespace Farapayamak.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SMSController : ControllerBase
    {

        private readonly IFarapayamakService _farapayamakService;

        public SMSController(IFarapayamakService farapayamakService)
        {
            _farapayamakService = farapayamakService;

             
        } 

        public async Task<IActionResult> Get()
        {
            var outboxResult = await _farapayamakService.SendRangeAsync(new List<string>
            {
                "09127706148","0912056610","314215234"
            },"hi nabi");
            return Ok();
        }

  
    }
}