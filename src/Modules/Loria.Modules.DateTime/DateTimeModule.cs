using Loria.Core;
using Loria.Core.Actions;
using Loria.Core.Actions.Activities;
using Loria.Core.Modules;
using System.Globalization;

namespace Loria.Modules.DateTime
{
    public class DateTimeModule : Module, IActivity
    {
        public override string Name => "Date and time module";
        public override string Description => "It allows me to give you current date and time";

        public string Action => "datetime";

        public string[] SupportedIntents => new[] { DateIntent, TimeIntent };
        public string[] Samples => new[] 
        {
            "perform datetime date",
            "perform datetime time"
        };

        public const string DateIntent = "date";
        public const string TimeIntent = "time";

        public DateTimeModule(Engine engine) 
            : base(engine)
        {
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
                case DateIntent:

                    var todayDate = System.DateTime.Now.ToString("D", CultureInfo.CurrentUICulture);
                    Engine.Propagator.Propagate(Command.Parse($"send console {todayDate}"));
                    break;

                case TimeIntent:

                    var todayTime = System.DateTime.Now.ToString("t", CultureInfo.CurrentUICulture);
                    Engine.Propagator.Propagate(Command.Parse($"send console {todayTime}"));
                    break;

                default:
                    break;
            }
        }
    }
}
