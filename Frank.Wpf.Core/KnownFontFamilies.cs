using System.Collections;
using FontFamily = System.Windows.Media.FontFamily;

namespace Frank.Wpf.Core;

public class KnownFontFamilies : IEnumerable<FontFamily>
{
    /// <inheritdoc />
    public IEnumerator<FontFamily> GetEnumerator()
    {
        yield return new FontFamily(KnownFontFamilyName.Arial);
        yield return new FontFamily(KnownFontFamilyName.ArialBlack);
        yield return new FontFamily(KnownFontFamilyName.Bahnschrift);
        yield return new FontFamily(KnownFontFamilyName.Calibri);
        yield return new FontFamily(KnownFontFamilyName.Cambria);
        yield return new FontFamily(KnownFontFamilyName.CambriaMath);
        yield return new FontFamily(KnownFontFamilyName.Candara);
        yield return new FontFamily(KnownFontFamilyName.ComicSansMS);
        yield return new FontFamily(KnownFontFamilyName.Consolas);
        yield return new FontFamily(KnownFontFamilyName.Constantia);
        yield return new FontFamily(KnownFontFamilyName.Corbel);
        yield return new FontFamily(KnownFontFamilyName.CourierNew);
        yield return new FontFamily(KnownFontFamilyName.Ebrima);
        yield return new FontFamily(KnownFontFamilyName.FranklinGothicMedium);
        yield return new FontFamily(KnownFontFamilyName.Gabriola);
        yield return new FontFamily(KnownFontFamilyName.Gadugi);
        yield return new FontFamily(KnownFontFamilyName.Georgia);
        yield return new FontFamily(KnownFontFamilyName.Impact);
        yield return new FontFamily(KnownFontFamilyName.InkFree);
        yield return new FontFamily(KnownFontFamilyName.JavaneseText);
        yield return new FontFamily(KnownFontFamilyName.LeelawadeeUI);
        yield return new FontFamily(KnownFontFamilyName.LucidaConsole);
        yield return new FontFamily(KnownFontFamilyName.LucidaSansUnicode);
        yield return new FontFamily(KnownFontFamilyName.MalgunGothic);
        yield return new FontFamily(KnownFontFamilyName.Mangal);
        yield return new FontFamily(KnownFontFamilyName.Marlett);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftHimalaya);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftJhengHei);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftJhengHeiUI);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftNewTaiLue);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftPhagsPa);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftSansSerif);
        yield return new FontFamily(KnownFontFamilyName.MicrosoftTaiLe);
        yield return new FontFamily(KnownFontFamilyName.Tahoma);
        yield return new FontFamily(KnownFontFamilyName.TimesNewRoman);
        yield return new FontFamily(KnownFontFamilyName.TrebuchetMS);
        yield return new FontFamily(KnownFontFamilyName.Verdana);
        yield return new FontFamily(KnownFontFamilyName.Webdings);
        yield return new FontFamily(KnownFontFamilyName.Wingdings);
        yield return new FontFamily(KnownFontFamilyName.YuGothic);
        yield return new FontFamily(KnownFontFamilyName.YuGothicUI);
        yield return new FontFamily(KnownFontFamilyName.JetBrainsMono);
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}