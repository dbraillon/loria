namespace Loria.Core.Modules
{
    public abstract class Module : IModule
    {
        public bool Enabled { get; set; }

        public abstract string Name { get; }
        public abstract string Description { get; }
        
        public Module()
        {
        }

        public void Activate() => Enabled = true;
        public void Deactivate() => Enabled = false;
        public bool IsEnabled() => Enabled;

        public abstract void Configure();
    }
}
