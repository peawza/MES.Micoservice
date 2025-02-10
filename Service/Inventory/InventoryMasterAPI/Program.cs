using InventoryMasterAPI;
using InventoryMasterPostgreSQLDB;
using Microsoft.EntityFrameworkCore;
using Utils;

var builder = WebApplication.CreateBuilder(args);


/* 
 --- DB Conneted Config---
 
 
 */
var connectionString =
    builder.Configuration.GetConnectionString("InventoryMasterDBConnection");

//builder.Services.AddDbContext<InventoryMasterDbContext>(options =>
//    options.UseNpgsql(connectionString));


builder.Services.AddDbContext<InventoryMasterDbContext>(options =>
    options.UseNpgsql(connectionString,
        b => b.MigrationsAssembly("InventoryMasterAPI")));


/* --- Add Repository & Service ---*/
StartupService.InitialService(builder.Services);

/* ---Add message bus Or message broker --- */


/* ---Add gRPC --- */


StartupAPIMicoService.StartupCreateBuilder(builder);

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();


StartupAPIMicoService.StartupCreateApplication(builder, app);
app.Run();
