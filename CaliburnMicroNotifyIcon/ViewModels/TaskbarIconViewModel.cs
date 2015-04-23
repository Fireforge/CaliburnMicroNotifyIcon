using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace CaliburnMicroNotifyIcon.ViewModels
{
   public interface IShell { }

   [Export(typeof(IShell))]
   public class TaskbarIconViewModel : Screen, IShell
   {
      [ImportingConstructor]
      public TaskbarIconViewModel(IWindowManager windowManager, MainViewModel mvm)
      {
         this.windowManager = windowManager;

         this.mvm = mvm;
      }

      IWindowManager windowManager;
      MainViewModel mvm;

      public void Show()
      {
         windowManager.ShowWindow(mvm);
      }

      public void Exit()
      {
         TryClose();
      }
   }
}
