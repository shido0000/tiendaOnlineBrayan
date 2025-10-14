using API.Application.IoC;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddRegistration(configuration); ;
IoCRegister.AddLogsRegistration(builder);

var app = builder.Build();
// Habilitar wwwroot como carpeta pública
app.UseStaticFiles();

IoCRegister.AddRegistration(app, app.Environment);




