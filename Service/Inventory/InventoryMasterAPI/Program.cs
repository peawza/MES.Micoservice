using InventoryReportAPI;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Utils.Startup.ConfigConstants(builder, typeof(Authentication.Constants.AUTH));
AuthenticationBuilder auth = Utils.Startup.ConfigAuthentication(builder);
Utils.Startup.ConfigRequestSize(builder);
Utils.Startup.ConfigCors(builder);
Utils.Startup.ConfigController(builder);
Utils.Startup.ConfigUtils(builder);
Utils.Startup.ConfigSwagger(builder);
/* --- Add Repository & Service ---*/
StartupService.InitialService(builder.Services);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
    });










builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//builder.Services.AddAuthorization(AuthorizePolicy.SetPolicy);

var app = builder.Build();

Utils.Startup.UseCors(app);
Utils.Startup.UseSwagger(builder, app);
Utils.Startup.UseForwardedHeaders(app);

// app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

