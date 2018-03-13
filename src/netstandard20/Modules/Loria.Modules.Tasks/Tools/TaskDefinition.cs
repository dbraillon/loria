using System;
using System.Collections.Generic;

namespace Loria.Modules.Tasks
{
    public class TaskDefinition
    {
        public string TaskName { get; set; }
        public bool DeleteAfterLastRun { get; set; }

        public List<TimeTrigger> Triggers { get; set; }
        public List<Action> Actions { get; set; }

        public TaskDefinition(string taskName)
        {
            TaskName = taskName;
            DeleteAfterLastRun = false;

            Triggers = new List<TimeTrigger>();
            Actions = new List<Action>();
        }
    }
}