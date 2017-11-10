using Loria.Core.Actions;

namespace Loria.Core.Propagators
{
    public interface IPropagator
    {
        void Propagate(Command command);
    }
}
