using System.Diagnostics;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Compass;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}

public class App : Application
{
    public double EconScore { get; set; } = 0.0;
    public double AuthLibScore { get; set; } = 0.0;
    public List<Question> Questions { get; } = new QuestionCompiler().Build();

    public override void Initialize()
    {
        Debug.WriteLine("App init method called.");
        Console.WriteLine("App init method called.");
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Console.WriteLine("OnFrameworkInitializationCompleted start");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Console.WriteLine("Creating MainWindow");
            desktop.MainWindow = new MainWindow();
            Console.WriteLine("Showing MainWindow");
            desktop.MainWindow.Show();
            Console.WriteLine("MainWindow shown");
        }
        else
        {
            Console.WriteLine("Application lifetime is not classic desktop");
        }

        base.OnFrameworkInitializationCompleted();
    }
}

public class MainWindow : Window
{
    private static readonly FontFamily StandardFont = FontFamily.Default;
    private static readonly FontFamily BoldFont = FontFamily.Default;

    private readonly StackPanel _root = new();
    private readonly App _app = (App)Application.Current!;
    private readonly List<Question> _questions = new QuestionCompiler().Build();
    private int _currentQuestionIndex;

    public MainWindow()
    {
        Title = "Political Alignment Quiz";
        Width = 800;
        Height = 600;
        CanResize = true;
        FontFamily = StandardFont;

        Content = _root;
        ShowStartScreen();
    }

    private void ShowStartScreen()
    {
        _root.Children.Clear();
        _root.Margin = new Thickness(24, 40, 24, 0);

        var title = new TextBlock
        {
            Text = "Political Alignment Quiz",
            FontSize = 32,
            FontFamily = BoldFont,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 20)
        };

