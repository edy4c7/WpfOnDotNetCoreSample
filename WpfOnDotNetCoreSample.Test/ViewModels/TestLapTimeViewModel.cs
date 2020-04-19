﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WpfOnDotNetCoreSample.ViewModels;

namespace WpfOnDotNetCoreSample.Test.ViewModels
{
	public class TestLapTimeViewModel
	{
		[Test]
		public void TestConstructor()
		{
			using (var vm = new LapTimeViewModel(1, TimeSpan.FromMilliseconds(10)))
			{
				Assert.AreEqual(1, vm.CountOfLap.Value);
				Assert.AreEqual(TimeSpan.FromMilliseconds(10), vm.LapTime.Value);
			}
		}	
	}
}
