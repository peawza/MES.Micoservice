var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");


var apiServiceEmail = builder.AddProject<Projects.Service_Email>("ServiceEmail");
var apiServiceCronjob = builder.AddProject<Projects.Service_CronJob>("ServiceCronJob");


builder.AddProject<Projects.WEB_APP>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiServiceEmail)
    .WithReference(apiServiceCronjob);


builder.Build().Run();
