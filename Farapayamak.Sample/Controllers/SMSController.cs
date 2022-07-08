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
            
            var inboxResult = await _farapayamakService.GetInboxMessagesAsync();
            var outboxResult = await _farapayamakService.GetOutboxMessagesAsync();
            return Ok();
        }

  
    }
}