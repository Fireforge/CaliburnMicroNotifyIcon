using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using CaliburnMicroNotifyIcon.ViewModels;

namespace CaliburnMicroNotifyIcon
{
   public class AppBootstrapper : BootstrapperBase
   {
      public AppBootstrapper()
      {
         Initialize();
      }

      protected override void OnStartup(object sender, StartupEventArgs e)
      {
         // Set the main window that DisplayRootViewFor makes to be completely hidden
         var settings = new Dictionary<string, object>
         {
             { "Visibility", Visibility.Hidden },
             { "AllowsTransparency", true },
             { "WindowStyle", WindowStyle.None },
         };
         DisplayRootViewFor<IShell>(settings);
      }

      // Boilerplate code taken from WPF Caliburn.Micro examples

      private CompositionContainer container;

      protected override void Configure()
      {
         // Configure CompositionContainer for connecting components

         container = new CompositionContainer(
            new AggregateCatalog(
                 AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                 )
            );

         var batch = new CompositionBatch();

         batch.AddExportedValue<IWindowManager>(new WindowManager());
         batch.AddExportedValue<IEventAggregator>(new EventAggregator());
         batch.AddExportedValue(container);

         container.Compose(batch);
      }

      //  Use this code to mark assemblies available to the Caliburn.Micro framework to use views from
      protected override IEnumerable<Assembly> SelectAssemblies()
      {
         return new[] {
            Assembly.GetExecutingAssembly()
         };
      }

      protected override object GetInstance(Type serviceType, string key)
      {
         string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
         var exports = container.GetExportedValues<object>(contract);

         if (exports.Any())
            return exports.First();

         throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
      }

      protected override IEnumerable<object> GetAllInstances(Type serviceType)
      {
         return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
      }

      protected override void BuildUp(object instance)
      {
         container.SatisfyImportsOnce(instance);
      }
   }
}
