using System;
using System.Collections.Generic;
using System.Text;

namespace Loria.Modules.Reminder
{
    public class Alarm
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Event Event { get; set; }

        public Alarm() { }
        public Alarm(string text, string time)
        {
            Id = Guid.NewGuid();
            Text = text;
            Event = new Event(DateTime.Now + TimeParser.Parse(time));
        }
    }
}
