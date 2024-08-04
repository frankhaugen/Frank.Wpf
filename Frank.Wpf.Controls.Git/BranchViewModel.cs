namespace Frank.Wpf.Controls.Git;

public class BranchViewModel
{
    public GitHash Hash { get; set; }
    
    public string Name { get; set; }
    
    public bool Current { get; set; }
    
    public bool Default { get; set; }
    
    public List<CommitViewModel> Commits { get; set; }
}