using System;

namespace Loria.Modules.Reminder
{
    public class Event
    {
        public DateTime DateTime { get; set; }

        public Event(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public bool Check() => DateTime.Now >= DateTime;
    }
}
