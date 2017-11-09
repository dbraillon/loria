using Loria.Core.Actions.Messengers;
using Loria.Core.Modules;
using System.Threading;

namespace Loria.Core
{
    public class Engine
    {
        public bool IsLiving { get; private set; }

        public ModuleFactory ModuleFactory { get; set; }
        public MessengerFactory MessengerFactory { get; set; }

        public Engine()
        {
            ModuleFactory = new ModuleFactory(this);
            MessengerFactory = new MessengerFactory(this);
        }

        public void Live()
        {
            LiveAsync();
            
            while (IsLiving) Thread.Sleep(1000);
        }
        public void LiveAsync()
        {
            IsLiving = true;
        }

        public void Stop()
        {
            IsLiving = false;
        }
    }
}