        var subtitle = new TextBlock
        {
            Text = "Answer the questions to see where you fall on the political spectrum.",
            FontSize = 18,
            FontFamily = StandardFont,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 6)
        };

        var sourceLine = new TextBlock
        {
            Text = "Inspired by politicalcompass.org",
            FontSize = 12,
            FontFamily = StandardFont,
            FontStyle = FontStyle.Italic,
            Foreground = Brushes.Gray,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 20)
        };

        var startButton = new Button
        {
            Content = "Start",
            Width = 220,
            Height = 52,
            HorizontalAlignment = HorizontalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            FontFamily = BoldFont,
            FontSize = 22,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 20, 0, 0),
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(8)
        };

        startButton.Click += (_, _) => ShowQuestionScreen();

        _root.Children.Add(title);
        _root.Children.Add(subtitle);
        _root.Children.Add(sourceLine);
        _root.Children.Add(startButton);
    }

    private void ShowQuestionScreen()
    {
        _root.Children.Clear();

        if (_currentQuestionIndex >= _questions.Count)
        {
            ShowResultsScreen();
            return;
        }

        var question = _questions[_currentQuestionIndex];

        var text = new TextBlock
        {
            Text = question.ToString(),
            FontFamily = StandardFont,
            FontSize = 24,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 20)
        };

        var optionsPanel = new StackPanel
        {
            Margin = new Thickness(24, 0, 24, 0)
        };

        foreach (var option in question.Options)
        {
            var button = new Button
            {
                Content = option.Text,
                FontFamily = StandardFont,
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12, 8, 12, 8)
            };

            button.Click += (_, _) =>
            {
                _app.EconScore += question.IsEcon ? option.Value : 0.0;
                _app.AuthLibScore += !question.IsEcon ? option.Value : 0.0;
                _currentQuestionIndex++;
                ShowQuestionScreen();
            };

            optionsPanel.Children.Add(button);
        }

        _root.Children.Add(text);
        _root.Children.Add(optionsPanel);
    }

    private void ShowResultsScreen()
    {
        _root.Children.Clear();

        const double chartSize = 1000;
        var scale = Math.Min((this.Width - 120) / chartSize, (this.Height - 220) / chartSize);
        var displaySize = Math.Max(240, chartSize * scale);

        var chartCanvas = new Canvas
        {
            Width = displaySize,
            Height = displaySize,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 0),
            Background = Brushes.Transparent
        };

        var gridImage = new Image
        {
            Width = displaySize,
            Height = displaySize,
            Source = LoadImage("grid.png"),
            Stretch = Stretch.Fill
        };

        var markerDiameter = 18 * scale;
        var dot = new Border
        {
            Width = markerDiameter,
            Height = markerDiameter,
            Background = Brushes.Red,
            BorderBrush = Brushes.DarkRed,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(markerDiameter / 2),
            ZIndex = 10
        };

        var econValue = SnapToGrid(_app.EconScore);
        var authLibValue = SnapToGrid(_app.AuthLibScore);

        var x = MapToCanvas(econValue, -14, 14, displaySize);
        var y = displaySize - MapToCanvas(authLibValue, -14, 14, displaySize);

        Canvas.SetLeft(dot, x - (dot.Width / 2));
        Canvas.SetTop(dot, y - (dot.Height / 2));

        chartCanvas.Children.Add(gridImage);
        chartCanvas.Children.Add(dot);

        var econLabel = GetEconLabel(econValue);
        var authLibLabel = GetAuthLibLabel(authLibValue);

        var scoreText = new TextBlock
        {
            Text = $"Econ: {econValue:F1} ({econLabel})   Auth/Lib: {authLibValue:F1} ({authLibLabel})",
            FontFamily = BoldFont,
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 0),
            Foreground = Brushes.Black
        };

        var restartButton = new Button
        {
            Content = "Restart",
            Width = 200,
            Height = 48,
            HorizontalAlignment = HorizontalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 20, 0, 0),
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(8),
            FontFamily = BoldFont,
            FontWeight = FontWeight.Bold,
            FontSize = 20
        };

        restartButton.Click += (_, _) =>
        {
            _currentQuestionIndex = 0;
            _app.EconScore = 0.0;
            _app.AuthLibScore = 0.0;
            ShowStartScreen();
        };

        var stack = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Width = this.Width - 60,
            Spacing = 12
        };

        scoreText.TextAlignment = TextAlignment.Center;

        stack.Children.Add(chartCanvas);
        stack.Children.Add(scoreText);
        stack.Children.Add(restartButton);

        _root.Children.Add(stack);
    }

    private static Bitmap LoadImage(string fileName)
    {
        var candidates = new[]
        {
            Path.Combine(AppContext.BaseDirectory, fileName),
            Path.Combine(Directory.GetCurrentDirectory(), fileName),
            Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net10.0", fileName)
        };

        foreach (var path in candidates)
        {
            if (File.Exists(path))
            {
                return new Bitmap(path);
            }
        }

        throw new FileNotFoundException($"Could not find image file: {fileName}");
    }

    private static double MapToCanvas(double value, double min, double max, double size)
    {
        var clamped = Math.Clamp(value, min, max);
        var range = max - min;
        if (range == 0)
        {
            return size / 2;
        }

        return ((clamped - min) / range) * size;
    }

    private static double SnapToGrid(double value)
    {
        return Math.Round(value * 2.0) / 2.0;
    }

    private static string GetEconLabel(double value)
    {
        if (value <= -10) return "Far Left";
        if (value <= -6) return "Left";
        if (value <= -2) return "Center Left";
        if (value <= 2) return "Center";
        if (value <= 6) return "Center Right";
        if (value <= 10) return "Right";
        return "Far Right";
    }

    private static string GetAuthLibLabel(double value)
    {
        if (value >= 10) return "Very Authoritarian";
        if (value >= 6) return "Authoritarian";
        if (value >= 2) return "Slightly Authoritarian";
        if (value >= -2) return "Center";
        if (value >= -6) return "Slightly Libertarian";
        if (value >= -10) return "Libertarian";
        return "Very Libertarian";
    }
}
