using GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthetication();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();

        });
});


//if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
//{
//    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
//}
//else
//{
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//}
builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
//app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
await app.RunAsync();
