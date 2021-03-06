﻿using Loria.Core.Actions;
using Loria.Core.Actions.Activities;
using Loria.Core.Actions.Messengers;
using Loria.Core.Listeners;
using Loria.Core.Modules;
using Loria.Core.Propagators;
using Loria.Core.Storages;
using Loria.Core.Storages.SQLite;
using System.Threading;

namespace Loria.Core
{
    public class Engine
    {
        public bool IsLiving { get; private set; }

        public ModuleFactory ModuleFactory { get; set; }
        public ActivityFactory ActivityFactory { get; set; }
        public MessengerFactory MessengerFactory { get; set; }
        public ListenerFactory ListenerFactory { get; set; }
        public Propagator Propagator { get; set; }
        public CommandBuilder CommandBuilder { get; set; }
        public DatabaseStorage Storage { get; set; }

        public Engine()
        {
            ModuleFactory = new ModuleFactory(this);
            ActivityFactory = new ActivityFactory(this);
            MessengerFactory = new MessengerFactory(this);
            ListenerFactory = new ListenerFactory(this);
            Propagator = new Propagator(this);
            CommandBuilder = new CommandBuilder(this);
            Storage = new DatabaseStorage();
            Storage.EnsureCreated();
        }

        public void Live()
        {
            LiveAsync();
            
            while (IsLiving) Thread.Sleep(1000);
        }
        public void LiveAsync()
        {
            IsLiving = true;
            ModuleFactory.ConfigureAll();
            ListenerFactory.StartAll();
        }

        public void Stop()
        {
            ListenerFactory.StopAll();
            IsLiving = false;
        }
    }
}
