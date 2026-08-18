using Microsoft.Extensions.Configuration;

namespace Marketplace.Playground
{
	public static class ConfigurationLoader
	{
		/// <summary>
		/// appsettings.json is linked into this project and copied next to the built
		/// assembly, so it is read from there. It used to be found by walking exactly
		/// five directories up from the working directory into the WPF project - which
		/// only held for one build configuration on one machine.
		/// </summary>
		public static IConfiguration Load()
			=> new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

		/// <summary>
		/// The configured connection string, used as-is: it already carries its own
		/// "Data Source=" prefix, and prepending a second one produced
		/// "Data Source=Data Source=app.db".
		/// </summary>
		public static string GetConnectionString(IConfiguration config)
			=> config.GetConnectionString("sqlite");
	}
}
