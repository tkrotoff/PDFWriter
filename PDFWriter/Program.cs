using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace PDFWriter
{
    class Program
    {
        static DataSet CreateDataSet()
        {
            DataTable table = new DataTable("Sample");

            DataColumn column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Symbol";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Desc";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Weight";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Price";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Cty";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Country";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Sct";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Sector";
            table.Columns.Add(column);

            List<string> rows = new List<string>();
            rows.Add("AAAA,AAAA,0.10,100,US,UNITED STATES,10,ALPHA");
            rows.Add("BBBB,BBBB,0.20,150,CA,CANADA,20,BETA");
            rows.Add("CCCC,CCCC,0.30,300,CA,CANADA,20,BETA");
            rows.Add("DDDD,DDDD,0.40,450,US,UNITED STATES,30,DELTA");

            foreach (string rowStr in rows)
            {
                string[] rowList = rowStr.Split(',');

                DataRow row = table.NewRow();
                int i = 0;
                foreach (DataColumn col in table.Columns)
                {
                    row[col.ColumnName] = rowList[i];
                    i++;
                }
                table.Rows.Add(row);
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table);

            dataSet.WriteXml("data.xml");

            DataSet newDataSet = new DataSet("Sample");
            newDataSet.ReadXml("data.xml");

            return newDataSet;
        }

        //Helvetica
        static double[] FH = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, 
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333, 
            .278, .278, .355, .556, .556, .889, .667, .191, .333, .333, .389, .584, .278, .333, .278, .278, 
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .278, .278, .584, .584, .584, .556, 
            1.015, .667, .667, .722, .722, .667, .611, .778, .722, .278, .500, .667, .556, .833, .722, .778, 
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .278, .278, .278, .469, .556, 
            .333, .556, .556, .500, .556, .556, .278, .556, .556, .222, .222, .500, .222, .833, .556, .556, 
            .556, .556, .333, .500, .278, .556, .500, .722, .500, .500, .500, .334, .260, .334, .584, .278, 
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .333, .333, .333, .222, 
            .222, .222, 1.00, .500, .500, .556, 1.00, .667, .667, .611, .278, .222, .944, .500, .500, .278, 
            .278, .333, .556, .556, .556, .556, .260, .556, .333, .737, .370, .556, .584, .278, .737, .333, 
            .400, .584, .333, .333, .333, .556, .537, .278, .333, .333, .365, .556, .834, .834, .834, .611, 
            .667, .667, .667, .667, .667, .667, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278, 
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611, 
            .556, .556, .556, .556, .556, .556, .889, .500, .556, .556, .556, .556, .278, .278, .278, .278, 
            .556, .556, .556, .556, .556, .556, .556, .584, .611, .556, .556, .556, .556, .500, .556, .500, 
        };

        //Helvetica Bold
        static double[] FHB = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, 
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333, 
            .278, .333, .474, .556, .556, .889, .722, .238, .333, .333, .389, .584, .278, .333, .278, .278, 
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .333, .333, .584, .584, .584, .611, 
            .975, .722, .722, .722, .722, .667, .611, .778, .722, .278, .556, .722, .611, .833, .722, .778, 
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .333, .278, .333, .584, .556, 
            .333, .556, .611, .556, .611, .556, .333, .611, .611, .278, .278, .556, .278, .889, .611, .611, 
            .611, .611, .389, .556, .333, .611, .556, .778, .556, .556, .500, .389, .280, .389, .584, .278, 
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .500, .500, .500, .278, 
            .278, .278, 1.00, .611, .611, .611, 1.00, .667, .667, .611, .278, .278, .944, .556, .500, .278, 
            .278, .333, .556, .556, .556, .556, .280, .556, .333, .737, .370, .556, .584, .278, .737, .333, 
            .400, .584, .333, .333, .333, .611, .556, .278, .333, .333, .365, .556, .834, .834, .834, .611, 
            .722, .722, .722, .722, .722, .722, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278, 
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611, 
            .556, .556, .556, .556, .556, .556, .889, .556, .556, .556, .556, .556, .278, .278, .278, .278, 
            .611, .611, .611, .611, .611, .611, .611, .584, .611, .611, .611, .611, .611, .556, .611, .556, 
        };

        static double GetTextWidth(string text, string font, int fontSize, int charSpace, int wordSpace)
        {
            double width = 0;

            foreach (char ch in text)
            {
                int asciiCode = (int)ch;
                int ws = 0;
                if (ch == 32) {
                    //Width of a space character
                    ws = wordSpace;
                }
                double charWidth = 0;
                if (font == "FH")
                {
                    charWidth = FH[ch];
                }
                else if (font == "FHB")
                {
                    charWidth = FHB[ch];
                }
                width += charWidth + charSpace + ws;
            }

            return width * fontSize;
        }

        static readonly string NO_COLOR = string.Empty;
        static readonly string BLACK = "0 0 0";
        static readonly string BLUE = "0 0 1";
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
        static readonly string YELLOW = "1 1 0";

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
        static string CreateText(string text, int yPos, string color, string font, int fontSize) {
            int charSpace = 0;
            int wordSpace = 0;
            double textWidth = GetTextWidth(text, font, fontSize, charSpace, wordSpace);

            //Black is the default value so no need to write it down
            string rg = string.Format("{0} rg", color);

            //If a font name is specified
            //FIXME and what if we specify the font size without specifying the font name?
            string Tf = string.Format("/{0} {1} Tf", font, fontSize);

            string Td = string.Format("0 {0} Td", yPos);

            string Tj = string.Format("({0}) Tj", text);

            return string.Format(@"
% CreateText (
   BT
       {0}
       {1}
       {2}
       {3}
    ET
%)", rg, Tf, Td, Tj
            );
        }

        static string CreateNode(string node, int refs, int depth, string text)
        {
            return "";
        }

        static string CreateMark(string box, double xPos, double yPos)
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

        static string CreateRow(string box, double width)
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

        static string CreateBox(string box, int margin, int padding, double xPos, double yPos)
        {
            string backgroundColor = NO_COLOR;
            int width = 0;
            int height = 0;
            return CreateBox(box, margin, padding, xPos, yPos, backgroundColor, width, height);
        }

        static string CreateBox(string box, int margin, int padding, double xPos, double yPos, string backgroundColor, double width, double height)
        {
            StringBuilder tmp = new StringBuilder();
            tmp.AppendLine(@"
% CreateBox (
    q"
            );

            if (margin != 0)
            {
                tmp.AppendFormat(@"
    % translation margin
    1 0 0 1 {0} {0} cm", margin
                );
            }

            if (width != 0 && height != 0)
            {
                tmp.AppendFormat(@"
    {0} rg
    0 0 {1} {2} re
    f", backgroundColor, width, height
                );
            }

            if (padding != 0)
            {
                tmp.AppendFormat(@"
    % translation padding
    1 0 0 1 {0} {0} cm", padding
                );
            }

            tmp.AppendFormat(@"
    % translation position
    1 0 0 1 {0} {1} cm
    {2}", xPos, yPos, box
            );

            tmp.AppendFormat(@"
    % remove translation margin, padding, position
    Q
%)"
            );

            return tmp.ToString();
        }

        static string AppendBox(string box, double scaling, double xPos, double yPos)
        {
            return string.Format(@"
% AppendBox (
    q {0} 0 0 {0} {1} {2} cm
    {3}
    Q
%)", scaling, xPos, yPos, box
            );
        }

        static void Main(string[] args)
        {
            _pdf.AppendLine("%PDF-1.3");

            /*_pdf.AppendLine(@"
0 0 obj
    <<
        /Title (Report)
        /Creator (PDFWR)
        /Producer (PDFWR)
    >>
endobj"
            );*/

            // Catalog Dictionnary
            _pdf.AppendLine(@"
% Catalog Dictionnary.
% Contains references to other objects defining the document's
% contents, outline, article threads ect...
1 0 obj
    <<
        /Type /Catalog
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
    <<
        /Type /Outlines
        /Count 0
    >>
endobj"
            );

            // Page Tree
            _pdf.AppendLine(@"
% Page Tree.
3 0 obj
    <<
        /Type /Pages
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
    <<
        /Type /Page
        /Parent 3 0 R
        /MediaBox [0 0 612 792]
        /Contents 5 0 R
        /Resources << /ProcSet 6 0 R >>
    >>
endobj"
            );

            _pdf.AppendLine(@"
5 0 obj
    <<
        /Length 0
    >>
stream"
            );

            //612 x 792

            string text = CreateText("Report", 2, BLACK, "FH", 9);
            string mark = CreateMark(text, 20, 766);
            _pdf.AppendLine(mark);

            text = CreateText("Source: PDFWR (www.pdfwr.com)", 2, BLACK, "FH", 9);
            mark = CreateMark(text, 20, 15);
            _pdf.AppendLine(mark);

            text = CreateText("04-Feb-2010", 2, BLACK, "FH", 9);
            mark = CreateMark(text, 540.475, 766);
            _pdf.AppendLine(mark);

            DataSet data = CreateDataSet();

            //There should be only one table
            foreach (DataTable table in data.Tables)
            {
                //General algorithm:
                //
                //    ----------------------------
                //    | Column 1 | Column 2 | ...
                // y  ----------------------------
                // ^  | Row 10   | Row 11   | ...
                // |  | Row 20   | Row 21   | ...
                // |  | Row 30   | Row 31   | ...
                // |  ----------------------------
                // 0 ----> x
                //
                // The first loop will read the horizontal line containing all the column names
                // ("Column 1", "Column 2"). 
                // Then it will read "Row 10", "Row 20", "Row 30" vertically.
                // One could imagine to read everything horizontally but DataTable does not work this way
                // and splits columns from rows.


                //Basic font attributes
                const string fontBold = "FHB";
                const string font = "FH";
                const int fontSize = 9;
                string textColor = BLACK;
                ////

                //
                string cellBackgroundColor = SILVER;

                //Space between each row: for example "Column 1" takes 30, "Column 2" 45 ect...
                //These numbers are aggregate inside this variable
                double countRowWidth = 0;

                //Total width of our table
                //This is used when scaling the table to fit into the PDF page
                double totalRowWidth = 0;
                double scaling = 1;

                //FIXME Height of a row
                const double height = 13;

                //Contains the column titles "Column 1", "Column 2"...
                string pdfColumnTitles = string.Empty;

                //FIXME
                const double initXPosBox = 170.2325;
                const double initYPosBox = 737;

                //
                foreach (DataColumn column in table.Columns)
                {
                    //Gets the largest width possible of a column
                    double largestTextWidth = GetTextWidth(column.ColumnName, fontBold, fontSize, 0, 0);
                    foreach (DataRow row in table.Rows)
                    {
                        string rowName = row[column.ColumnName].ToString();

                        double tmp = GetTextWidth(rowName, font, fontSize, 0, 0);
                        if (tmp > largestTextWidth)
                        {
                            largestTextWidth = tmp;
                        }
                    }
                    ////

                    totalRowWidth += largestTextWidth;



                    Console.WriteLine("column: " + column.ColumnName);

                    //Write all the column titles inside pdfColumnTitles
                    string cell = CreateTextCell(column.ColumnName, 2, textColor, font, fontSize);
                    double width = largestTextWidth + 2;
                    string box = CreateBox(cell, 1, 1, 0, 0, cellBackgroundColor, width, height);
                    pdfColumnTitles += CreateRow(box, countRowWidth);
                    ////
                    

                    //Loop over the rows
                    {
                        string pdfRows = string.Empty;
                        double yPosBox = 0;

                        foreach (DataRow row in table.Rows)
                        {
                            string rowName = row[column.ColumnName].ToString();
                            Console.WriteLine("row: " + rowName);

                            //A string should be green
                            string color = GREEN;
                            int rowNameInt32;
                            bool result = Int32.TryParse(rowName, out rowNameInt32);
                            if (result)
                            {
                                //A number should be blue
                                color = BLUE;
                            }
                            else
                            {
                                double rowNameDouble;
                                result = Double.TryParse(rowName, out rowNameDouble);
                                if (result)
                                {
                                    //A number should be blue
                                    color = BLUE;
                                }
                            }
                            cell = CreateTextCell(rowName, 2, color, font, fontSize);

                            box = CreateBox(cell, 1, 1, 0, yPosBox);
                            pdfRows += box;

                            yPosBox -= height;
                        }

                        _pdf.AppendLine(
                            AppendBox(pdfRows, scaling, initXPosBox + countRowWidth, initYPosBox - height)
                        );
                    }
                    ////


                    countRowWidth += width + 2;
                }

                _pdf.AppendLine(
                    AppendBox(pdfColumnTitles, scaling, initXPosBox, initYPosBox)
                );
            }


            text = CreateText("Page 1 out of 1", 2, BLACK, "FH", 9);
            mark = CreateMark(text, 530.953, 15);
            _pdf.AppendLine(mark);
           
            //Length: the number of bytes from the beginning of the line following
            //the keyword stream to the last byte just before the keyword
            //endstream. (There may be an additional EOL marker, preceding endstream,
            //that is not included in the count and is not logically part of
            //the stream data.)
            /*_pdf.AppendFormat(@"
5 0 obj
    <<
        /Length {0}
    >>
stream", length
            );*/

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
    <<
        /Type /Font
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
    <<
        /Size 8
        /Root 1 0 R
    >>"
            );

            _pdf.AppendLine(@"
startxref
625"
            );

            _pdf.AppendLine(@"%%EOF");
            

            //Write the PDF to a file
            StreamWriter file = new StreamWriter(@"C:\Users\Krotoff\Desktop\pdfwriter.pdf");
            file.Write(_pdf);
            file.Close();
            ////
        }
    }
}
