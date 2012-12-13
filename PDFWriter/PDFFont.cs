namespace PDF
{
    /// <summary>
    /// Represents a font in the PDF world.
    /// </summary>
    public class PDFFont : PDFStructureObject
    {
        public PDFFont(string fontName, string baseFont)
        {
            FontName = fontName;
            BaseFont = baseFont;
        }

        public string FontName
        {
            get;
            private set;
        }

        private string BaseFont
        {
            get;
            set;
        }

        public override string ToInnerPDF()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFFont (
{0} 0 obj
    <<
        /Type /Font
        /Subtype /Type1
        /Name /{1}
        /BaseFont /{2}
        /Encoding /PDFDocEncoding
    >>
endobj
% )
", ObjectNumber, FontName, BaseFont);
        }
    }
}
