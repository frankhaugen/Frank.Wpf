using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class DropdownWindow : Window
{

    private readonly Dropdown<MyClass?> _dropdown;

    public DropdownWindow()
    {
        Title = "Dropdown Window";
        Width = 400;
        Height = 300;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        _dropdown = new Dropdown<MyClass?>()
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
        var showSelectedButton = new Button()
        {
            Content = "Show Selected",
            Margin = new Thickness(0, 10, 0, 0)
        };
        showSelectedButton.Click += (sender, args) =>
        {
            var selected = _dropdown.SelectedItem;
            MessageBox.Show($"Selected {selected.Name} with Id {selected.Id} and Age {selected.Age}");
        };
        
        var setSelectionButton = new Button()
        {
            Content = "Set Selection",
            Margin = new Thickness(0, 10, 0, 0)
        };
        setSelectionButton.Click += (sender, args) =>
        {
            var random = new Random();
            var randomIndex = random.Next(0, _dropdown.Items.Count());
            MyClass? randomItem = null;
            while (randomItem == null)
            {
                var item = _dropdown.Items.ElementAt(randomIndex);
                if (item != _dropdown.SelectedItem)
                {
                    randomItem = item;
                }
                else
                {
                    randomIndex = random.Next(0, _dropdown.Items.Count());
                }
            }
            _dropdown.SetSelectedItem(randomItem);
        };
        
        var stackPanel = new StackPanel();
        stackPanel.Children.Add(_dropdown);
        stackPanel.Children.Add(showSelectedButton);
        stackPanel.Children.Add(setSelectionButton);
        
        Content = stackPanel;
    }

    private class MyClass
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }
    }
}