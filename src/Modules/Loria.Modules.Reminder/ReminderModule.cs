using Loria.Core.Actions.Activities;
using Loria.Core.Modules;
using System;
using Loria.Core;
using System.Collections.Generic;
using Loria.Core.Listeners;
using Loria.Core.Actions;
using Loria.Core.Actions.Messengers;

namespace Loria.Modules.Reminder
{
    public class ReminderModule : Listener, IActivity
    {
        public override string Name => "Reminder module";
        public override string Description => "It allows me to remind you things you've told me";

        public string Action => "reminder";

        public string[] SupportedIntents => new[] { AddIntent };
        public string[] Samples => new[]
        {
            "reminder add -text order a pizza -time 2h"
        };

        public const string AddIntent = "add";
        public const string TextEntity = "text";
        public const string TimeEntity = "time";

        public List<Alarm> Alarms { get; set; }

        public ReminderModule(Engine engine) 
            : base(engine, 1000)
        {
            Alarms = new List<Alarm>();
        }
        
        public override void Configure()
        {
            // Nothing to configure yet
            Activate();
        }

        public void Perform(ActivityCommand command)
        {
            switch (command.Intent)
            {
                case AddIntent:

                    var textEntity = command.GetEntity(TextEntity);
                    if (textEntity == null) throw new ArgumentNullException(TextEntity);

                    var timeEntity = command.GetEntity(TimeEntity);
                    if (timeEntity == null) throw new ArgumentNullException(TimeEntity);

                    Alarms.Add(new Alarm(textEntity.Value, timeEntity.Value));
                    break;

                default:
                    break;
            }
        }

        public override Command Listen()
        {
            var alarms = new List<Alarm>(Alarms);
            foreach (var alarm in alarms)
            {
                if (alarm.Event.Check())
                {
                    Alarms.Remove(alarm);
                    return new MessengerCommand("console", alarm.Text);
                }
            }

            return null;
        }
    }
}
