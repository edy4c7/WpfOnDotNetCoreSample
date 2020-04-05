using Moq;
using NUnit.Framework;
using WpfOnDotNetCoreSample.Models;
using Infrastructures = WpfOnDotNetCoreSample.Models.Infrastructures;
using System;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using WpfOnDotNetCoreSample.ViewModels;
using System.Windows.Input;

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
			using (var vm = new MainWindowViewModel())
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
			using (var vm = new MainWindowViewModel())
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
	}
}