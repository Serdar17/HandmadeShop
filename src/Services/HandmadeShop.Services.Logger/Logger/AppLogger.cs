using Serilog.Events;

namespace HandmadeShop.Services.Logger.Logger;

public class AppLogger : IAppLogger
{
    private readonly Serilog.ILogger _logger;

    private const string SystemName = "HandmadeShop";

    public AppLogger(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    private string ConstructMessageTemplate(string messageTemplate, object? module = null)
    {
        var moduleName = module?.GetType().Name;

        if (string.IsNullOrEmpty(moduleName))
            return $"[{SystemName}] {messageTemplate} ";
        
        return $"[{SystemName}:{module}] {messageTemplate} ";
    }

    public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Write(level, ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Write(LogEventLevel level, object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Write(level, ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Write(LogEventLevel level, Exception exception, string messageTemplate,
        params object[] propertyValues)
    {
        _logger?.Write(level, exception, ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Write(LogEventLevel level, Exception exception, object module, string messageTemplate,
        params object[] propertyValues)
    {
        _logger?.Write(level, exception, ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Verbose(string messageTemplate, params object[] propertyValues)
    {
        _logger?.Verbose(ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Verbose(object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Verbose(ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        _logger?.Debug(ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Debug(object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Debug(ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Information(string messageTemplate, params object[] propertyValues)
    {
        _logger?.Information(ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Information(object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Information(ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        _logger?.Warning(ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Warning(object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Warning(ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Error(string messageTemplate, params object[] propertyValues)
    {
        _logger?.Error(ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Error(object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Error(ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Error(exception, ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Error(Exception exception, object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Error(exception, ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Fatal(string messageTemplate, params object[] propertyValues)
    {
        _logger?.Fatal(ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Fatal(object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Fatal(ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Fatal(exception, ConstructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Fatal(Exception exception, object module, string messageTemplate, params object[] propertyValues)
    {
        _logger?.Fatal(exception, ConstructMessageTemplate(messageTemplate, module), propertyValues);
    }
}