using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace CaliburnMicroNotifyIcon.ViewModels
{
   [Export(typeof(MainViewModel))]
   public class MainViewModel : Screen
   {
      [ImportingConstructor]
      public MainViewModel()
      {
         Text = "Hello World!";
      }

      public string Text { get; set; }
   }
}
