using Loria.Core.Modules;

namespace Loria.Core.Actions.Activities
{
    public interface IActivity : IAction
    {
        string[] SupportedIntents { get; }
        string[] Samples { get; }

        void Perform(ActivityCommand command, IModule sender);
    }
}
