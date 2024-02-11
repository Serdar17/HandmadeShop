using HandmadeShop.Common.Settings;
using HandmadeShop.Services.Logger.Logger;
using HandmadeShop.Worker;
using HandmadeShop.Worker.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var logSettings = Settings.Load<LogSettings>(LogSettings.SectionName);
builder.AddAppLogger(logSettings);

services.AddHttpContextAccessor();

services.AddAppHealthChecks();

services.RegisterAppServices();


var app = builder.Build();


var logger = app.Services.GetRequiredService<IAppLogger>();


app.UseAppHealthChecks();


logger.Information("Worker has started");


// Start task executor

logger.Information("Try to connect to RabbitMq");

app.Services.GetRequiredService<ITaskExecutor>().Start();

logger.Information("RabbitMq connected");


// Run app

logger.Information("Worker started");

app.Run();

logger.Information("Worker has stopped");
