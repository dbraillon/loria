using Loria.Core.Actions;
using Loria.Core.Modules;

namespace Loria.Core.Propagators
{
    public interface IPropagator
    {
        void Propagate(Command command, IModule sender);
    }
}
