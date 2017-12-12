namespace Loria.Core.Actions.Messengers
{
    public interface IMessenger : IAction
    {
        void Perform(MessengerCommand command);
    }
}
