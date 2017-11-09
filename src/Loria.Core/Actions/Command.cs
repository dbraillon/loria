using System.Linq;

namespace Loria.Core.Actions
{
    public abstract class Command
    {
        public string Raw { get; set; }
        public string[] Splitted { get; set; }

        public string Type { get; set; }
        public string Action { get; set; }

        public Command(string raw)
        {
            Raw = raw;
            Splitted = Raw.Split(' ');

            Type = Splitted.ElementAtOrDefault(0);
            Action = Splitted.ElementAtOrDefault(1);
        }
    }
}
