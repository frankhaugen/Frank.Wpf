namespace Frank.Wpf.Controls.Git;

public readonly struct GitHash
{
    public string Value { get; }

    public GitHash(string value)
    {
        Value = value;
    }

    public static implicit operator string(GitHash hash) => hash.Value;

    public static implicit operator GitHash(string value) => new(value);
}