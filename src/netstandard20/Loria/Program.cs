using Loria.Core;
using Loria.Core.Actions.Messengers;
using System;
using System.Diagnostics;
using System.Linq;
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
            engine.ModuleFactory.Items.Where(i => i.IsEnabled()).ToList().ForEach(m => Debug.WriteLine($"  {m.Name.PadRight(15, ' ')}\t{m.Description}"));

            Debug.WriteLine("Activities loaded:");
            engine.ActivityFactory.Items.Where(i => i.IsEnabled()).ToList().ForEach(m => Debug.WriteLine($"  {m.Name.PadRight(15, ' ')}\t{m.Action}"));

            Debug.WriteLine("Messengers loaded:");
            engine.MessengerFactory.Items.Where(i => i.IsEnabled()).ToList().ForEach(m => Debug.WriteLine($"  {m.Name.PadRight(15, ' ')}\t{m.Action}"));

            Debug.WriteLine("Listeners loaded:");
            engine.ListenerFactory.Items.Where(i => i.IsEnabled()).ToList().ForEach(m => Debug.WriteLine($"  {m.Name}"));
            
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
