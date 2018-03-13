using Loria.Core.Actions;
using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using Loria.Core.Modules;
using System.Linq;

namespace Loria.Core.Propagators
{
    public class Propagator : IPropagator
    {
        public Engine Engine { get; set; }

        public Propagator(Engine engine)
        {
            Engine = engine;
        }

        public void Propagate(Command command, IModule sender)
        {
            if (command == null) return;

            if (command is ActivityCommand)
                PropagateActivity(command as ActivityCommand, sender);

            if (command is MessengerCommand)
                PropagateMessenger(command as MessengerCommand, sender);
        }
        
        public void PropagateActivity(ActivityCommand command, IModule sender) => Engine.ActivityFactory.Perform(command, sender);

        public void PropagateMessenger(MessengerCommand command, IModule sender) => Engine.MessengerFactory.Perform(command, sender);
        public void PropagateMessenger(string message, IModule sender)
        {
            var senderKeywords = sender.Keywords?.Split(' ') ?? new string[0];

            var messengers = Engine.MessengerFactory.Items;
            var messenger = messengers
                .Select(m =>
                {
                    var keywords = m.Keywords?.Split(' ') ?? new string[0];
                    var count = keywords.Intersect(senderKeywords).Count();

                    return new { Messenger = m, Count = count };
                })
                .OrderByDescending(m => m.Count)
                .Select(m => m.Messenger)
                .FirstOrDefault();

            if (messenger != null)
            {
                messenger.Perform(new MessengerCommand(messenger.Action, message), sender);
            }
        }
    }
}
