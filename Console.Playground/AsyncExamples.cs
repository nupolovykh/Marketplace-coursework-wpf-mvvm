using System.Diagnostics;
using System.Security.Cryptography;

namespace Playground
{
	public enum Loading
	{
		hard,
		soft
	}

	internal static class AsyncExamples
	{
		private static int N = 8 * 2 * 2 * 2;

		public static void ThreadExample(Loading loading)
		{
			var stopWatch = Stopwatch.StartNew();
			{
				Thread[] threads = new Thread[N];
				for (int i = 0; i < N; i++)
				{
					threads[i] = new Thread((_) =>
						{
							if(loading == Loading.hard) HeavyLoad();
							else Thread.Sleep(TimeSpan.FromSeconds(1));
						}
					);

					threads[i].Start();
				}
				for (int i = 0; i < N; i++)
				{
					threads[i].Join();
				}
			}
			stopWatch.Stop();
			Console.WriteLine($"{N} threads sleeps for 1sec => {stopWatch.Elapsed}");
		}

		public static void TaskExample(Loading loading)
		{
			var stopWatch = Stopwatch.StartNew();

			{
				Task[] tasks = new Task[N];
				for (int i = 0; i < N; i++)
				{
					tasks[i] = new Task(() =>
						{
							if (loading == Loading.hard) HeavyLoad();
							else Task.Delay(TimeSpan.FromSeconds(1)).Wait();
						}
					);

					tasks[i].Start();
				}
				Task.WaitAll(tasks);
			}
			stopWatch.Stop();
			Console.WriteLine($"{N} tasks sync   sleeps for 1sec => {stopWatch.Elapsed}");

		}

		public static void TaskASyncExample(Loading loading)
		{
			var stopWatch = Stopwatch.StartNew();
			Task[] tasks = new Task[N];
			for (int i = 0; i < N; i++)
			{
				tasks[i] = new Task(async () =>
				{
					if (loading == Loading.hard) HeavyLoad();
					else await Task.Delay(TimeSpan.FromSeconds(1));
				}
				);

				tasks[i].Start();
			}
			Task.WaitAll(tasks);
			stopWatch.Stop();
			Console.WriteLine($"{N} tasks async  sleeps for 1sec => {stopWatch.Elapsed}");
		}

		public static void HeavyLoadWithTimer()
		{
			var stopWatch = Stopwatch.StartNew();
			{
				HeavyLoad();
			}
			stopWatch.Stop();
		}

		private static void HeavyLoad()
		{
			var rand = new Random();
			byte[] bytes = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				bytes[i] = (byte)rand.Next(0, 256);
			}
			for (int i = 0; i < 1_000_000; i++)
			{
				MD5.HashData(bytes);
			}
		}
	}
}
