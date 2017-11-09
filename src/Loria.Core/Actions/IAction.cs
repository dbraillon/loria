using Loria.Core.Modules;

namespace Loria.Core.Actions
{
    public interface IAction : IModule
    {
        string Action { get; }
    }
}
