using System.Windows.Controls;
using TreeView = System.Windows.Controls.TreeView;

namespace Frank.Wpf.Core;

public class TreeViewWalker
{
    public void Walk(TreeView treeView, Func<TreeViewItem, bool> action)
    {
        foreach (var item in treeView.Items)
        {
            if (item is TreeViewItem treeViewItem)
            {
                WalkItem(treeViewItem, action);
            }
        }
    }
        
    private void WalkItem(TreeViewItem current, Func<TreeViewItem, bool> action)
    {
        if (action(current))
        {
            foreach (var item in current.Items)
            {
                if (item is TreeViewItem treeViewItem)
                {
                    WalkItem(treeViewItem, action);
                }
            }
        }
    }
}