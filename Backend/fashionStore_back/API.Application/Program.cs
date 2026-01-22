using API.Application.IoC;
using API.Data.ClasesAuxiliares.Correo;
using API.Domain.Services.NotificacionTiempoReal;
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
builder.Services.AddSignalR();

var app = builder.Build();

// Archivos estáticos
app.UseStaticFiles();

// SignalR
app.MapHub<PedidosHub>("/pedidosHub");

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

// SMTP
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

app.Run();



