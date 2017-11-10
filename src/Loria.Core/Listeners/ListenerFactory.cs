using System.Collections.Generic;

namespace Loria.Core.Listeners
{
    public class ListenerFactory
    {
        public Engine Engine { get; set; }
        public List<IListener> Items { get; set; }

        public ListenerFactory(Engine engine)
        {
            Engine = engine;
            Items = Engine.ModuleFactory.GetAll<IListener>();
        }

        public void StartAll()
        {
            foreach (var listener in Items)
            {
                listener.Start();
            }
        }

        public void StopAll()
        {
            foreach (var listener in Items)
            {
                listener.Stop();
            }
        }

        public void PauseAll()
        {
            foreach (var listener in Items)
            {
                listener.Pause();
            }
        }

        public void ResumeAll()
        {
            foreach (var listener in Items)
            {
                listener.Resume();
            }
        }
    }
}
