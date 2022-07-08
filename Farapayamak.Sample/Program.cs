using Farapayamak;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFarapayamakSMSProvider(options => {
    options.Username = "###";
    options.Password = "###";
    options.DefaultNumber = "###";
});

 

builder.Services.AddControllers();

var app = builder.Build();

 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
