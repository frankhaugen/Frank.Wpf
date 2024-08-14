using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Core;

namespace Frank.Wpf.Tests.App.Windows;

public class TextBoxWithLineNumbersWindow : Window
{
    private readonly StackPanel _stackPanel = new();
    private readonly TextBoxWithLineNumbers _textBoxWithLineNumbers;
    private readonly Dropdown<FontFamily> _fontFamilyDropdown;
    public TextBoxWithLineNumbersWindow()
    {
        Title = "Text Box With Line Numbers Window";
        Width = 800;
        Height = 600;
        _textBoxWithLineNumbers = new TextBoxWithLineNumbers()
        {
            Text = "Line 1\nLine 2\nLine 3\nLine 4\nLine 5\nLine 6\nLine 7\nLine 8\nLine 9\nLine 10",
            FontSize = 16,
            FontFamily = new FontFamily(KnownFontFamilyName.ComicSansMS),
        };
        
        _fontFamilyDropdown = new Dropdown<FontFamily>()
        {
            Items = new KnownFontFamilies(),
            DisplayFunc = x => x.Source,
            SelectionChangedAction = x => _textBoxWithLineNumbers.FontFamily = x,
        };
        
        _stackPanel.Children.Add(_fontFamilyDropdown);
        _stackPanel.Children.Add(_textBoxWithLineNumbers);
        
        Content = _stackPanel;
    }
}