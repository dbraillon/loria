using System.Collections.Generic;
using System.Linq;

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

        public void Perform(MessengerCommand command)
        {
            var messenger = Items.FirstOrDefault(m => m.Action == command.Action);
            if (messenger != null)
            {
                messenger.Perform(command);
            }
        }
    }
}
