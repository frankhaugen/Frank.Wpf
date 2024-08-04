using System.Windows.Controls;

namespace Frank.Wpf.Controls.Git;

internal class CommitMessageViewModel : StackPanel
{
    public CommitMessageViewModel()
    {
        Orientation = Orientation.Vertical;
        Children.Add(new TextBlock { Text = ShortMessage });
        Children.Add(new TextBlock { Text = Hash });
        Children.Add(new TextBlock { Text = Author });
        Children.Add(new TextBlock { Text = Timestamp.ToString("s") });
        Children.Add(new TextBlock { Text = Message });
    }
    
    public string? Message { get; set; }
    
    public string? Author { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
    
    public string? Hash { get; set; }
    
    public string? ShortMessage { get; set; }
    
    public string? Branch { get; set; }
}