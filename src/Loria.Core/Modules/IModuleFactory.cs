using System.Collections.Generic;

namespace Loria.Core.Modules
{
    public interface IModuleFactory
    {
        List<IModule> GetAll();
        IModule Get(string name);

        void Reload();
    }
}