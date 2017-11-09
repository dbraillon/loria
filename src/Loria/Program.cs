using Loria.Core.Modules;
using System;

namespace Loria
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var moduleFactory = new ModuleFactory();
            moduleFactory.GetAll().ForEach(m => Console.WriteLine($"Loaded {m.Name}"));

            Console.ReadLine();
        }
    }
}
