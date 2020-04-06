using System;
using Livet;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using WpfOnDotNetCoreSample.Models;

namespace WpfOnDotNetCoreSample.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		// Some useful code snippets for ViewModel are defined as l*(llcom, llcomn, lvcomm, lsprop, etc...).
		private Stopwatch stopwatch = new Stopwatch(new Models.Infrastructures.Stopwatch());
		
		public ReadOnlyReactiveProperty<TimeSpan> Ellapsed { get; }
		
		public ReactiveCommand StartCommand { get; }

		public ReactiveCommand StopCommand { get; }

		public ReactiveCommand ResetCommand { get; }

		public MainWindowViewModel()
		{
			Ellapsed = stopwatch.ObserveProperty(x => x.Ellapsed)
				.ToReadOnlyReactiveProperty();

			StartCommand = stopwatch.ObserveProperty(x => x.IsRunning)
				.Inverse()
				.ToReactiveCommand();
			StartCommand.Subscribe(() => stopwatch.Start());

			StopCommand = stopwatch.ObserveProperty(x => x.IsRunning)
				.ToReactiveCommand();
			StopCommand.Subscribe(() => stopwatch.Stop());

			ResetCommand = new ReactiveCommand();
			ResetCommand.Subscribe(() => stopwatch.Reset());
		}

		public void Initialize()
		{

		}
	}
}
