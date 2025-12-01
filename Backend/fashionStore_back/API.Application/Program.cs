using API.Application.IoC;
using API.Data.ClasesAuxiliares.Correo;
using API.Domain.Services.NotificacionTiempoReal;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddRegistration(configuration); ;
IoCRegister.AddLogsRegistration(builder);

builder.Services.AddSignalR();


var app = builder.Build();
// Habilitar wwwroot como carpeta pública
app.UseStaticFiles();

// Startup.cs o Program.cs
app.MapHub<PedidosHub>("/pedidosHub");


IoCRegister.AddRegistration(app, app.Environment);

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));


