using HandmadeShop.Common.Settings;
using HandmadeShop.Context;
using HandmadeShop.Identity;
using HandmadeShop.Identity.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var logSettings = Settings.Load<LogSettings>(LogSettings.SectionName);
builder.AddAppLogger(logSettings);

services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.RegisterAppServices();

services.AddIS4();

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseIS4();

app.Run();