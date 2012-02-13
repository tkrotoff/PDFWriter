using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// Writes a text/string inside a PDF document.
    /// </summary>
    class PDFText : PDFGraphicObject
    {
        private string _text;
        private Font _font;

        public PDFText(string text, Font font)
        {
            _text = text;
            _font = font;
        }

        public override string ToInnerPDF()
        {
            //rg: RGB color of the text. Black is 0 0 0
            string rg = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} rg", _font.Color);

            //Tf: set the font name and size
            //Example: F13 specifies the font Helvetica
            string Tf = string.Format(System.Globalization.CultureInfo.InvariantCulture, "/{0} {1} Tf", _font.Name, _font.Size);

            //Td: specify a starting position on the page
            string Td = string.Format(System.Globalization.CultureInfo.InvariantCulture, "0 {0} Td", 2);

            //Tj: paint the glyphs for a string of characters
            string Tj = string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0}) Tj", _text);

            //Tw: set the word spacing: a number expressed in unscaled
            //text space units. Word spacing is used by the Tj, TJ, and ' operators.
            //Default value: 0

            //Tc: set the character spacing: a number expressed in unscaled
            //text space units. Character spacing is used by the Tj, TJ, and ' operators.
            //Default value: 0

            //Tz: set the horizontal scaling (scale ÷ 100)
            //Scale is a number specifying the percentage of the normal width
            //Default value: 100 (normal width)

            //BT means begin text and ET means... end text

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFText (
    BT
       {0}
       {1}
       {2}
       {3}
    ET
% )
", rg, Tf, Td, Tj
            );
        }
    }
}
