namespace Loria.Core.Modules
{
    public interface IModule
    {
        string Name { get; }
        string Description { get; }
        string Keywords { get; }

        bool IsEnabled();
        void Configure();
        void Activate();
        void Deactivate();
    }
}
