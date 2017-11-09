using Loria.Core.Modules;

namespace Loria.Modules.Console
{
    public class ConsoleModule : Module
    {
        public override string Name => "Console module";
        public override string Description => "It allows me to see what you type in console and to answer you there";

        public override void Configure()
        {
            // Nothing to configure yet
            Activate();
        }
    }
}
