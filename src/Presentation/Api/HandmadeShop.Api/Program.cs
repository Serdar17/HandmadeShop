using HandmadeShop.Api;
using HandmadeShop.Api.Configuration;
using HandmadeShop.Common.Settings;
using HandmadeShop.Context;
using HandmadeShop.Context.Setup;
using HandmadeShop.Services.Logger.Logger;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var mainSettings = Settings.Load<MainSettings>(MainSettings.SectionName);
var logSettings = Settings.Load<LogSettings>(LogSettings.SectionName);
var swaggerSettings = Settings.Load<SwaggerSettings>(SwaggerSettings.SectionName);
var identitySettings = Settings.Load<IdentitySettings>(IdentitySettings.SectionName);

builder.AddAppLogger(mainSettings, logSettings);

services.AddHttpContextAccessor()
    .AddAppDbContext(builder.Configuration)
    .AddAppCors()
    .AddAppSwagger(mainSettings, swaggerSettings, identitySettings)
    .AddAppValidator()
    .AddAppAuth(identitySettings)
    .AddAppControllerAndViews();

services.RegisterServices(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();
app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAppAuth();

app.UseAppControllerAndViews();

app.UseAppMiddlewares();

DbInitializer.Execute(app.Services);
//
// DbSeeder.Execute(app.Services);

logger.Information("The HandmadeShop.API has started");

app.Run();

logger.Information("The HandmadeShop.API has started");

