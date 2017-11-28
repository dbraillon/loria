using System;
using System.Linq;

namespace Loria.Core.Actions.Activities
{
    public class ActivityCommand : Command
    {
        public string Intent { get; set; }
        public Entity[] Entities { get; set; }

        public ActivityCommand(string action, string intent, params Entity[] entities) 
            : base(CommandBuilder.ActivityKeyword, action)
        {
            Intent = intent;
            Entities = entities;
        }

        public Entity GetEntity(string name)
        {
            return Entities?.FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
