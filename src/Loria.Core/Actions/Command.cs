using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using System.Linq;

namespace Loria.Core.Actions
{
    public abstract class Command
    {
        public string Raw { get; set; }
        public string[] Splitted { get; set; }

        public string Type { get; set; }
        public string Action { get; set; }

        protected Command(string raw)
        {
            Raw = raw;
            Splitted = Raw.Split(' ');

            Type = Splitted.ElementAtOrDefault(0);
            Action = Splitted.ElementAtOrDefault(1);
        }

        public static Command Parse(string raw)
        {
            if (ActivityCommand.IsRelated(raw))
                return new ActivityCommand(raw);

            if (MessengerCommand.IsRelated(raw))
                return new MessengerCommand(raw);
            
            return null;
        }
    }
}
