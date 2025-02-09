using InventoryReportAPI;
using Utils;


var builder = WebApplication.CreateBuilder(args);


/* 
 --- DB Conneted Config---
 
 
 */



/* --- Add Repository & Service ---*/
StartupService.InitialService(builder.Services);

/* ---Add message bus Or message broker --- */


/* ---Add gRPC --- */


StartupAPIMicoService.StartupCreateBuilder(builder);

var app = builder.Build();


StartupAPIMicoService.StartupCreateApplication(builder, app);
app.Run();
