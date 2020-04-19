using System;
using System.Collections.Generic;
using System.Text;
using Livet;
using Reactive.Bindings;

namespace WpfOnDotNetCoreSample.ViewModels
{
	public class LapTimeViewModel : ViewModel
	{
		public ReadOnlyReactiveProperty<int> CountOfLap { get; }
		public ReadOnlyReactiveProperty<TimeSpan> LapTime { get; }

		public LapTimeViewModel(int countOfLap, TimeSpan lapTime)
		{
			CountOfLap = new ReactiveProperty<int>(countOfLap)
				.ToReadOnlyReactiveProperty();
			LapTime = new ReactiveProperty<TimeSpan>(lapTime)
				.ToReadOnlyReactiveProperty();
		}
	}
}
