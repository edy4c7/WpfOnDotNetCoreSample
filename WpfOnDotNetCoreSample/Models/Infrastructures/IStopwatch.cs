using System;
using System.Collections.Generic;
using System.Text;

namespace WpfOnDotNetCoreSample.Models.Infrastructures
{
	public interface IStopwatch
	{
		TimeSpan Elapsed { get; }
		long ElapsedMilliseconds { get; }
		long ElapsedTicks { get; }
		bool IsRunning { get; }
		void Reset();
		void Restart();
		void Start();
		void Stop();
	}
}
