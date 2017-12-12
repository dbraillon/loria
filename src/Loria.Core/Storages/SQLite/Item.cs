using System.ComponentModel.DataAnnotations;

namespace Loria.Core.Storages.SQLite
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Module { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Serialized { get; set; }
    }
}
