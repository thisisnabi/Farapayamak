using Farapayamak;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFarapayamakSMSProvider(options => {
    options.Username = "#######";
    options.Password = "#######";
    options.DefaultNumber = "#######";
    options.UseDefaultIsFlash = false; // default false
    options.MaxReciveMessageCount = 100; // default 50
});
 

builder.Services.AddControllers();

var app = builder.Build();

 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
