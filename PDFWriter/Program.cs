using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PDFWriter
{
    class Program
    {
        static readonly string BLACK = "0 0 0";
        /*static readonly string BLUE = "0 0 1";
        static readonly string CYAN = "0 1 1";
        static readonly string GRAY = "0.5 0.5 0.5";
        static readonly string GREEN = "0 0.5 0";
        static readonly string LIME = "0 1 0";
        static readonly string MAGENTA = "1 0 1";
        static readonly string MAROON = "0.5 0 0";
        static readonly string NAVY = "0 0 0.5";
        static readonly string OLIVE = "0.5 0.5 0";
        static readonly string PURPLE = "0.5 0 0.5";
        static readonly string RED = "1 0 0";
        static readonly string SILVER = "0.75 0.75 0.75";
        static readonly string TEAL = "0 0.5 0.5";
        static readonly string WHITE = "1 1 1";
        static readonly string YELLOW = "1 1 0";*/

        static StringBuilder _pdf = new StringBuilder();

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
        static string CreateText(string text, int yPos, string color, string font, int fontSize,
                int scale, int charSpace, int wordSpace) {

            string rg = string.Empty;
            if (color != BLACK)
            {
                //Black is the default value so no need to write it down
                rg = string.Format("{0} rg", color);
            }

            string Tf = string.Empty;
            if (!string.IsNullOrEmpty(font))
            {
                //If a font name is specified
                //FIXME and what if we specify the font size without specifying the font name?
                Tf = string.Format("/{0} {1} Tf", font, fontSize);
            }

            string Td = string.Empty;
            if (yPos != 0)
            {
                //xPos = 0 and yPos = 0 are the default values
                Td = string.Format("0 {0} Td", yPos);
            }

            string Tz = string.Empty;
            if (scale != 100)
            {
                //100 is the default value (normal width)
                Tz = string.Format("{0} Tz", scale);
            }

            string Tc = string.Empty;
            if (charSpace != 0)
            {
                //0 is the default value
                Tc = string.Format("{0} Tc", charSpace);
            }

            string Tw = string.Empty;
            if (wordSpace != 0)
            {
                //0 is the default value
                Tw = string.Format("{0} Tw", wordSpace);
            }

            string Tj = string.Format("({0}) Tj", text);

            return string.Format(@"
% CreateText (
   BT
       % Text color
       {0}
       % Font name & size
       {1}
       % Text x & y position
       {2}
       % Horizontal scaling
       {3}
       % Character spacing
       {4}
       % Word spacing
       {5}
       % Text to show
       {6}
    ET
%)", rg, Tf, Td, Tz, Tc, Tw, Tj
            );
        }

        static string CreateMark(string box, int xPos, int yPos, int hAlign, int vAlign)
        {
            return string.Format(@"
% CreateMark (
    % translation x=?,y=?
    q 1 0 0 1 {0} {1} cm
    {2}
    % remove translation x=?,y=?
    Q
%)", xPos, yPos, box
            );
        }

        static string CreateTextCell(string text, int yPos, string color, string font, int fontSize)
        {
            return string.Format(@"
% CreateTextCell (
    BT
        {0} rg
        /{1} {2} Tf
        0 {3} Td
        ({4}) Tj
    ET
%)", color, font, fontSize, yPos, text
            );
        }

        static string CreateRow(string box, int width)
        {
            return string.Format(@"
% CreateRow (
    % translation width
    q 1 0 0 1 {0} 0 cm
    {1}
    % remove translation width
    Q
%)", width, box
            );

        }

        static void Main(string[] args)
        {
            _pdf.AppendLine("%PDF-1.3");

            // Catalog Dictionnary
            _pdf.AppendLine(@"
% Catalog Dictionnary.
% Contains references to other objects defining the document's
% contents, outline, article threads ect...
1 0 obj
    << /Type /Catalog
        /Outlines 2 0 R
        /Pages 3 0 R
    >>
endobj"
            );

            // Document Outline
            _pdf.AppendLine(@"
% Document Outline.
% Allows the user to navigate interactively from one part of
% the document to another.
% The outline consists of a tree-structured hierarchy of
% outline items (sometimes called bookmarks), which serve as a
% 'visual table of contents' to display the document's
% structure to the user.
2 0 obj
    << /Type /Outlines
        /Count 0
    >>
endobj"
            );

            // Page Tree
            _pdf.AppendLine(@"
% Page Tree.
3 0 obj
    << /Type /Pages
        % Kids: an array of indirect references to the immediate children of this node.
        % The children may be page objects or other page tree nodes.
        /Kids [4 0 R]
        % Count: the number of leaf nodes (page objects) that are descendants of this
        % node within the page tree.
        /Count 1
    >>
endobj"
            );

            // Page Node
            _pdf.AppendLine(@"
% Page Node.
4 0 obj
    << /Type /Page
        /Parent 3 0 R
        /MediaBox [0 0 612 792]
        /Contents 5 0 R
        /Resources << /ProcSet 6 0 R >>
    >>
endobj"
            );

            _pdf.AppendLine(@"
5 0 obj
    << /Length 73 >>
stream"
            );

            //string text = CreateText("Report", 2, BLACK, "FH", 9, 100, 0, 0);
            //string mark = CreateMark(text, 20, 766, 0, 0);
            //_pdf.Append(mark);

            string cell = CreateTextCell("Symbol", 2, BLACK, "FHB", 9);
            string row = CreateRow(cell, 0);
            _pdf.Append(row);

            _pdf.AppendLine(@"
endstream
endobj"
            );

            _pdf.AppendLine(@"
6 0 obj
    [/PDF /Text]
endobj"
            );


            _pdf.AppendLine(@"
7 0 obj
    << /Type /Font
        /Subtype /Type1
        /Name /F1
        /BaseFont /Helvetica
        /Encoding /MacRomanEncoding
    >>
endobj"
            );

            _pdf.AppendLine(@"
xref
0 8
0000000000 65535 f
0000000009 00000 n
0000000074 00000 n
0000000120 00000 n
0000000179 00000 n
0000000364 00000 n
0000000466 00000 n
0000000496 00000 n"
            );

            _pdf.AppendLine(@"
trailer
    << /Size 8
        /Root 1 0 R
    >>"
            );

            _pdf.AppendLine(@"
startxref
625"
            );

            _pdf.AppendLine(@"%%EOF");


            StreamWriter file = new StreamWriter(@"C:\Users\Krotoff\Desktop\pdfwriter.pdf");
            file.Write(_pdf);
            file.Close();
        }
    }
}
