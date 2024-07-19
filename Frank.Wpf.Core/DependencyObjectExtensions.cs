using System.Windows;
using System.Windows.Media;
using GroupBox = System.Windows.Controls.GroupBox;

namespace Frank.Wpf.Core;

public static class DependencyObjectExtensions
{
    public static IEnumerable<T> GetDecendants<T>(this DependencyObject obj) where T : DependencyObject
    {
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            foreach (var child in GetDecendants<T>(VisualTreeHelper.GetChild(obj, i)))
                if (child is T)
                    yield return child;
    }

    public static IEnumerable<T> GetAllChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj == null) yield break;
        var depObjCount = VisualTreeHelper.GetChildrenCount(depObj);
        for (var i = 0; i < depObjCount; i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);
            if (child is T dependencyObject)
                yield return dependencyObject;
            
            if (child is GroupBox { Content: T o })
            {
                yield return (T)child;
                child = o;
            }

            foreach (var childOfChild in GetAllChildren<T>(child))
                yield return childOfChild;
        }
    }


    /// <summary>
    /// Finds a Child of a given item in the visual tree.
    /// All credits to author CrimsonX: https://stackoverflow.com/questions/636383/how-can-i-find-wpf-controls-by-name-or-type
    /// </summary>
    /// <param name="parent">A direct parent of the queried item.</param>
    /// <typeparam name="T">The type of the queried item.</typeparam>
    /// <param name="childName">x:Name or Name of child. </param>
    /// <returns>The first parent item that matches the submitted type parameter. 
    /// If not matching item can be found, 
    /// a null parent is being returned.</returns>
    public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
    {
        // Confirm parent and childName are valid. 
        if (parent == null)
            return null;

        T foundChild = null;

        var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (var i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            // If the child is not of the request child type child
            if (child is not T childType)
            {
                // recursively drill down the tree
                foundChild = FindChild<T>(child, childName);

                // If the child is found, break so we do not overwrite the found child. 
                if (foundChild != null)
                    break;
            }
            else if (!string.IsNullOrEmpty(childName))
            {
                // If the child's name is set for search
                if (childType is not FrameworkElement frameworkElement || frameworkElement.Name != childName) continue;
                // if the child's name is of the request name
                foundChild = childType;
                break;
            }
            else
            {
                // child element found.
                foundChild = childType;
                break;
            }
        }

        return foundChild;
    }
}