namespace Api.Extensions
{
    public static class LoggerExtension
    {
        public static ILoggingBuilder Configuration(this ILoggingBuilder logger, WebApplicationBuilder builder)
        {
            logger.AddConfiguration(builder.Configuration.GetSection("Logging"));
            logger.AddConsole();
            logger.AddDebug();
            logger.AddEventSourceLogger();

            return logger;
        }
    }
}
