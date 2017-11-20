using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loria.Core.Actions
{
    public class CommandBuilder
    {
        public const string ActivityKeyword = "perform";
        public const string MessengerKeyword = "send";
        public const string EmptyEntityKey = "empty";
        public const char Separator = ' ';

        public Engine Engine { get; set; }

        public CommandBuilder(Engine engine)
        {
            Engine = engine;
        }

        public Command Parse(string str)
        {
            var strSplitted = str.Split(Separator);
            var firstArg = strSplitted.ElementAtOrDefault(0);

            if (HasActionKeyword(firstArg))
            {
                // No shortcut, action keyword is here
                var type = firstArg;

                if (string.Equals(type, ActivityKeyword, StringComparison.OrdinalIgnoreCase))
                {
                    return ParseActivity(strSplitted, 1);
                }

                else if (string.Equals(type, MessengerKeyword, StringComparison.OrdinalIgnoreCase))
                {
                    return ParseMessenger(strSplitted, 1);
                }

                else
                {
                    throw new ApplicationException(
                        "Command you typed cannot be parsed."
                    );
                }
            }
            else
            {
                // User is trying to get a shortcut by omitting action keyword
                var action = firstArg;

                var activityRelated = Engine.ActivityFactory.Get(action);
                var messengerRelated = Engine.MessengerFactory.Get(action);

                if (activityRelated != null && messengerRelated != null)
                {
                    throw new ApplicationException(
                        $"You can't use command shortcut if targetted action implement both IActivity and IMessenger. " +
                        $"Try to type :{Environment.NewLine}" +
                        $"{ActivityKeyword} {str}" +
                        $"or" +
                        $"{MessengerKeyword} {str}"
                    );
                }
                
                else if (activityRelated != null)
                {
                    return ParseActivity(strSplitted, 0);
                }

                else if (messengerRelated != null)
                {
                    return ParseMessenger(strSplitted, 0);
                }

                else
                {
                    throw new ApplicationException(
                        "There is no activity nor messenger related to the command you type."
                    );
                }
            }
        }
        
        public ActivityCommand ParseActivity(string[] strSplitted, int actionIndex)
        {
            var action = strSplitted.ElementAtOrDefault(actionIndex);
            var intent = strSplitted.ElementAtOrDefault(actionIndex + 1);

            var entitiesStr = strSplitted.Skip(actionIndex + 2);
            var entities = new List<Entity>
            {
                new Entity(EmptyEntityKey, string.Empty)
            };

            foreach (var entityStr in entitiesStr)
            {
                // If a part start with '-' symbol then its the name part
                // if not, its still the value part

                if (entityStr.StartsWith("-"))
                {
                    entities.Add(new Entity(entityStr.Substring(1), string.Empty));
                }
                else
                {
                    var lastEntity = entities.Last();
                    lastEntity.Value = $"{lastEntity.Value}{Separator}{entityStr}".Trim(Separator);
                }
            }

            return new ActivityCommand(action, intent, entities.ToArray());
        }
        public MessengerCommand ParseMessenger(string[] strSplitted, int actionIndex)
        {
            var action = strSplitted.ElementAtOrDefault(actionIndex);
            var message = string.Join(Separator.ToString(), strSplitted.Skip(actionIndex + 1));

            return new MessengerCommand(action, message);
        }

        public bool HasActionKeyword(string firstArg)
        {
            return
                string.Equals(firstArg, ActivityKeyword, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(firstArg, MessengerKeyword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
