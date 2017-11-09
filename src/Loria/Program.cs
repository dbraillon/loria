using Loria.Core;
using Loria.Core.Actions.Messengers;
using System;

namespace Loria
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            engine.LiveAsync();

            engine.ModuleFactory.Items.ForEach(m => Console.WriteLine($"{m.Name} - {m.Description}"));
            engine.MessengerFactory.Items.ForEach(m => Console.WriteLine($"{m.Name} - {m.Action}"));
            engine.MessengerFactory.Items.ForEach(m => m.Perform(new MessengerCommand("send console Test command!")));

            Console.ReadLine();
        }
    }
}
