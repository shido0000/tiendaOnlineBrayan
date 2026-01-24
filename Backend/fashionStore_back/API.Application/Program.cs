using API.Application.IoC;
using API.Data.ClasesAuxiliares.Correo;
using API.Data.ClasesAuxiliares.WhatsApp;
using API.Domain.Services.NotificacionTiempoReal;
using API.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;

//var builder = WebApplication.CreateBuilder(args);


//// Add services to the container.
//ConfigurationManager configuration = builder.Configuration;


//builder.Services.AddRegistration(configuration); ;
//IoCRegister.AddLogsRegistration(builder);

//builder.Services.AddSignalR();


//var app = builder.Build();
//// Habilitar wwwroot como carpeta p�blica
//app.UseStaticFiles();

//// Startup.cs o Program.cs
//app.MapHub<PedidosHub>("/pedidosHub");


//IoCRegister.AddRegistration(app, app.Environment);

//builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

var builder = WebApplication.CreateBuilder(args);

// Habilitar Kestrel y leer endpoints de appsettings.json
builder.WebHost
    .UseKestrel()
    .UseConfiguration(builder.Configuration);

// Add services
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddRegistration(configuration);
IoCRegister.AddLogsRegistration(builder);
//builder.Services.AddSignalR();
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });


// Configurar CORS para SignalR
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", builder =>
    {
        builder
            .WithOrigins(
                "http://localhost:8080",
                "http://localhost:9000",
                "http://localhost:9001",
                "http://localhost:6004",
                "http://localhost:3000",
                "http://localhost:5173",
                "https://localhost:8080",
                "https://localhost:6005",
                "https://localhost:9000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // ¡IMPORTANTE PARA SIGNALR!
    });

});


// SMTP
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.Configure<WhatsAppOptions>(builder.Configuration.GetSection("WhatsApp"));


var app = builder.Build();
app.UseCors("SignalRPolicy");

//app.UseWebSockets();


// Archivos estáticos
app.UseStaticFiles();

//app.UseCors(builder =>
//    builder.AllowAnyOrigin()
//           .AllowAnyMethod()
//           .AllowAnyHeader());


// SignalR
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();
//app.MapHub<PedidosHub>("/pedidosHub");

app.Use(async (context, next) =>
{
    await next();

    // Si el resultado es 404 y no es una API ni un archivo estático
    if (context.Response.StatusCode == 404 &&
        !context.Request.Path.Value.StartsWith("/api") &&
        !System.IO.Path.HasExtension(context.Request.Path.Value))
    {
        context.Response.StatusCode = 200;
        await context.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "index.html"));
    }
});

// Rutas de tu API
IoCRegister.AddRegistration(app, app.Environment);


app.Run();



