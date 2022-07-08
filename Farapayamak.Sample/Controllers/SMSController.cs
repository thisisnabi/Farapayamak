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
            var result =await _farapayamakService.SendSMSAsync("234234234234","09127706148", "salam");


            return Ok();
        }

  
    }
}