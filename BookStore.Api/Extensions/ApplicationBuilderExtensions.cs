namespace BookStore.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    /// This method is used to configure the application pipeline for logging requests and responses.
    public static IApplicationBuilder UseSimpleLogging(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var path = context.Request.Path;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var logFilePath = Path.Combine(logDirectory, "RequestLog.txt");

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            var logStart = $"---- Request Start ----{Environment.NewLine}" +
                           $"[{timestamp}] Request started: {path}{Environment.NewLine}";

            await File.AppendAllTextAsync(logFilePath, logStart);

            await next();

            var logEnd = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Request ended:   {path}{Environment.NewLine}" +
                         $"---- Request End ----{Environment.NewLine}";

            await File.AppendAllTextAsync(logFilePath, logEnd);
        });
    }

    /// This method is used to configure the application pipeline for handling exceptions.
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API v1");
            c.RoutePrefix = string.Empty;
        });

        return app;
    }
}