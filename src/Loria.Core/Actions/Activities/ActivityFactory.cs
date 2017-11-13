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

        public void Perform(ActivityCommand command)
        {
            var activity = Items.FirstOrDefault(m => m.Action == command.Action);
            if (activity != null)
            {
                activity.Perform(command);
            }
        }
    }
}
