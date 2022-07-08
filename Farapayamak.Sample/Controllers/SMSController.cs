using Microsoft.AspNetCore.Mvc;

namespace Farapayamak.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SMSController : ControllerBase
    {

        private readonly IFarapayamakService _smsService;

        public SMSController(IFarapayamakService smsService)
        {
            _smsService = smsService;
        }

        public async Task PanelInfo()
        {
            var accCredit = await _smsService.GetCredit();
            // Result 
            // accCredit.IsSuccess      (bool)
            // accCredit.Credit         (decimal)

            var accBasePrice = await _smsService.GetBasePrice();
            // Result 
            // accBasePrice.IsSuccess   (bool)
            // accBasePrice.BasePrice   (decimal)

            var accBalance = await _smsService.GetAccountBalance();
            // Result 
            // accBalance.IsSuccess     (bool)
            // accBalance.Balance       (decimal)

            var userNumbers = await _smsService.GetUserNumbers();

            // Result 
            // userNumbers.IsSuccess     (bool)
            // userNumbers.Response      (string) action message
            // userNumbers.Numbers       (list of string)
        }

        public async Task SendSingleMessage()
        {
            // use defualt panel number
            var sendResult = await _smsService.SendAsync("09127706148","Hi dear [thisisnabi]");
             
            // use custom panel number [xxx is your panel number]
            //var sendResult = await _smsService.SendAsync("xxx","09127706148", "Hi dear [thisisnabi]");

            // Result
            // sendResult.IsSuccess     (bool)
            // sendResult.Response      (string) action message
            // sendResult.RecivedId     (long) if send was a failure, you get -1.
        }


        public async Task SendRangeMessage()
        {
            List<string> recivers = new() {
                "09127706148",
                "09120000000",
                "12312313452"
            };

            // use defualt panel number
            var sendResult = await _smsService.SendRangeAsync(recivers, "Hi dear [---]");
             
            // use custom panel number [xxx is your panel number]
            //var sendResult = await _smsService.SendRangeAsync("xxx", recivers, "Hi dear [---]");

            // Result
            // sendResult.IsSuccess     (bool)
            // sendResult.Status        (list of records) List<(string number,string response,long recivedId)> 
            // sendResult.Status.Item
                     // Item.IsSuccess     (bool)
                     // Item.Response      (string) action message
                     // Item.RecivedId     (long) if send was a failure, you get -1.
        }


        public async Task GetInboxOutBoxMessages()
        { 
            // use index 0, you can pass index
            var inboxMessages = await _smsService.GetInboxMessagesAsync();
            // Result
            // inboxMessages.IsSuccess     (bool)
            // inboxMessages.Response      (string) action message
            // inboxMessages.Messages      (list of MessageItemModel)

            // use index 0, you can pass index
            var outboxMessages = await _smsService.GetOutboxMessagesAsync();
            // Result
            // outboxMessages.IsSuccess     (bool)
            // outboxMessages.Response      (string) action message
            // outboxMessages.Messages      (list of MessageItemModel)
        }



        public async Task GetMessageStatus()
        {
            // use defualt panel number
            var sendResult = await _smsService.SendAsync("09127706148", "Hi dear [thisisnabi]");

            // Result
            // sendResult.IsSuccess     (bool)
            // sendResult.Response      (string) action message
            // sendResult.RecivedId     (long) if send was a failure, you get -1.

            if (sendResult.IsSuccess)
            {
                var msgStatus = await _smsService.GetMessageStatusAsync(sendResult.RecivedId);
                // msgStatus.IsSuccess     (bool)
                // msgStatus.Response      (string) action message
            }
        }
    }
}