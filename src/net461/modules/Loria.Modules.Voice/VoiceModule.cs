using Loria.Core;
using Loria.Core.Modules;

namespace Loria.Modules.Voice
{
    public class VoiceModule : Module
    {
        public override string Name => "Voice module";
        public override string Description => "It allows me to speak with you with a digital voice";

        public VoiceModule(Engine engine) 
            : base(engine)
        {
        }

        public override void Configure()
        {
            // Nothing to configure yet
            Activate();
        }
    }
}
