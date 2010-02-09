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
        static string CreateText(string text, double yPos, Font font) {
            string rg = string.Format("{0} rg", font.Color);
            string Tf = string.Format("/{0} {1} Tf", font.Name, font.Size);
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
            string backgroundColor = Color.NoColor;
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

            PageLayout pageLayout = new PageLayout();

            _pdf.AppendFormat(@"
% Page Node.
4 0 obj
    <<
        /Type /Page
        /Parent 3 0 R
        /MediaBox [0 0 {0} {1}]
        /Contents 5 0 R
        /Resources << /ProcSet 6 0 R >>
    >>
endobj", pageLayout.Width, pageLayout.Height
            );

            _pdf.AppendLine(@"
5 0 obj
    <<
        /Length 0
    >>
stream"
            );

            //Default font
            Font defaultFont = new Font(Font.Helvetica, 9);
            Font defaultBoldFont = new Font(Font.HelveticaBold, 9);

            string text = CreateText("Report", 2, defaultFont);
            string mark = CreateMark(text, 20, 766);
            _pdf.AppendLine(mark);

            text = CreateText("Source: PDFWR (www.pdfwr.com)", 2, defaultFont);
            mark = CreateMark(text, 20, 15);
            _pdf.AppendLine(mark);

            text = CreateText("04-Feb-2010", 2, defaultFont);
            mark = CreateMark(text, 540.475, 766);
            _pdf.AppendLine(mark);

            //
            string cellBackgroundColor = Color.Silver;


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


                //Space between each row: for example "Column 1" takes 30, "Column 2" 45 ect...
                //These numbers are aggregate inside this variable
                double countRowWidth = 0;

                //FIXME Height of a row
                const double height = 13;

                //Contains the column titles "Column 1", "Column 2"...
                string pdfColumnTitles = string.Empty;
                string pdfRowBox = string.Empty;

                //
                foreach (DataColumn column in table.Columns)
                {
                    //Gets the largest width possible of a column
                    double largestColumnWidth = FontMetrics.GetTextWidth(column.ColumnName, defaultBoldFont);
                    foreach (DataRow row in table.Rows)
                    {
                        string rowName = row[column.ColumnName].ToString();

                        double tmp = FontMetrics.GetTextWidth(rowName, defaultFont);
                        if (tmp > largestColumnWidth)
                        {
                            largestColumnWidth = tmp;
                        }
                    }
                    ////

                    Console.WriteLine("column: " + column.ColumnName);

                    //Write all the column titles inside pdfColumnTitles
                    double xPos = 2;
                    string cell = CreateText(column.ColumnName, xPos, defaultFont);
                    double width = largestColumnWidth + xPos;
                    int margin = 1;
                    int padding = 1;
                    string box = CreateBox(cell, margin, padding, 0, 0, cellBackgroundColor, width, height);
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

                            Font font = new Font(Font.Helvetica, 9, Color.Green);

                            //A string should be green
                            int rowNameInt32;
                            bool result = Int32.TryParse(rowName, out rowNameInt32);
                            if (result)
                            {
                                //A number should be blue
                                font.Color = Color.Blue;
                            }
                            else
                            {
                                double rowNameDouble;
                                result = Double.TryParse(rowName, out rowNameDouble);
                                if (result)
                                {
                                    //A number should be blue
                                    font.Color = Color.Blue;
                                }
                            }
                            cell = CreateText(rowName, xPos, font);

                            box = CreateBox(cell, margin, padding, 0, yPosBox);
                            pdfRows += box;

                            yPosBox -= height;
                        }

                        pdfRowBox += AppendBox(pdfRows, 1, countRowWidth, -height);
                    }
                    ////

                    countRowWidth += width + 2;
                }

                //Total width of our table
                //This is used when scaling the table to fit into the PDF page
                double scaling = (pageLayout.Width - (pageLayout.RightMargin + pageLayout.LeftMargin)) / (countRowWidth);
                if (scaling > 1)
                {
                    scaling = 1;
                }

                //FIXME
                double initXPosBox = (pageLayout.Width / 2) - (countRowWidth / 2);
                if (initXPosBox < pageLayout.LeftMargin)
                {
                    initXPosBox = pageLayout.LeftMargin;
                }
                const double initYPosBox = 737;

                _pdf.AppendLine(
                    AppendBox(pdfColumnTitles, scaling, initXPosBox, initYPosBox)
                );

                _pdf.AppendLine(
                    AppendBox(pdfRowBox, scaling, initXPosBox, initYPosBox)
                );
            }


            text = CreateText("Page 1 out of 1", 2, defaultFont);
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
