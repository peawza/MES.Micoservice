var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");


var apiServiceEmail = builder.AddProject<Projects.Service_Email>("ServiceEmail");
var apiServiceCronjob = builder.AddProject<Projects.Service_CronJob>("ServiceCronJob");


// เพิ่ม Services ใน Aspire
var apiAuth = builder.AddProject<Projects.Authentication>("ServiceAuth");
var apiInventoryMaster = builder.AddProject<Projects.InventoryMasterAPI>("ServiceInventoryMaster");
var apiInventoryOperation = builder.AddProject<Projects.InventoryOperationAPI>("ServiceInventoryOperation");
var apiInventoryReport = builder.AddProject<Projects.InventoryReportAPI>("ServiceInventoryReport");

//*note Rename
/*
var apiMaintenanceMaster = builder.AddProject<Projects.ProductionMasterAPI>("ServiceMaintenanceMaster");
var apiMaintenanceOperation = builder.AddProject<Projects.ProductionOperationAPI>("ServiceMaintenanceOperation");
var apiMaintenanceReport = builder.AddProject<Projects.ProductionReportAPI>("ServiceMaintenanceReport");
*/
//*note Rename

var apiProductionMaster = builder.AddProject<Projects.ProductionMasterAPI>("ServiceProductionMaster");
var apiProductionOperation = builder.AddProject<Projects.ProductionOperationAPI>("ServiceProductionOperation");
var apiProductionReport = builder.AddProject<Projects.ProductionReportAPI>("ServiceProductionReport");
var apiQualityMaster = builder.AddProject<Projects.QualityMasterAPI>("ServiceQualityMaster");
var apiQualityOperation = builder.AddProject<Projects.QualityOperationAPI>("ServiceQualityOperation");
var apiQualityReport = builder.AddProject<Projects.QualityReportAPI>("ServiceQualityReport");



builder.AddProject<Projects.WEB_APP>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiServiceEmail)
    .WithReference(apiServiceCronjob);


builder.Build().Run();
