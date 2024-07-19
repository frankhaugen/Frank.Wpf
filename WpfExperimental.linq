<Query Kind="Program">
  <Reference Relative="Frank.Wpf.Hosting\bin\Debug\net8.0-windows\Frank.Wpf.Hosting.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Hosting\bin\Debug\net8.0-windows\Frank.Wpf.Hosting.dll</Reference>
  <Reference Relative="Frank.Wpf.Markdown.Previewer\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Previewer.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Markdown.Previewer\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Previewer.dll</Reference>
  <Namespace>Frank.Wpf.Hosting</Namespace>
  <Namespace>Frank.Wpf.Markdown.Previewer</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeUncapsulator>false</IncludeUncapsulator>
  <DisableMyExtensions>true</DisableMyExtensions>
  <CopyLocal>true</CopyLocal>
  <RuntimeVersion>8.0</RuntimeVersion>
</Query>

[STAThread]
void Main()
{
	var builder = Host.CreateWpfHostBuilder();
	builder.Services.AddSingleton<MarkdownPreviewer>();
	var host = builder.Build<LinqPadWindow>();
	
	host.Run();
}

public class LinqPadWindow : MainWindow
{
	public LinqPadWindow(MarkdownPreviewer markdownPreviewer)
	{
		markdownPreviewer.Height = 300;
		var stackPanel = new System.Windows.Controls.StackPanel();

		var textbox = new System.Windows.Controls.TextBox()
		{
			AcceptsReturn = true,
			AcceptsTab = true,
			VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto,
			MinLines = 10,
			MaxLines = 15,
			FontFamily = new System.Windows.Media.FontFamily("Consolas"),
			TextWrapping = System.Windows.TextWrapping.Wrap,
			HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto
		};

		textbox.TextChanged += (sender, args) =>
		{
			var text = ((System.Windows.Controls.TextBox)sender).Text;
			//Dispatcher.Invoke(() =>
			//			{
							markdownPreviewer.Markdown.Markdown = text;
						//});
		};

		stackPanel.Children.Add(textbox);
		stackPanel.Children.Add(markdownPreviewer);
		Content = stackPanel;
	}
}
