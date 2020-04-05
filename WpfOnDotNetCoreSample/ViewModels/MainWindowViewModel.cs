using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using WpfOnDotNetCoreSample.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace WpfOnDotNetCoreSample.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		// Some useful code snippets for ViewModel are defined as l*(llcom, llcomn, lvcomm, lsprop, etc...).
		private Stopwatch stopwatch = new Stopwatch(new Models.Infrastructures.Stopwatch());
		
		public ReadOnlyReactiveProperty<TimeSpan> Ellapsed { get; }
		
		public ReactiveCommand StartCommand { get; }

		public ReactiveCommand StopCommand { get; }

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
		}

		public void Initialize()
		{

		}
	}
}
