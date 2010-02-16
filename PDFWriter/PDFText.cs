using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFText : PDFGraphicObject
    {
        private string _text;
        private Font _font;

        /// <summary>
        /// C# 4 will introduce default parameters that will give us the possibility to improve this function.
        /// </summary>
        /// <param name="text">
        /// Tj: paint the glyphs for a string of characters.
        /// </param>
        /// <param name="color">
        /// rg: RGB color of the text. Black is 0 0 0.
        /// </param>
        /// <param name="font">
        /// Tf: set the font. Example: F13 specifies the font Helvetica.
        /// </param>
        /// <param name="fontSize">
        /// Tf: set the font size.
        /// </param>
        /// <param name="yPos">
        /// Td: specify a starting position on the page.
        /// </param>
        /// <param name="scale">
        /// Tz: set the horizontal scaling (scale ÷ 100). Scale is a number specifying the
        /// percentage of the normal width. Default value: 100 (normal width).
        /// </param>
        /// <param name="charSpace">
        /// Tc: set the character spacing: a number expressed in unscaled
        /// text space units. Character spacing is used by the Tj, TJ, and ' operators.
        /// Default value: 0
        /// </param>
        /// <param name="wordSpace">
        /// Tw: set the word spacing: a number expressed in unscaled
        /// text space units. Word spacing is used by the Tj, TJ, and ' operators. Default
        /// value: 0.
        /// </param>
        public PDFText(string text, Font font)
        {
            _text = text;
            _font = font;
        }

        public override string ToInnerPDF()
        {
            string rg = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} rg", _font.Color);
            string Tf = string.Format(System.Globalization.CultureInfo.InvariantCulture, "/{0} {1} Tf", _font.Name, _font.Size);
            string Td = string.Format(System.Globalization.CultureInfo.InvariantCulture, "0 {0} Td", 2);
            string Tj = string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0}) Tj", _text);

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
