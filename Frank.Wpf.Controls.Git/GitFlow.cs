using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Line = System.Windows.Shapes.Line;

namespace Frank.Wpf.Controls.Git;

public class GitFlow : UserControl
{
    private readonly Canvas _canvas;
    private readonly RepositoryViewModel _repository;

    public GitFlow(RepositoryViewModel repository)
    {
        _repository = repository;
        _canvas = new Canvas();
        Content = _canvas;
        Loaded += GitFlow_Loaded;
        
        DrawGitFlow();
    }

    private void DrawGitFlow()
    {
        var defaultBranch = _repository.Branches.FirstOrDefault(b => b.Default);
        var branches = _repository.Branches.ToList();
        var commits = _repository.Branches.SelectMany(b => b.Commits).ToList();
        
        
        var branchSpacing = 100;
        var commitSpacing = 50;
        
        var startX = 50;
        var startY = 50;

        foreach (var branch in branches)
        {
            var y = startY + branchSpacing * branches.IndexOf(branch);
            DrawBranch(startX, y, branch.Name, Colors.Black);
            
            foreach (var commit in branch.Commits)
            {
                var x = startX + commitSpacing * branch.Commits.IndexOf(commit);
                DrawCommit(x, y, Colors.Black, new CommitMessageViewModel
                {
                    Message = commit.ShortMessage,
                    Author = commit.Author,
                    Timestamp = commit.Timestamp,
                    Hash = commit.Hash
                });
            }
        }
    }

    private void DrawBranch(double x, double y, string name, Color color)
    {
        Line branchLine = new()
        {
            X1 = x,
            Y1 = y,
            X2 = x + 600,
            Y2 = y,
            Stroke = new SolidColorBrush(color),
            StrokeThickness = 2
        };

        _canvas.Children.Add(branchLine);

        TextBlock branchName = new()
        {
            Text = name,
            Foreground = new SolidColorBrush(color),
            FontSize = 14
        };

        Canvas.SetLeft(branchName, x - 40);
        Canvas.SetTop(branchName, y - 10);
        _canvas.Children.Add(branchName);
    }

    private void DrawCommit(double x, double y, Color color, CommitMessageViewModel message)
    {
        Ellipse commit = new()
        {
            Width = 10,
            Height = 10,
            Fill = new SolidColorBrush(color),
            ToolTip = message
        };

        Canvas.SetLeft(commit, x - 5);
        Canvas.SetTop(commit, y - 5);
        _canvas.Children.Add(commit);
    }

    private void GitFlow_Loaded(object sender, RoutedEventArgs e)
    {
        // Logic to retrieve and visualize git commits would go here.

        // This is just an example. Update with real data.
        var commits = new List<CommitViewModel>(); // Assume Commit is a class defined to hold information about a Git commit.

        foreach (var commit in commits)
        {
            var commitVisual = CreateCommitVisual(commit);
            _canvas.Children.Add(commitVisual);
        }
    }

    private UIElement CreateCommitVisual(CommitViewModel commit)
    {
        // Creation of a UI element to represent the commit goes here.
        // This is just a placeholder. Update it with the actual implementation.
        return new TextBlock { Text = commit.Message }; // Assuming Commit has a property called Message.
    }
}