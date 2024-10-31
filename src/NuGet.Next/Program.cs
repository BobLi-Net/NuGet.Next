using NuGet.Next.Extensions;
using NuGet.Next.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNuGetNext(builder.Configuration);

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

app.MapApis();

app.Run();