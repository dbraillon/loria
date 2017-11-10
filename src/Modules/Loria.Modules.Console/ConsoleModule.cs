using Loria.Core;
using Loria.Core.Actions;
using Loria.Core.Actions.Messengers;
using Loria.Core.Listeners;

namespace Loria.Modules.Console
{
    public class ConsoleModule : Listener, IMessenger
    {
        public override string Name => "Console module";
        public override string Description => "It allows me to see what you type in console and to answer you there";

        public string Action => "console";

        public ConsoleModule(Engine engine) 
            : base(engine, 1)
        {
        }

        public override void Configure()
        {
            // Nothing to configure yet
            Activate();
        }

        public void Perform(MessengerCommand command)
        {
            System.Console.WriteLine(command.Message);
        }

        public override Command Listen()
        {
            return Command.Parse(System.Console.ReadLine());
        }
    }
}
