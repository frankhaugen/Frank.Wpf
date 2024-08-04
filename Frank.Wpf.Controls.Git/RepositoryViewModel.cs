namespace Frank.Wpf.Controls.Git;

public class RepositoryViewModel
{
    public Uri Uri { get; set; }
    
    public string Name { get; set; }
    
    public bool Local { get; set; }
    
    public List<BranchViewModel> Branches { get; set; }
    
    public string DefaultBranchHash { get; set; }
}