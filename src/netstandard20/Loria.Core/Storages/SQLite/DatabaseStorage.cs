using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loria.Core.Storages.SQLite
{
    public class DatabaseStorage : IStorage
    {
        public void EnsureCreated()
        {
            using (var db = new Database())
            {
                db.Database.EnsureCreated();
            }
        }

        public void Create(string module, string key, object obj)
        {
            using (var db = new Database())
            {
                var item = db.Items.FirstOrDefault(i => i.Module == module && i.Key == key);
                if (item != null) throw new ApplicationException($"An item with {nameof(key)} '{key}' already exists");

                db.Items.Add(
                    new Item
                    {
                        Module = module,
                        Key = key,
                        Type = obj.GetType().FullName,
                        Serialized = JsonConvert.SerializeObject(obj)
                    }
                );
                db.SaveChanges();
            }
        }

        public void Delete(string module, string key)
        {
            using (var db = new Database())
            {
                var item = db.Items.FirstOrDefault(i => i.Module == module && i.Key == key);
                if (item == null) return;

                db.Items.Remove(item);
                db.SaveChanges();
            }
        }

        public TObject Read<TObject>(string module, string key)
        {
            using (var db = new Database())
            {
                var item = db.Items.FirstOrDefault(i => i.Module == module && i.Key == key);
                if (item == null) return default(TObject);

                return JsonConvert.DeserializeObject<TObject>(item.Serialized);
            }
        }

        public IEnumerable<TObject> ReadAll<TObject>(string module)
        {
            using (var db = new Database())
            {
                var items = db.Items.Where(i => i.Module == module).ToList();

                return items.Select(item => JsonConvert.DeserializeObject<TObject>(item.Serialized));
            }
        }

        public void Update(string module, string key, object obj)
        {
            using (var db = new Database())
            {
                var item = db.Items.FirstOrDefault(i => i.Module == module && i.Key == key);
                if (item == null) throw new ApplicationException($"No item with {nameof(key)} '{key}' exists");

                item.Serialized = JsonConvert.SerializeObject(obj);
                item.Type = obj.GetType().FullName;
                db.SaveChanges();
            }
        }
    }
}
