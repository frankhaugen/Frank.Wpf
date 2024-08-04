namespace Frank.Wpf.Controls.Git;

public class CommitViewModel
{
    public GitHash Hash { get; set; }
    
    public string ShortMessage { get; set; }
    
    public string Message { get; set; }
    
    public string Author { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
    
    public GitHash BranchHash { get; set; }
}