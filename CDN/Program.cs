using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Logging;
using System.Runtime;
using Cord.CDN;
using Cord.CDN.Repository;
using Rikarin.Runner;

var builder = WebApplication.CreateBuilder(args);
GCSettings.LatencyMode = GCLatencyMode.LowLatency;

builder.AddConfig();
builder.AddLogger();
builder.InitKestrel();

builder.Services.Configure<KestrelServerOptions>(options => {
    options.AllowSynchronousIO = true;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
});

builder.AddHealthChecks(true, false);
builder.AddControllers();
// builder.AddCorsPolicy();

// builder.Services.AddMediatR(typeof(MemberAddedHandler));
builder.Services.AddOpenApiDocument();

builder.InitDatabase();
builder.AddMetrics("cord_cdn");

var app = builder.Build();

app.UseMetricsAllMiddleware();
app.UseMetricsAllEndpoints();
app.MapHealthChecks("/healthz");

// Documentation
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseReDoc();

IdentityModelEventSource.ShowPII = true;

app.UseRouting();
app.UseExceptionHandler("/error");
// app.UseCors("CorsPolicy");
app.UseDomainEvents();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});  

foreach (var dir in ImageService.Resources) {
    Directory.CreateDirectory($"../resources/{dir}");
}

app.Run();
