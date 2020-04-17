using System.Windows;

using Livet;
using WpfOnDotNetCoreSample.ObjectResolvers;

namespace WpfOnDotNetCoreSample
{
	public partial class App : Application
	{
		static App()
		{
			ObjectResolver.Instance.Compile();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			DispatcherHelper.UIDispatcher = Dispatcher;
			//AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
		}

		// Application level error handling
		//private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		//{
		//    //TODO: Logging
		//    MessageBox.Show(
		//        "Something errors were occurred.",
		//        "Error",
		//        MessageBoxButton.OK,
		//        MessageBoxImage.Error);
		//
		//    Environment.Exit(1);
		//}
	}
}
