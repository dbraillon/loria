using Loria.Core.Actions;
using Loria.Core.Modules;
using System.Threading;

namespace Loria.Core.Listeners
{
    public abstract class Listener : Module, IListener
    {
        public int MillisecondsSleep { get; }
        public Thread Thread { get; }
        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }

        public Listener(Engine engine, int millisecondsSleep)
            : base (engine)
        {
            MillisecondsSleep = millisecondsSleep;
            Thread = new Thread(Loop);
            IsRunning = false;
            IsPaused = false;
        }

        public virtual void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Thread.Start();
            }
        }

        public virtual void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Thread.Interrupt();
                Thread.Join();
            }
        }

        public virtual void Pause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
            }
        }

        public virtual void Resume()
        {
            if (IsPaused)
            {
                IsPaused = false;
            }
        }

        public virtual void Loop()
        {
            while (IsRunning)
            {
                if (!IsPaused)
                {
                    var result = Listen();
                    if (result != null)
                    {
                        Engine.Propagator.Propagate(result, this);
                    }
                }

                Thread.Sleep(MillisecondsSleep);
            }
        }

        public abstract Command Listen();
    }
}
