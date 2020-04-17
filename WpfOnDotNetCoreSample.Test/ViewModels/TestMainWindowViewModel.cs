using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using Reactive.Bindings.Extensions;
using WpfOnDotNetCoreSample.Models;
using WpfOnDotNetCoreSample.ViewModels;

namespace WpfOnDotNetCoreSample.Test.ViewModels
{
	public class TestMainWindowViewModel
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void TestStartCommand()
		{
			var mock = new Mock<IStopwatchService>();
			mock.Setup(m => m.Start())
				.Callback(() =>
				{
					mock.Setup(m => m.Ellapsed).Returns(TimeSpan.FromMilliseconds(10));
					mock.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Ellapsed"));
					mock.Setup(m => m.IsRunning).Returns(true);
					mock.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("IsRunning"));
				});

			using (var vm = new MainWindowViewModel(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				vm.Ellapsed.ObserveProperty(x => x.Value, false)
					.Subscribe(_ => are.Set());
				vm.StartCommand.Execute();

				Assert.IsTrue(are.WaitOne(10));
				Assert.IsFalse(vm.StartCommand.CanExecute());
			}
		}

		[Test]
		public void TestStopCommand()
		{
			var mock = new Mock<IStopwatchService>();
			mock.Setup(m => m.Start())
				.Callback(() =>
				{
					mock.Setup(m => m.Ellapsed).Returns(TimeSpan.FromMilliseconds(10));
					mock.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Ellapsed"));
					mock.Setup(m => m.IsRunning).Returns(true);
					mock.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("IsRunning"));
				});

			using (var vm = new MainWindowViewModel(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				Assert.IsFalse(vm.StopCommand.CanExecute());

				vm.Ellapsed.ObserveProperty(x => x.Value, false)
					.Subscribe(_ => are.Set());
				vm.StartCommand.Execute();

				Assert.IsTrue(vm.StopCommand.CanExecute());

				are.WaitOne(10);
				vm.StopCommand.Execute();

				Assert.IsFalse(are.WaitOne(10));
			}
		}

		[Test]
		public void TestResetCommand()
		{
			var mock = new Mock<IStopwatchService>();

			using (var vm = new MainWindowViewModel(mock.Object))
			using (var are = new AutoResetEvent(false))
			{
				Assert.IsTrue(vm.ResetCommand.CanExecute());

				vm.Ellapsed.ObserveProperty(x => x.Value, false)
					.Subscribe(_ => are.Set());
				vm.ResetCommand.Execute();

				are.WaitOne(10);

				Assert.IsFalse(are.WaitOne(10));
			}
		}
	}
}