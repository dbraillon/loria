namespace Loria.Core.Actions
{
    public abstract class Command
    {
        public string Type { get; set; }
        public string Action { get; set; }

        protected Command(string type, string action)
        {
            Type = type;
            Action = action;
        }
    }
}
