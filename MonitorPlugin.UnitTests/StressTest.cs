using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Environment = System.Environment;
using NUnit.Framework;
using System.IO;
using Monitor_Plugin.Inventor_API;
using Monitor_Plugin;
using Monitor_Plugin.Parameters;


namespace MonitorPlugin.UnitTests
{
	[TestFixture]
	[Ignore("Ignore a fixture")]
	public class StressTest
	{
		private readonly StreamWriter _writer;
		private PerformanceCounter _ramCounter;
		private PerformanceCounter _cpuCounter;

		public StressTest()
		{
			_writer = new StreamWriter(@".\StressTest.txt");
		}



		[Test]
		[Ignore("Ignore a stress-test")]
		public void Start()
		{
			int n = 0;
			bool createBackFlag = true;
			while (true)
			{
				InventorAPI inventorAPI = new InventorAPI();

				List<double> listMonitorParameters = new List<double>()
				{
					10, 160, 40, 50, 15, 172, 400, 30
				};

				MonitorParameters monitorParameters = new MonitorParameters(listMonitorParameters);

				MonitorManager monitorManager = new MonitorManager(inventorAPI,
					monitorParameters);

				var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension("Inventor.exe"));
				var process = processes.First();

				// При первой итерации проинициализировать объекты, отвечающие за фиксирование нагрузки
				if (n == 0)
				{
					_ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
					_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
				}

				//_cpuCounter.NextValue();

				monitorManager.CreateMonitor(createBackFlag);

				var ram = _ramCounter.NextValue();
				var cpu = Math.Round(_cpuCounter.NextValue());

				_writer.Write($"{n}. ");
				_writer.Write($"RAM: {Math.Round(ram / 1024 / 1024)} MB");
				_writer.Write($"\tCPU: {cpu} %");
				_writer.Write(Environment.NewLine);
				_writer.Flush();
				n += 1;
			}
		}

		public void CloseApplication()
		{
			foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension("Inventor.exe")))
			{
				process.Kill();
			}
		}
	}
}
