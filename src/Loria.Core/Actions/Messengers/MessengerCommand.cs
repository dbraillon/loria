namespace Loria.Core.Actions.Messengers
{
    public class MessengerCommand : Command
    {
        public string Message { get; set; }

        public MessengerCommand(string action, string message) 
            : base(CommandBuilder.MessengerKeyword, action)
        {
            Message = message;
        }
    }
}
