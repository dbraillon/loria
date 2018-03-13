using System;

namespace Loria.Modules.Reminder
{
    public class Alarm
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Event Event { get; set; }
        public bool Repeat { get; set; }

        public Alarm() { }
        public Alarm(string text, string time, bool repeat)
        {
            Id = Guid.NewGuid();
            Text = text;
            Event = new Event(DateTime.Now + TimeParser.Parse(time));
            Repeat = repeat;
        }
        public Alarm(string text, DateTime at, bool repeat)
        {
            Id = Guid.NewGuid();
            Text = text;
            Event = new Event(at);
            Repeat = repeat;
        }
    }
}
