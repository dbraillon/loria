using System.Linq;

namespace Loria.Core.Actions.Messengers
{
    public class MessengerCommand : Command
    {
        public string Message { get; set; }

        public MessengerCommand(string raw) : base(raw)
        {
            Message = string.Join(" ", Splitted.Skip(2));
        }

        public static bool IsRelated(string raw)
        {
            var command = new MessengerCommand(raw);
            return command.Type == "send";
        }
    }
}
