using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmailSender.Application.IoC;
using EmailSender.Infrastructure.Configurations;
using EmailSender.Infrastructure.IoC;
using EmailSender.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", builder =>
    {
        builder.WithOrigins(new string[] { "http://localhost:8080", "https://email-sender-fnt.herokuapp.com" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

builder.Services.AddControllers();

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

app.UseAuthentication();

app.MapGet("/", () => "Hello World!");

app.Run();
