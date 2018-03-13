using Loria.Core;
using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using Loria.Core.Modules;
using System.Linq;

namespace Loria.Modules.Help
{
    public class HelpModule : Module, IActivity
    {
        public override string Name => "Help module";
        public override string Description => "It allows me to give informations about my loaded modules";
        public override string Keywords => "help";

        public string Action => "help";

        public string[] SupportedIntents => new string[0];
        public string[] Samples => new string[0];


        public HelpModule(Engine engine) 
            : base(engine)
        {
        }

        public override void Configure()
        {
            // Nothing to configure yet
            Activate();
        }

        public void Perform(ActivityCommand command, IModule sender)
        {
            var moduleAsked = command?.Intent?.ToLower();

            var activities = Engine.ModuleFactory.GetAll<IActivity>();
            var messengers = Engine.ModuleFactory.GetAll<IMessenger>();

            if (!string.IsNullOrEmpty(moduleAsked))
            {
                activities = activities.Where(a => a.Action.ToLower() == moduleAsked).ToList();
                messengers = messengers.Where(m => m.Action.ToLower() == moduleAsked).ToList();
            }
            
            activities.ForEach(a => Engine.Propagator.PropagateMessenger($"{a.Action} - {a.Description}", sender));
            messengers.ForEach(m => Engine.Propagator.PropagateMessenger($"{m.Action} - {m.Description}", sender));
        }
    }
}
