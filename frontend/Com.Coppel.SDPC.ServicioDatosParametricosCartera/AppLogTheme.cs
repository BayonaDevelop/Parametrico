using Serilog.Sinks.SystemConsole.Themes;

namespace Com.Coppel.SDPC.ServicioDatosParametricosCartera;

public class AppLogTheme : ConsoleTheme
{
	private readonly Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> _styles;

	public AppLogTheme()
	{
		_styles = new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
		{
			[ConsoleThemeStyle.Text] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
			[ConsoleThemeStyle.SecondaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Gray },
			[ConsoleThemeStyle.LevelVerbose] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
			[ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
			[ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Green },
			[ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkYellow },
			[ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkRed },
			[ConsoleThemeStyle.LevelFatal] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkRed },
			[ConsoleThemeStyle.Invalid] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Red, Background = ConsoleColor.White },
			[ConsoleThemeStyle.Scalar] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
			[ConsoleThemeStyle.String] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Magenta },
			[ConsoleThemeStyle.Number] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
			[ConsoleThemeStyle.Boolean] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
			[ConsoleThemeStyle.Null] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
			[ConsoleThemeStyle.Name] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow }
		};
	}

	public override int Set(TextWriter output, ConsoleThemeStyle style)
	{
		if (_styles.TryGetValue(style, out var themeStyle))
		{
			if (themeStyle.Foreground.HasValue)
				Console.ForegroundColor = themeStyle.Foreground.Value;
			if (themeStyle.Background.HasValue)
				Console.BackgroundColor = themeStyle.Background.Value;
		}
		return 0;
	}

	public override void Reset(TextWriter output)
	{
		Console.ResetColor();
	}

	public override bool CanBuffer => false;

	protected override int ResetCharCount => 0;
}
