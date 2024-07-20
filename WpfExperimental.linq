<Query Kind="Program">
  <Reference Relative="Frank.Wpf.Controls.Grid\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Grid.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Controls.Grid\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Grid.dll</Reference>
  <Reference Relative="Frank.Wpf.Controls.Pages\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Pages.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Controls.Pages\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Pages.dll</Reference>
  <Reference Relative="Frank.Wpf.Dialogs\bin\Debug\net8.0-windows\Frank.Wpf.Dialogs.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Dialogs\bin\Debug\net8.0-windows\Frank.Wpf.Dialogs.dll</Reference>
  <Reference Relative="Frank.Wpf.Hosting\bin\Debug\net8.0-windows\Frank.Wpf.Hosting.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Hosting\bin\Debug\net8.0-windows\Frank.Wpf.Hosting.dll</Reference>
  <Reference Relative="Frank.Wpf.Markdown.Editor\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Editor.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Markdown.Editor\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Editor.dll</Reference>
  <Reference Relative="Frank.Wpf.Markdown.Previewer\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Previewer.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Markdown.Previewer\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Previewer.dll</Reference>
  <NuGetReference>MdXaml</NuGetReference>
  <Namespace>Frank.Wpf.Controls.Grid</Namespace>
  <Namespace>Frank.Wpf.Controls.Pages</Namespace>
  <Namespace>Frank.Wpf.Dialogs</Namespace>
  <Namespace>Frank.Wpf.Hosting</Namespace>
  <Namespace>Frank.Wpf.Markdown.Editor</Namespace>
  <Namespace>Frank.Wpf.Markdown.Previewer</Namespace>
  <Namespace>MdXaml</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <IncludeUncapsulator>false</IncludeUncapsulator>
  <DisableMyExtensions>true</DisableMyExtensions>
  <CopyLocal>true</CopyLocal>
  <RuntimeVersion>8.0</RuntimeVersion>
</Query>

[STAThread]
void Main()
{
	var builder = Host.CreateWpfHostBuilder();
	var host = builder.Build<LinqPadWindow>();
	
	host.Run();
}

public class LinqPadWindow : MainWindow
{
	public LinqPadWindow()
	{
		var grid = new Frank.Wpf.Controls.Grid.Grid(3, 3);

		var editor = new MarkdownEditor();
		var previewer = new MarkdownPreviewer();
		var pageFrame = new PageFrame();

		pageFrame.SetPage(new Page()
		{
			Content = new Label() {
				Content = "Hello World!",
				MinWidth = 256,
				MaxHeight = 128
			}
		});

		grid.SetCellContent(0, 0, editor);
		grid.SetCellContent(1, 1, pageFrame);
		grid.SetCellContent(2, 2, previewer);
		editor.Markdown = MyText.TestMarkdown;
		editor.MarkdownChanged += (sender, args) => previewer.Markdown = editor.Markdown;
		Content = grid;
	}
}

static class MyText
{
	public static string TestMarkdown = """
										# My Code

										Some code:

										```csharp
										public static void Main()
										{
										   Console.WriteLine("Hello World!");
										}
										```
										
										""";
}