using System;
using System.ComponentModel;

namespace WpfOnDotNetCoreSample.Models
{
	public interface IStopwatchService : INotifyPropertyChanged
	{
		TimeSpan Ellapsed { get; }
		bool IsRunning { get; }

		void Dispose();
		void Reset();
		void Start();
		void Stop();
	}
}