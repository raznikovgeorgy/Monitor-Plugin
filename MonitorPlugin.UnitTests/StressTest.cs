using System;
using System.Diagnostics;
using Environment = System.Environment;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MonitorPlugin;
using System.IO;
using Monitor_Plugin.Inventor_API;
using Inventor;
using Monitor_Plugin;
using Monitor_Plugin.Parameters;


namespace MonitorPlugin.UnitTests
{
	[TestFixture]
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
		public void Start()
		{
			int n = 0;
			bool createBackFlag = true;
			while (n < 50)
			{
				InventorAPI inventorAPI = new InventorAPI();

				List<double> listMonitorParameters = new List<double>()
				{
					10, 160, 40, 50, 15, 172, 400, 30
				};

				MonitorParameters monitorParameters = new MonitorParameters(listMonitorParameters);

				MonitorManager monitorManager = new MonitorManager(inventorAPI,
					monitorParameters);

				var processes = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension("Inventor.exe"));
				var process = processes.First();
				//var process = processes[4];

				// При первой итерации проинициализировать объекты, отвечающие за фиксирование нагрузки
				if (n == 0)
				{
					_ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
					_cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
				}

				_cpuCounter.NextValue();

				monitorManager.CreateMonitor(createBackFlag);

				var ram = _ramCounter.NextValue();
				var cpu = _cpuCounter.NextValue();

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
			foreach (var process in Process.GetProcessesByName("Inventor"))
			{
				process.Kill();
			}
		}
	}
}
