using Loria.Core.Actions;
using Loria.Core.Actions.Messengers;

namespace Loria.Core.Propagators
{
    public class Propagator : IPropagator
    {
        public Engine Engine { get; set; }

        public Propagator(Engine engine)
        {
            Engine = engine;
        }

        public void Propagate(Command command)
        {
            if (command == null) return;

            if (command is MessengerCommand)
                PropagateMessenger(command as MessengerCommand);
        }
        
        public void PropagateMessenger(MessengerCommand command) => Engine.MessengerFactory.Perform(command);
    }
}
