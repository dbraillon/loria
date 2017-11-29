using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Loria.Core.Modules
{
    public class ModuleFactory
    {
        public Engine Engine { get; set; }
        public DirectoryInfo Dir { get; set; }
        public List<Assembly> Assemblies { get; set; }
        public List<IModule> Items { get; set; }

        public ModuleFactory(Engine engine)
        {
            Engine = engine;

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Reload();
        }
        
        public List<TModule> GetAll<TModule>()
        {
            return Items.Where(m => typeof(TModule).IsAssignableFrom(m.GetType())).OfType<TModule>().ToList();
        }

        public void Reload()
        {
            Dir = InitializeDirectory();
            LoadFromDirectory();
            LoadFromAssemblies();
        }
        public void Reload(string directoryPath)
        {
            Dir = InitializeDirectory(directoryPath);
            Reload();
        }
        
        public void ConfigureAll()
        {
            foreach (var item in Items)
            {
                try
                {
                    item.Configure();
                }
                catch
                {
                    // Something went wrong while configuring a module
                    // It will not be activated
                }
            }
        }

        protected DirectoryInfo InitializeDirectory()
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "modules");
            return InitializeDirectory(directoryPath);
        }
        protected DirectoryInfo InitializeDirectory(string directoryPath)
        {
            return Directory.CreateDirectory(directoryPath);
        }

        protected void LoadFromDirectory() => Assemblies = LoadFromDirectory(Dir).ToList();
        protected IEnumerable<Assembly> LoadFromDirectory(string directoryPath)
        {
            return LoadFromDirectory(new DirectoryInfo(directoryPath));
        }
        protected IEnumerable<Assembly> LoadFromDirectory(DirectoryInfo directory)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (!directory.Exists) throw new ArgumentException($"Directory '{directory.FullName}' must exist.", nameof(directory));

            var dllFiles = directory.EnumerateFiles("*.dll", SearchOption.AllDirectories);
            var assemblies = dllFiles.Select(dll => Assembly.LoadFile(dll.FullName));

            return assemblies;
        }

        protected void LoadFromAssemblies() => Items = LoadFromAssemblies(Assemblies).ToList();
        protected IEnumerable<IModule> LoadFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(LoadFromAssembly);
        }
        protected IEnumerable<IModule> LoadFromAssembly(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t =>
                    typeof(IModule).IsAssignableFrom(t) &&
                    t.IsClass && !t.IsAbstract
                )
                .Select(t => Activator.CreateInstance(t, Engine))
                .OfType<IModule>();
        }

        protected Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            var entryAssembly = Assembly.GetEntryAssembly();
            var entryAssemblyDir = new DirectoryInfo(Path.GetDirectoryName(entryAssembly.Location));
            var entryAssemblyDlls = entryAssemblyDir.EnumerateFiles("*.dll");

            var assembly = 
                Assemblies.FirstOrDefault(a => a.GetName().Name == assemblyName.Name) ??
                entryAssemblyDlls
                    .Where(dll => Path.GetFileNameWithoutExtension(dll.Name) == assemblyName.Name)
                    .Select(dll => Assembly.LoadFile(dll.FullName))
                    .FirstOrDefault();

            return assembly;
        }
    }
}
