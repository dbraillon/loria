using Loria.Core.Modules;

namespace Loria.Core.Listeners
{
    public interface IListener : IModule
    {
        void Start();
        void Stop();
        void Pause();
        void Resume();
    }
}
