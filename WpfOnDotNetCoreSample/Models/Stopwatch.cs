using System;
using System.Reactive.Linq;
using Livet;
using WpfOnDotNetCoreSample.Models.Infrastructures;

namespace WpfOnDotNetCoreSample.Models
{
	public class Stopwatch : NotificationObject, IDisposable
	{
		private IStopwatch stopwatch;

		private IDisposable subscription;

		public bool IsRunning => stopwatch.IsRunning;

		public TimeSpan Ellapsed => stopwatch.Elapsed;

		public Stopwatch(IStopwatch stopwatch)
		{
			this.stopwatch = stopwatch;
		}

		public void Start()
		{
			stopwatch.Start();
			subscription = Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(10))
				.Subscribe(_ => RaisePropertyChanged(nameof(Ellapsed)));
			RaisePropertyChanged(nameof(IsRunning));
		}

		public void Stop()
		{
			stopwatch.Stop();
			subscription?.Dispose();
			RaisePropertyChanged(nameof(IsRunning));
		}
		
		public void Reset()
		{
			stopwatch.Reset();
			subscription?.Dispose();
			RaisePropertyChanged(nameof(Ellapsed));
			RaisePropertyChanged(nameof(IsRunning));
		}

		#region IDisposable Support
		private bool isDisposed = false; // 重複する呼び出しを検出するには

		protected virtual void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
				if (disposing)
				{
					// TODO: マネージ状態を破棄します (マネージ オブジェクト)。
					subscription?.Dispose();
				}

				// TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
				// TODO: 大きなフィールドを null に設定します。

				isDisposed = true;
			}
		}

		// TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
		// ~Stopwatch()
		// {
		//   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
		//   Dispose(false);
		// }

		// このコードは、破棄可能なパターンを正しく実装できるように追加されました。
		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
			Dispose(true);
			// TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
