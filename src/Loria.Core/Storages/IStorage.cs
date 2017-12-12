using System.Collections.Generic;

namespace Loria.Core.Storages
{
    public interface IStorage
    {
        void Create(string module, string key, object obj);
        void Delete(string module, string key);
        void Update(string module, string key, object obj);
        TObject Read<TObject>(string module, string key);
        IEnumerable<TObject> ReadAll<TObject>(string module);
    }
}
