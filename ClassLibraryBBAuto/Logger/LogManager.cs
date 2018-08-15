using Serilog;

namespace BBAuto.Domain.Logger
{
  public static class LogManager
  {
    public static ILogger Logger { get; } = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.ColoredConsole()
      .WriteTo.RollingFile(@"\\bbmru08\Programs\Utility\BBAuto\Log\{Date}.txt")
      .CreateLogger();
  }
}
