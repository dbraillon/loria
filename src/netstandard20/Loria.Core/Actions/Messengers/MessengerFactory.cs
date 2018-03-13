using Loria.Core.Modules;
using System;
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

        public void Perform(MessengerCommand command, IModule sender)
        {
            var messenger = Get(command.Action);
            if (messenger != null)
            {
                messenger.Perform(command, sender);
            }
        }
        
        public IMessenger Get(string action)
        {
            return Items.FirstOrDefault(i => string.Equals(i.Action, action, StringComparison.OrdinalIgnoreCase));
        }
    }
}
