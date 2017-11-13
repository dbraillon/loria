namespace Loria.Core.Actions
{
    public class Entity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Entity(string name, string value)
        {
            Name = name.StartsWith("-") ? name.Substring(1) : name;
            Value = value;
        }

        public override string ToString() => $"-{Name} {Value}";
    }
}
