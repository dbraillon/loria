﻿using Loria.Core;
using Loria.Core.Actions;
using Loria.Core.Actions.Activities;
using Loria.Core.Modules;
using NuGet.Configuration;
using NuGet.PackageManagement;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Loria.Modules.NuGet
{
    public class NugetModule : Module, IActivity
    {
        public override string Name => "NuGet module";
        public override string Description => "It allows me to update my capabilities by downloading new modules";

        public string Action => "nuget";
        public string[] SupportedIntents => new[] { SearchIntent, InstallIntent, UpdateIntent };
        public string[] Samples => throw new NotImplementedException();

        public const string SearchIntent = "search";
        public const string InstallIntent = "install";
        public const string UpdateIntent = "update";
        public const string TextEntity = "text";
        public const string PackageEntity = "package";

        public SourceRepository SourceRepository { get; set; }
        public PackageSearchResource SearchResource { get; set; }
        public PackageMetadataResource MetadataResource { get; set; }
        public NuGetPackageManager PackageManager { get; set; }

        public NugetModule(Engine engine) 
            : base(engine)
        {
        }
        
        public override void Configure()
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());

            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            SourceRepository = new SourceRepository(packageSource, providers);

            var settings = Settings.LoadDefaultSettings(Engine.ModuleFactory.Dir.FullName, null, new MachineWideSettings());

            SearchResource = SourceRepository.GetResource<PackageSearchResource>();
            MetadataResource = SourceRepository.GetResource<PackageMetadataResource>();
            PackageManager = new NuGetPackageManager(Repository.CreateProvider(providers.Select(p => p.Value)), settings, Engine.ModuleFactory.Dir.FullName);

            Activate();
        }

        public void Perform(ActivityCommand command)
        {
            switch (command.Intent)
            {
                case SearchIntent:

                    var textEntity = command.Entities.FirstOrDefault(e => e.Name == TextEntity);
                    var searchMetadata = SearchResource.SearchAsync(textEntity.Value, new SearchFilter(false), 0, 10, null, CancellationToken.None).GetAwaiter().GetResult();
                    var message = string.Join(Environment.NewLine, searchMetadata.Select(m => $"{m.Title} - {m.Description}"));
                    
                    Engine.Propagator.Propagate(Command.Parse($"send console {message}"));
                    break;

                case InstallIntent:

                    var packageEntity = command.Entities.FirstOrDefault(e => e.Name == PackageEntity);
                    var packagesMetadata = MetadataResource.GetMetadataAsync(packageEntity.Value, false, false, new Logger(), CancellationToken.None).GetAwaiter().GetResult();
                    var packageMetadata = packagesMetadata.LastOrDefault();

                    var resolutionContext = new ResolutionContext(DependencyBehavior.Highest, false, false, VersionConstraints.None);

                    PackageManager.InstallPackageAsync(
                        PackageManager.PackagesFolderNuGetProject,
                        packageMetadata.Identity,
                        resolutionContext,
                        new ProjectContext(),
                        SourceRepository,
                        Enumerable.Empty<SourceRepository>(),
                        CancellationToken.None
                    ).GetAwaiter().GetResult();

                    break;
            }

            
        }
    }
}