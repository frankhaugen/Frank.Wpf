using System.Collections;

namespace Frank.Wpf.Core;

public class KnownFontFamilyNames : IEnumerable<string>
{
    /// <inheritdoc />
    public IEnumerator<string> GetEnumerator()
    {
        yield return KnownFontFamilyName.Arial;
        yield return KnownFontFamilyName.ArialBlack;
        yield return KnownFontFamilyName.Bahnschrift;
        yield return KnownFontFamilyName.Calibri;
        yield return KnownFontFamilyName.Cambria;
        yield return KnownFontFamilyName.CambriaMath;
        yield return KnownFontFamilyName.Candara;
        yield return KnownFontFamilyName.ComicSansMS;
        yield return KnownFontFamilyName.Consolas;
        yield return KnownFontFamilyName.Constantia;
        yield return KnownFontFamilyName.Corbel;
        yield return KnownFontFamilyName.CourierNew;
        yield return KnownFontFamilyName.Ebrima;
        yield return KnownFontFamilyName.FranklinGothicMedium;
        yield return KnownFontFamilyName.Gabriola;
        yield return KnownFontFamilyName.Gadugi;
        yield return KnownFontFamilyName.Georgia;
        yield return KnownFontFamilyName.Impact;
        yield return KnownFontFamilyName.InkFree;
        yield return KnownFontFamilyName.JavaneseText;
        yield return KnownFontFamilyName.LeelawadeeUI;
        yield return KnownFontFamilyName.LucidaConsole;
        yield return KnownFontFamilyName.LucidaSansUnicode;
        yield return KnownFontFamilyName.MalgunGothic;
        yield return KnownFontFamilyName.Mangal;
        yield return KnownFontFamilyName.Marlett;
        yield return KnownFontFamilyName.MicrosoftHimalaya;
        yield return KnownFontFamilyName.MicrosoftJhengHei;
        yield return KnownFontFamilyName.MicrosoftJhengHeiUI;
        yield return KnownFontFamilyName.MicrosoftNewTaiLue;
        yield return KnownFontFamilyName.MicrosoftPhagsPa;
        yield return KnownFontFamilyName.MicrosoftSansSerif;
        yield return KnownFontFamilyName.MicrosoftTaiLe;
        yield return KnownFontFamilyName.Tahoma;
        yield return KnownFontFamilyName.TimesNewRoman;
        yield return KnownFontFamilyName.TrebuchetMS;
        yield return KnownFontFamilyName.Verdana;
        yield return KnownFontFamilyName.Webdings;
        yield return KnownFontFamilyName.Wingdings;
        yield return KnownFontFamilyName.YuGothic;
        yield return KnownFontFamilyName.YuGothicUI;
        yield return KnownFontFamilyName.JetBrainsMono;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
public static class KnownFontFamilyName
{
    public const string Arial = "Arial";
    public const string ArialBlack = "Arial Black";
    public const string Bahnschrift = "Bahnschrift";
    public const string Calibri = "Calibri";
    public const string Cambria = "Cambria";
    public const string CambriaMath = "Cambria Math";
    public const string Candara = "Candara";
    public const string ComicSansMS = "Comic Sans MS";
    public const string Consolas = "Consolas";
    public const string Constantia = "Constantia";
    public const string Corbel = "Corbel";
    public const string CourierNew = "Courier New";
    public const string Ebrima = "Ebrima";
    public const string FranklinGothicMedium = "Franklin Gothic Medium";
    public const string Gabriola = "Gabriola";
    public const string Gadugi = "Gadugi";
    public const string Georgia = "Georgia";
    public const string Impact = "Impact";
    public const string InkFree = "Ink Free";
    public const string JavaneseText = "Javanese Text";
    public const string LeelawadeeUI = "Leelawadee UI";
    public const string LucidaConsole = "Lucida Console";
    public const string LucidaSansUnicode = "Lucida Sans Unicode";
    public const string MalgunGothic = "Malgun Gothic";
    public const string Mangal = "Mangal";
    public const string Marlett = "Marlett";
    public const string MicrosoftHimalaya = "Microsoft Himalaya";
    public const string MicrosoftJhengHei = "Microsoft JhengHei";
    public const string MicrosoftJhengHeiUI = "Microsoft JhengHei UI";
    public const string MicrosoftNewTaiLue = "Microsoft New Tai Lue";
    public const string MicrosoftPhagsPa = "Microsoft PhagsPa";
    public const string MicrosoftSansSerif = "Microsoft Sans Serif";
    public const string MicrosoftTaiLe = "Microsoft Tai Le";
    public const string Tahoma = "Tahoma";
    public const string TimesNewRoman = "Times New Roman";
    public const string TrebuchetMS = "Trebuchet MS";
    public const string Verdana = "Verdana";
    public const string Webdings = "Webdings";
    public const string Wingdings = "Wingdings";
    public const string YuGothic = "Yu Gothic";
    public const string YuGothicUI = "Yu Gothic UI";
    public const string JetBrainsMono = "JetBrains Mono";
}