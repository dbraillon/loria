using NuGet.Packaging;
using NuGet.ProjectManagement;
using System.Diagnostics;
using System.Xml.Linq;

namespace Loria.Modules.NuGet
{
    public class ProjectContext : INuGetProjectContext
    {
        public void Log(MessageLevel level, string message, params object[] args)
        {
            if (level != MessageLevel.Debug)
            {
                Debug.WriteLine(message, args);
            }
        }

        public FileConflictAction ResolveFileConflict(string message) => FileConflictAction.Ignore;

        public PackageExtractionContext PackageExtractionContext { get; set; }

        public XDocument OriginalPackagesConfig { get; set; }

        public ISourceControlManagerProvider SourceControlManagerProvider => null;

        public ExecutionContext ExecutionContext => null;

        public void ReportError(string message)
        {
        }

        public NuGetActionType ActionType { get; set; }
        public TelemetryServiceHelper TelemetryService { get; set; }
    }
}
