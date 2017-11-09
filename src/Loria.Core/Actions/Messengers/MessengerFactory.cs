using System.Collections.Generic;

namespace Loria.Core.Actions.Messengers
{
    public class MessengerFactory
    {
        public Engine Engine { get; set; }
        public List<IMessenger> Items { get; set; }

        public MessengerFactory(Engine engine)
        {
            Engine = engine;
            Items = Engine.ModuleFactory.GetAll<IMessenger>();
        }
    }
}
