using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class DropdownWindow : Window
{

    private readonly Dropdown<MyClass> _dropdown;

    public DropdownWindow()
    {
        _dropdown = new Dropdown<MyClass>()
        {
            Items = new List<MyClass>
            {
                new MyClass { Id = Guid.NewGuid(), Name = "Frank", Age = 30 },
                new MyClass { Id = Guid.NewGuid(), Name = "John", Age = 40 },
                new MyClass { Id = Guid.NewGuid(), Name = "Jane", Age = 50 }
            },
            DisplayFunc = x => x.Name,
            SelectionChangedAction = x => { MessageBox.Show($"Selected {x.Name} with Id {x.Id} and Age {x.Age}"); }
        };
        
        var stackPanel = new StackPanel();
        stackPanel.Children.Add(_dropdown);
        
        Content = stackPanel;
    }

    private class MyClass
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }
    }
}