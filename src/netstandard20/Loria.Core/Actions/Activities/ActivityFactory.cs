using Loria.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loria.Core.Actions.Activities
{
    public class ActivityFactory
    {
        public Engine Engine { get; set; }
        public List<IActivity> Items { get; set; }

        public ActivityFactory(Engine engine)
        {
            Engine = engine;
            Items = Engine.ModuleFactory.GetAll<IActivity>();
        }

        public void Perform(ActivityCommand command, IModule sender)
        {
            var activity = Get(command.Action);
            if (activity != null)
            {
                activity.Perform(command, sender);
            }
        }

        public IActivity Get(string action)
        {
            return Items.FirstOrDefault(i => string.Equals(i.Action, action, StringComparison.OrdinalIgnoreCase));
        }
    }
}
