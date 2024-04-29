using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;

namespace Playground
{
	public static class PathBuilder
	{
		public static int Reverts = 5;
		public static string DebugFolder = Directory.GetCurrentDirectory();

		private static string Support(int time, string path)
		{
			string parentDirectory = Directory.GetParent(path)!.FullName; time--;

			if (time is not 0) path = Support(time, parentDirectory);
			return path;
		}

		public static string CreatingPath() => Path.Combine(Support(Reverts, DebugFolder), "MyWpfAppForDb.WPF");

		public static string GetDataSource(IConfiguration config)
			=> String.Concat("Data Source=", config.GetConnectionString("sqlite"));
	}
}
