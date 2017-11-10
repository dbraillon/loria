using Loria.Core;
using Loria.Core.Actions.Messengers;
using System;
using System.Threading;

namespace Loria
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            engine.LiveAsync();

            Console.WriteLine("Modules loaded:");
            engine.ModuleFactory.Items.ForEach(m => Console.WriteLine($"{m.Name} - {m.Description}"));

            Console.WriteLine("Messengers loaded:");
            engine.MessengerFactory.Items.ForEach(m => Console.WriteLine($"{m.Name} - {m.Action}"));

            Console.WriteLine("Listeners loaded:");
            engine.ListenerFactory.Items.ForEach(m => Console.WriteLine($"{m.Name}"));

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
