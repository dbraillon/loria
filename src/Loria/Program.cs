using Loria.Core;
using Loria.Core.Actions.Messengers;
using System;
using System.Diagnostics;
using System.Threading;

namespace Loria
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            engine.LiveAsync();

            Debug.WriteLine("Modules loaded:");
            engine.ModuleFactory.Items.ForEach(m => Debug.WriteLine($"  {m.Name.PadRight(15, ' ')}\t{m.Description}"));

            Debug.WriteLine("Activities loaded:");
            engine.ActivityFactory.Items.ForEach(m => Debug.WriteLine($"  {m.Name.PadRight(15, ' ')}\t{m.Action}"));

            Debug.WriteLine("Messengers loaded:");
            engine.MessengerFactory.Items.ForEach(m => Debug.WriteLine($"  {m.Name.PadRight(15, ' ')}\t{m.Action}"));

            Debug.WriteLine("Listeners loaded:");
            engine.ListenerFactory.Items.ForEach(m => Debug.WriteLine($"  {m.Name}"));
            
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
