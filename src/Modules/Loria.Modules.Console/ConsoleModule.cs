using Loria.Core;
using Loria.Core.Actions.Messengers;
using Loria.Core.Modules;

namespace Loria.Modules.Console
{
    public class ConsoleModule : Module, IMessenger
    {
        public override string Name => "Console module";
        public override string Description => "It allows me to see what you type in console and to answer you there";

        public string Action => "console";

        public ConsoleModule(Engine engine) 
            : base(engine)
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
    }
}
