<Query Kind="Program">
  <Reference Relative="Frank.Wpf.Controls.Git\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Git.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Controls.Git\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Git.dll</Reference>
  <Reference Relative="Frank.Wpf.Controls.Grid\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Grid.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Controls.Grid\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Grid.dll</Reference>
  <Reference Relative="Frank.Wpf.Controls.Pages\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Pages.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Controls.Pages\bin\Debug\net8.0-windows\Frank.Wpf.Controls.Pages.dll</Reference>
  <Reference Relative="Frank.Wpf.Dialogs\bin\Debug\net8.0-windows\Frank.Wpf.Dialogs.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Dialogs\bin\Debug\net8.0-windows\Frank.Wpf.Dialogs.dll</Reference>
  <Reference Relative="Frank.Wpf.Hosting\bin\Debug\net8.0-windows\Frank.Wpf.Hosting.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Hosting\bin\Debug\net8.0-windows\Frank.Wpf.Hosting.dll</Reference>
  <Reference Relative="Frank.Wpf.Markdown.Editor\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Editor.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Markdown.Editor\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Editor.dll</Reference>
  <Reference Relative="Frank.Wpf.Markdown.Previewer\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Previewer.dll">D:\frankrepos\Frank.Wpf\Frank.Wpf.Markdown.Previewer\bin\Debug\net8.0-windows\Frank.Wpf.Markdown.Previewer.dll</Reference>
  <NuGetReference>LibGit2Sharp</NuGetReference>
  <NuGetReference>MdXaml</NuGetReference>
  <Namespace>Frank.Wpf.Controls.Git</Namespace>
  <Namespace>Frank.Wpf.Controls.Grid</Namespace>
  <Namespace>Frank.Wpf.Controls.Pages</Namespace>
  <Namespace>Frank.Wpf.Dialogs</Namespace>
  <Namespace>Frank.Wpf.Hosting</Namespace>
  <Namespace>Frank.Wpf.Markdown.Editor</Namespace>
  <Namespace>Frank.Wpf.Markdown.Previewer</Namespace>
  <Namespace>LibGit2Sharp</Namespace>
  <Namespace>MdXaml</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
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
		var repository = new Repository(@"D:\frankrepos\Frank.GitKit");

		RepositoryViewModel repositoryViewModel = MapToViewModel(repository);
		
		var gitFlow = new GitFlow(repositoryViewModel);
		
		Content = gitFlow;
	}

	RepositoryViewModel MapToViewModel(Repository repository)
	{
		var commits = repository.Branches.SelectMany(b =>
		 b.Commits.Select(c =>
		 new CommitViewModel
		 {
		 	ShortMessage = c.MessageShort,
		 	Message = c.Message,
		 	Author = c.Author.Email,
		 	Timestamp = c.Author.When,
			 BranchHash = b.CanonicalName
		 })
		).ToList();

		var branches = repository.Branches.Select(b => new BranchViewModel
		{
			Name = b.FriendlyName,
			Commits = commits.Where(c => c.BranchHash == b.CanonicalName).ToList()
		}).ToList();
		
		return new RepositoryViewModel
		{
			Branches = branches
		};
	}
}
