using System;

namespace Loria.Modules.Tasks
{
    public enum Frequency
    {
        Minute,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Annual,
        Once
    }

    public class Schedule
    {
        public Frequency Frequency { get; set; }
        public int Modifier { get; set; }

        public Schedule(Frequency frequency) : this(frequency, 1)
        {
        }
        public Schedule(Frequency frequency, int modifier)
        {
            Frequency = Frequency;
            Modifier = modifier;
        }
    }
}