using Loria.Core;
using Loria.Core.Actions;
using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using Loria.Core.Listeners;
using Loria.Core.Modules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Loria.Modules.Tasks
{
    public class TaskModule : Listener, IActivity
    {
        public override string Name => "Task module";
        public override string Description => "It allows me to create schedulable tasks";
        public override string Keywords => "task";

        public string Action => "task";

        public string[] SupportedIntents => new[]
        {
            AddIntent,
            DeleteIntent,
            RunIntent,
            GetIntent,
            HelpIntent
        };
        public string[] Samples => new[]
        {
            "task add -name Trash reminder -schedule weekly -daysOfWeek 1,3,5 -starttime 20:00"
        };

        public const string AddIntent = "add";
        public const string AddNameEntity = "name";
        public const string AddDescEntity = "desc";
        public const string AddScheduleEntity = "schedule";
        public const string AddModifierEntity = "modifier";
        public const string AddDaysOfWeekEntity = "daysOfWeek";
        public const string AddDaysOfMonthEntity = "daysOfMonth";
        public const string AddMonthsOfYearEntity = "monthsOfYear";
        public const string AddStartTimeEntity = "starttime";
        public const string AddEndTimeEntity = "endtime";
        public const string AddStartDateEntity = "startdate";
        public const string AddEndDateEntity = "enddate";
        public const string AddDeleteEntity = "delete";
        public const string AddHelpEntity = "help";
        public const string AddHelpAltEntity = "?";

        public const string DeleteIntent = "delete";
        public const string DeleteAllEntity = "all";
        public const string DeleteNameEntity = "name";
        public const string DeleteHelpEntity = "help";
        public const string DeleteHelpAltEntity = "?";
        
        public const string RunIntent = "run";
        public const string RunNameEntity = "name";
        public const string RunHelpEntity = "help";
        public const string RunHelpAltEntity = "?";

        public const string GetIntent = "get";
        public const string GetAllEntity = "all";
        public const string GetNameEntity = "name";
        public const string GetHelpEntity = "help";
        public const string GetHelpAltEntity = "?";

        public const string HelpIntent = "help";

        public TaskModule(Engine engine) 
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
            var answer = Handle(command);
            if (!string.IsNullOrEmpty(answer))
            {
                Engine.Propagator.PropagateMessenger(answer, sender);
            }
        }

        public string Handle(ActivityCommand command)
        {
            switch (command.Intent)
            {
                case AddIntent: return HandleAdd(command);
                case DeleteIntent: return HandleDelete(command);
                case GetIntent: return HandleGet(command);
                case RunIntent: return HandleRun(command);
                case HelpIntent: return HandleHelp(command);
                default: return HandleHelp(command);
            }
        }

        public string HandleAdd(ActivityCommand command)
        {
            var nameEntity = command.GetEntity(AddNameEntity);
            var descEntity = command.GetEntity(AddDescEntity);
            var scheduleEntity = command.GetEntity(AddScheduleEntity);
            var modifierEntity = command.GetEntity(AddModifierEntity);
            var daysOfWeekEntity = command.GetEntity(AddDaysOfWeekEntity);
            var daysOfMonthEntity = command.GetEntity(AddDaysOfMonthEntity);
            var monthsOfYearEntity = command.GetEntity(AddMonthsOfYearEntity);
            var startTimeEntity = command.GetEntity(AddStartTimeEntity);
            var endTimeEntity = command.GetEntity(AddEndTimeEntity);
            var startDateEntity = command.GetEntity(AddStartDateEntity);
            var endDateEntity = command.GetEntity(AddEndDateEntity);
            var deleteEntity = command.GetEntity(AddDeleteEntity);
            var helpEntity = command.GetEntity(AddHelpEntity);
            var helpAltEntity = command.GetEntity(AddHelpAltEntity);

            if (string.IsNullOrEmpty(nameEntity?.Value)) return "You didn't give me a name for this new task";
            if (string.IsNullOrEmpty(scheduleEntity?.Value)) return "You didn't give me a schedule for this new task";
            if (!Enum.TryParse(scheduleEntity.Value, out Frequency frequency)) return "You didn't give me a valid frequency schedule";
            if (!int.TryParse(modifierEntity?.Value, out int modifier)) modifier = 1;
            
            var daysOfWeekSpl = daysOfWeekEntity?.Value.Split(',').Select(str => str.Trim());
            var daysOfWeek = daysOfWeekSpl?
                .Select(str =>
                {
                    if (int.TryParse(str, out int dayOfWeek))
                    {
                        return dayOfWeek;
                    }

                    return -1;
                })
                .Where(dayOfWeek => dayOfWeek >= 1 && dayOfWeek <= 7)
                .ToArray();

            var daysOfMonthSpl = daysOfMonthEntity?.Value.Split(',').Select(str => str.Trim());
            var daysOfMonth = daysOfMonthSpl?
                .Select(str =>
                {
                    if (int.TryParse(str, out int dayOfMonth))
                    {
                        return dayOfMonth;
                    }

                    return -1;
                })
                .Where(dayOfMonth => dayOfMonth >= 1 && dayOfMonth <= 31)
                .ToArray();

            var monthsOfYearSpl = monthsOfYearEntity?.Value.Split(',').Select(str => str.Trim());
            var monthsOfYear = monthsOfYearSpl?
                .Select(str =>
                {
                    if (int.TryParse(str, out int monthOfYear))
                    {
                        return monthOfYear;
                    }

                    return -1;
                })
                .Where(monthOfYear => monthOfYear >= 1 && monthOfYear <= 12)
                .ToArray();
            
            var startTime = startTimeEntity != null ?
                DateTime.ParseExact(startTimeEntity?.Value, "HH:mm", CultureInfo.InvariantCulture) :
                DateTime.Now;
            var endTime = endTimeEntity != null ?
                DateTime.ParseExact(endTimeEntity?.Value, "HH:mm", CultureInfo.InvariantCulture) :
                DateTime.Now;
            var startDate = startDateEntity != null ?
                DateTime.ParseExact(startDateEntity?.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) :
                DateTime.Now;
            var endDate = endDateEntity != null ?
                DateTime.ParseExact(endDateEntity?.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) :
                DateTime.Now;

            var trigger = new TimeTrigger
            {
                Schedule = new Schedule(frequency, modifier),
                SelectedDaysOfMonth = daysOfMonth ?? Enumerable.Range(1, 31).ToArray(),
                SelectedDaysOfWeek = daysOfWeek ?? Enumerable.Range(1, 7).ToArray(),
                SelectedMonthsOfYear = monthsOfYear ?? Enumerable.Range(1, 12).ToArray(),
                StartDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second),
                EndDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second)
            };

            var task = new TaskDefinition(nameEntity.Value)
            {
                DeleteAfterLastRun = deleteEntity != null
            };
            task.Triggers.Add(trigger);

            return "Successfuly added a task";
        }

        public string HandleDelete(ActivityCommand command)
        {
            return "";
        }

        public string HandleRun(ActivityCommand command)
        {
            return "";
        }

        public string HandleGet(ActivityCommand command)
        {
            return "";
        }

        public string HandleHelp(ActivityCommand command)
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var dirPath = Path.GetDirectoryName(assemblyPath);
            var readmePath = Path.Combine(dirPath, "README.md");

            return File.ReadAllText(readmePath);
        }

        public override Command Listen()
        {
            return null;
        }
    }
}
