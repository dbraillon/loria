using Loria.Core;
using Loria.Core.Actions;
using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using Loria.Core.Listeners;
using Loria.Core.Modules;
using System;
using System.Globalization;

namespace Loria.Modules.Reminder
{
    public class ReminderModule : Listener, IActivity
    {
        public override string Name => "Reminder module";
        public override string Description => "It allows me to remind you things you've told me";
        public override string Keywords => "reminder";

        public string Action => "reminder";

        public string[] SupportedIntents => new[] { AddIntent };
        public string[] Samples => new[]
        {
            "reminder add -text order a pizza -time 2h"
        };

        public const string AddIntent = "add";
        public const string TextEntity = "text";
        public const string TimeEntity = "time";
        public const string AtEntity = "at";
        public const string RepeatEntity = "repeat";

        public ReminderModule(Engine engine) 
            : base(engine, 1000)
        {
        }
        
        public override void Configure()
        {
            // Nothing to configure yet
            Activate();
        }

        public void Perform(ActivityCommand command, IModule sender)
        {
            var answer = HandleCommand(command);

            if (!string.IsNullOrEmpty(answer))
            {
                Engine.Propagator.PropagateMessenger(answer, sender);
            }
        }

        public string HandleCommand(ActivityCommand command)
        {
            switch (command.Intent)
            {
                case AddIntent: return Add(command);
                default: return "You didn't tell me what was your intent.";
            }
        }

        public string Add(ActivityCommand command)
        {
            var textEntity = command.GetEntity(TextEntity);
            if (string.IsNullOrEmpty(textEntity?.Value)) return "You didn't tell me what the reminder was about.";

            var timeEntity = command.GetEntity(TimeEntity);
            var atEntity = command.GetEntity(AtEntity);
            var repeatEntity = command.GetEntity(RepeatEntity);

            if (timeEntity != null) return HandleTime(textEntity, timeEntity, repeatEntity);
            if (atEntity != null) return HandleAt(atEntity, atEntity, repeatEntity);

            return "You didn't tell me when the reminder should fire.";
        }
        public string HandleTime(Entity textEntity, Entity timeEntity, Entity repeatEntity)
        {
            if (string.IsNullOrEmpty(timeEntity?.Value)) return "You didn't tell me the amount of time before the reminder should fire.";

            try
            {
                var alarm = new Alarm(textEntity.Value, timeEntity.Value, repeatEntity != null);
                Engine.Storage.Create(Name, alarm.Id.ToString(), alarm);

                return $"Reminder set to fire in {alarm.Event.DateTime - DateTime.Now}";
            }
            catch (FormatException)
            {
                return "You give me a time format that I don't know.";
            }
        }
        public string HandleAt(Entity textEntity, Entity atEntity, Entity repeatEntity)
        {
            if (string.IsNullOrEmpty(atEntity?.Value)) return "You didn't tell me when the reminder should fire.";

            try
            {
                var date = DateTime.ParseExact(atEntity.Value, "yyyyMMdd HHmm", CultureInfo.CurrentCulture);
                var alarm = new Alarm(textEntity.Value, date, repeatEntity != null);
                Engine.Storage.Create(Name, alarm.Id.ToString(), alarm);

                return $"Reminder set to fire at {alarm.Event.DateTime}";
            }
            catch (FormatException)
            {
                return "You give me a date format that I don't know.";
            }
        }
        
        public override Command Listen()
        {
            foreach (var alarm in Engine.Storage.ReadAll<Alarm>(Name))
            {
                if (alarm.Event.Check())
                {
                    Engine.Storage.Delete(Name, alarm.Id.ToString());
                    return new MessengerCommand("console", alarm.Text);
                }
            }

            return null;
        }
    }
}
