using NuGet.Common;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Loria.Modules.NuGet
{
    public class Logger : ILogger
    {
        public void Log(LogLevel level, string data) => Debug.WriteLine(data);
        public void Log(ILogMessage message) => Debug.WriteLine(message);

        public async Task LogAsync(LogLevel level, string data) => Debug.WriteLine(data);
        public async Task LogAsync(ILogMessage message) => Debug.WriteLine(message);

        public void LogDebug(string data) => Debug.WriteLine(data);
        public void LogError(string data) => Debug.WriteLine(data);
        public void LogInformation(string data) => Debug.WriteLine(data);
        public void LogInformationSummary(string data) => Debug.WriteLine(data);
        public void LogMinimal(string data) => Debug.WriteLine(data);
        public void LogVerbose(string data) => Debug.WriteLine(data);
        public void LogWarning(string data) => Debug.WriteLine(data);
    }
}
