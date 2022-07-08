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
            var bala = await _farapayamakService.GetUserNumbers();
 

            return Ok();
        }

  
    }
}