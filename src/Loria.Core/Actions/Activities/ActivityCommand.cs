using System.Collections.Generic;
using System.Linq;

namespace Loria.Core.Actions.Activities
{
    public class ActivityCommand : Command
    {
        public string Intent { get; set; }
        public Entity[] Entities { get; set; }

        public ActivityCommand(string raw) : base(raw)
        {
            // First, retrieve intent
            var separator = ' ';
            var commandSplitted = raw.Split(separator);
            Intent = commandSplitted.ElementAtOrDefault(2);

            // Then, retrieve entities
            var entitiesStr = commandSplitted.Skip(3);
            var entities = new List<Entity>();

            foreach (var entityStr in entitiesStr)
            {
                // If a part start with '-' symbol then its the name part
                // if not, its still the value part

                if (entityStr.StartsWith("-"))
                {
                    entities.Add(new Entity(entityStr.Substring(1), string.Empty));
                }
                else
                {
                    var lastEntity = entities.LastOrDefault();
                    if (lastEntity != null)
                    {
                        lastEntity.Value = $"{lastEntity.Value}{separator}{entityStr}".Trim();
                    }
                }
            }

            Entities = entities.ToArray();
        }

        public static bool IsRelated(string raw)
        {
            var command = new ActivityCommand(raw);
            return command.Type == "perform";
        }
    }
}
