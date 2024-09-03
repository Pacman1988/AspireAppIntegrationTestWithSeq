var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq").ExcludeFromManifest();

var apiService = builder.AddProject<Projects.AspireApp1_ApiService>("apiservice")
    .WithReference(seq);

builder.AddProject<Projects.AspireApp1_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
