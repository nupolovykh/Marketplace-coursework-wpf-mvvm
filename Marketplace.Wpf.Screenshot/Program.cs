using Marketplace.EntityFramework.Services;
using Marketplace.Wpf;
using Marketplace.Wpf.State.Authenticators;
using Marketplace.Wpf.ViewModels;
using Marketplace.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

if (args.Length < 1)
{
	Console.Error.WriteLine("Usage: Marketplace.Wpf.Screenshot <output-png-path> [screen]");
	Console.Error.WriteLine("Screens: authorization, registration, home, profile, statistics, delivery");
	return 1;
}

string outputPath = args[0];
string screen = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]) ? args[1].ToLowerInvariant() : "home";

int exitCode = 0;

// WPF's layout and rendering machinery requires an STA thread; top-level
// statements do not run on one.
var thread = new Thread(() =>
{
	try
	{
		Render(screen, outputPath);
	}
	catch (Exception ex)
	{
		Console.Error.WriteLine(ex);
		exitCode = 1;
	}
});
thread.SetApartmentState(ApartmentState.STA);
thread.Start();
thread.Join();

return exitCode;

static void Render(string screen, string outputPath)
{
	// The host is built exactly as the app builds it, so the screenshot exercises
	// the real configuration, DI graph and database rather than a stand-in.
	IHost host = App.CreateHostBuilder().Build();
	host.Start();

	// Creates the schema and applies the seed data, the same call App.OnStartup
	// makes. Done before any dispatcher context exists, so the blocking waits
	// below cannot deadlock against it.
	if (ConnectionChecker.DatabaseValidation(host, out Exception failure) is not null)
	{
		throw new InvalidOperationException("The database could not be prepared.", failure);
	}

	bool needsAccount = screen is not ("authorization" or "registration");

	if (needsAccount)
	{
		// A seeded administrator, so role-gated controls are part of the picture.
		host.Services.GetRequiredService<IAuthenticator>().Login("John Doe", "123").GetAwaiter().GetResult();
	}

	// WPF expects an Application to exist even though Run() is never called, and
	// the view models await their data - so a dispatcher context has to be in
	// place for those continuations to have somewhere to land.
	_ = new Application();
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

	(FrameworkElement view, Func<bool> loaded) = Compose(host, screen);

	// Let the view models finish loading. Draining the dispatcher queue is what
	// actually advances them; the sleep only yields to the database work running
	// on the thread pool.
	DateTime deadline = DateTime.UtcNow.AddSeconds(30);

	while (DateTime.UtcNow < deadline)
	{
		Dispatcher.CurrentDispatcher.Invoke(() => { }, DispatcherPriority.SystemIdle);

		if (loaded()) break;

		Thread.Sleep(50);
	}

	if (!loaded()) Console.Error.WriteLine($"warning: {screen} still had no data after 30s, rendering it anyway");

	// One more drain so bindings raised by the final load are applied.
	Dispatcher.CurrentDispatcher.Invoke(() => { }, DispatcherPriority.SystemIdle);

	// Views declaring an explicit size are rendered at it; the ones that only set
	// a minimum get the app window's content area.
	var size = new Size(
		double.IsNaN(view.Width) ? 960 : view.Width,
		double.IsNaN(view.Height) ? 600 : view.Height);

	// RenderTargetBitmap starts from a transparent canvas. A Border gives it a
	// real opaque background without a VisualBrush, which computes its own
	// viewbox from the visual bounds and silently stretches the layout.
	var wrapper = new Border { Background = Brushes.White, Child = view };
	wrapper.Measure(size);
	wrapper.Arrange(new Rect(size));
	wrapper.UpdateLayout();

	Dispatcher.CurrentDispatcher.Invoke(() => { }, DispatcherPriority.SystemIdle);

	var bitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
	bitmap.Render(wrapper);

	var encoder = new PngBitmapEncoder();
	encoder.Frames.Add(BitmapFrame.Create(bitmap));

	Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(outputPath))!);

	using (FileStream stream = File.Create(outputPath))
	{
		encoder.Save(stream);
	}

	Console.WriteLine($"Saved {outputPath} ({size.Width}x{size.Height}, screen: {screen})");
}

// Resolves the view model for a screen out of DI and pairs its view with a test
// for "has this finished loading", since every data-backed screen fills itself
// asynchronously in its constructor.
static (FrameworkElement View, Func<bool> Loaded) Compose(IHost host, string screen)
{
	switch (screen)
	{
		case "authorization":
		{
			var viewModel = host.Services.GetRequiredService<AuthorizationVM>();
			return (new Authorization { DataContext = viewModel }, () => true);
		}
		case "registration":
		{
			var viewModel = host.Services.GetRequiredService<RegistrationVM>();
			return (new Registration { DataContext = viewModel }, () => true);
		}
		case "home":
		{
			var viewModel = host.Services.GetRequiredService<HomeVM>();
			return (new Home { DataContext = viewModel }, () => viewModel.Products?.Count > 0);
		}
		case "profile":
		{
			var viewModel = host.Services.GetRequiredService<ProfileVM>();
			return (new Profile { DataContext = viewModel }, () => viewModel.CurrentEmployee is not null);
		}
		case "statistics":
		{
			var viewModel = host.Services.GetRequiredService<StatisticsVM>();
			return (new Statistics { DataContext = viewModel }, () => viewModel.Employees?.Count > 0);
		}
		case "delivery":
		{
			var viewModel = host.Services.GetRequiredService<YourDeliveryInfoVM>();
			return (new YourDeliveryInfo { DataContext = viewModel }, () => viewModel.Orders?.Count > 0);
		}
		default:
			throw new ArgumentException($"Unknown screen \"{screen}\".");
	}
}
