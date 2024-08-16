namespace Frank.Wpf.Core.Beatification;

public interface IBeautificationTrigger
{
    bool ShouldBeautify(string text);
}