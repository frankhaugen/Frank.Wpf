namespace Frank.Wpf.Core.Beatification;

public class SimpleTrigger : IBeautificationTrigger
{
    private readonly Func<string, bool> _shouldBeautify;

    public SimpleTrigger(Func<string, bool> shouldBeautify)
    {
        _shouldBeautify = shouldBeautify;
    }

    public bool ShouldBeautify(string code)
    {
        return _shouldBeautify(code);
    }
}