using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Frank.Wpf.Core.Beatification;

public class XmlBeautifier : TextBeautifierBase
{
    public override string Beautify(string text)
    {
        var stringBuilder = new StringBuilder();

        var element = XElement.Parse(text);

        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Indent = true,
            NewLineOnAttributes = true
        };

        using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
        {
            element.Save(xmlWriter);
        }

        return stringBuilder.ToString();
    }
}