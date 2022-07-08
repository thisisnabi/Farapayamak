
# Farapayamak

Farapayamak is an Iranian SMS Provider, and this package is a helper service for easy use. So you can use it on .Net Core Projects and use DI with custom configuration.


> First, you must create base account on [farapayamak.ir](https://farapayamak.ir/)

## Authors

- [@thisisnabi](https://www.github.com/thisisnabi)




## Features

- Send single and multiple SMS at time.
- GetMessages from Inbox and Outbox
- Get Credit and balance account
- DI (IFarapayamakService)


## Install

Install with Package Manager Console  

```bash
  Install-Package Farapayamak
```
 
## Add/DI
 
```csharp
// you must set configuration when add Service in 'Startup' Class
public void ConfigureServices(IServiceCollection services)
{
    services.AddVerificoder(options => {
         options.Username = "nilesoft.ir";
         options.Password = "lakejrf!#$RASF@";
         options.DefaultNumber = "500012701212122323"; // your number in Farapayamak Panel
         options.UseDefaultIsFlash = false; // default false
         options.MaxReciveMessageCount = 100; // default 50, when you need get sms from panel inbox or outbox
    });
}
```


## Panel/Info
```csharp
public class AccountController : Controller
{

    private readonly IFarapayamakService _smsService;
    public AccountController(IFarapayamakService smsService)
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
}
```

## Send Single Message
```csharp
public async Task SendSingleMessage()
{
       // use defualt panel number, you setting up a configuration Time
       var sendResult = await _smsService.SendAsync("09127706148","Hi dear [thisisnabi]");
             
       // use custom panel number [xxx is your panel number]
       var sendResult = await _smsService.SendAsync("xxx","09127706148", "Hi dear [thisisnabi]");

       // Result
       // sendResult.IsSuccess     (bool)
       // sendResult.Response      (string) action message
       // sendResult.RecivedId     (long) if send was a failure, you get -1.
}
```


## Send Range Message
```csharp
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
       var sendResult = await _smsService.SendRangeAsync("xxx", recivers, "Hi dear [---]");

       // Result
       // sendResult.IsSuccess     (bool)
       // sendResult.Status        (list of records) List<(string number,string response,long recivedId)> 
       // sendResult.Status.Item
              // Item.IsSuccess     (bool)
              // Item.Response      (string) action message
              // Item.RecivedId     (long) if send was a failure, you get -1.
}
```
