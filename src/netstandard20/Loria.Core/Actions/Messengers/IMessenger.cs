using Loria.Core.Modules;

namespace Loria.Core.Actions.Messengers
{
    public interface IMessenger : IAction
    {
        void Perform(MessengerCommand command, IModule sender);
    }
}
