using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmailSender.Application.IoC;
using EmailSender.Infrastructure.Configurations;
using EmailSender.Infrastructure.IoC;
using EmailSender.IoC;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", builder =>
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyOrigin()
                .WithOrigins("http://localhost:8080", "https://email-sender-fnt.herokuapp.com");
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModules()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ApplicationModules()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new InfrastructureModules()));

builder.Services.Configure<SmtpConfiguration>(builder.Configuration.GetSection("Smtp"));
builder.Services.Configure<AesConfiguration>(builder.Configuration.GetSection("Aes"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("cors");

app.UseAuthorization();

app.MapControllers();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.MapGet("/", () => "Hello World!");

app.Run();
