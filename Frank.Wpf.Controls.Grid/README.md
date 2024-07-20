# Frank.Wpf.Controls.Grid

A simple WPF grid control that supports row and column definitions for easy layout of controls.

## Usage

```csharp
using Frank.Wpf.Controls.Grid;

public class MyWindow : Window
{
    public MyWindow()
    {
        var grid = new Grid(3, 3);

        var button1 = new Button { Content = "Button 1" };
        var button2 = new Button { Content = "Button 2" };
        var button3 = new Button { Content = "Button 3" };
        var button4 = new Button { Content = "Button 4" };

        grid.SetCellContent(0, 0, button1);
        grid.SetCellContent(0, 1, button2);
        grid.SetCellContent(1, 0, button3);
        grid.SetCellContent(1, 1, button4);
        
        Content = grid;
    }
}
```