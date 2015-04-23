﻿using System.Windows;
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
         DisplayRootViewFor<TaskbarIconViewModel>();
      }
   }
}
