using System;
using System.Reactive.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using Reactive.Bindings.Extensions;
using WpfOnDotNetCoreSample.Models;
using Infrastructures = WpfOnDotNetCoreSample.Models.Infrastructures;

namespace WpfOnDotNetCoreSample.Test.Models
{
	public class TestStopwatch
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void TestStart()
		{
			var mock = new Mock<Infrastructures.IStopwatch>();
			var called = false;
			mock.Setup(m => m.Start()).Callback(() => called = true);

			using (var sw = new Stopwatch(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				sw.ObserveProperty(x => x.Ellapsed, false)
					.Subscribe(_ => are.Set());

				sw.Start();

				Assert.IsTrue(called);
				Assert.IsTrue(are.WaitOne(10));
			}
		}

		[Test]
		public void TestStop()
		{
			var mock = new Mock<Infrastructures.IStopwatch>();
			var called = false;
			mock.Setup(m => m.Stop()).Callback(() => called = true);

			using (var sw = new Stopwatch(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				sw.ObserveProperty(x => x.Ellapsed, false)
					.Subscribe(_ => are.Set());

				sw.Stop();

				Assert.IsTrue(called);
				Assert.IsFalse(are.WaitOne(10));
			}
		}

		[Test]
		public void TestReset()
		{
			var mock = new Mock<Infrastructures.IStopwatch>();
			var called = false;
			mock.Setup(m => m.Reset()).Callback(() => called = true);

			using (var sw = new Stopwatch(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				sw.ObserveProperty(x => x.Ellapsed, false)
					.Subscribe(_ => are.Set());
				sw.Start();

				sw.Reset();

				sw.Stop();

				Assert.IsTrue(called);
				Assert.IsFalse(are.WaitOne(10));
			}
		}

		[Test]
		public void TestIsRunning()
		{
			var mock = new Mock<Infrastructures.IStopwatch>();
			var called = false;
			mock.Setup(m => m.IsRunning).Callback(() => called = true);

			using (var sw = new Stopwatch(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				sw.ObserveProperty(x => x.IsRunning, false)
					.Subscribe(_ => are.Set());

				var isRunning = sw.IsRunning;
				Assert.IsTrue(called);

				sw.Start();
				Assert.IsTrue(are.WaitOne(10));
				
				sw.Stop();
				Assert.IsTrue(are.WaitOne(10));
			}
		}

		[Test]
		public void TestEllapsed()
		{
			var mock = new Mock<Infrastructures.IStopwatch>();
			var called = false;
			mock.Setup(m => m.Elapsed).Callback(() => called = true);

			using (var sw = new Stopwatch(mock.Object))
			{
				var ellapsed = sw.Ellapsed;
				Assert.IsTrue(called);
			}
		}
	}
}