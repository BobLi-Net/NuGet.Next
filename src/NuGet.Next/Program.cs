using System.Text.Json.Serialization;
using NuGet.Next.Converters;
using NuGet.Next.Extensions;
using NuGet.Next.Middlewares;
using NuGet.Next.Service;

var builder = WebApplication.CreateBuilder(args);

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

app.Configure(builder.Environment, builder.Configuration);

app.UseMiddleware<ExceptionMiddleware>();

app.UseResponseCompression();

app.UseStaticFiles();

app.MapApis();

app.Run();