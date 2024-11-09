using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using NuGet.Next;
using NuGet.Next.Converters;
using NuGet.Next.Extensions;
using NuGet.Next.Middlewares;
using NuGet.Next.Service;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);


var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
});

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    builder.Services.AddWindowsService(options => { options.ServiceName = "NuGetNext"; });
}

builder.Services.AddControllers();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonDateTimeConverter());
    options.SerializerOptions.Converters.Add(new JsonDateTimeOffsetConverter());
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddNuGetNext(builder.Configuration);
builder.Services.AddResponseCompression();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrateDatabase();

app.Configure(builder.Environment, builder.Configuration);

app.UseMiddleware<ExceptionMiddleware>();

app.UseResponseCompression();

app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    // Add BaGet's endpoints.
    var baget = new NuGetNextEndpointBuilder();

    baget.MapEndpoints(endpoints);
    
});

app.MapApis();

app.Run();