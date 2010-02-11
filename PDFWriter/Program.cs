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

        /// <summary>
        /// Gets the largest width possible of a column.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        static private double GetMaxColumnWidth(DataColumn column, DataTable table)
        {
            double maxColumnWidth = FontMetrics.GetTextWidth(column.ColumnName, defaultBoldFont);
            foreach (DataRow row in table.Rows)
            {
                string rowName = row[column.ColumnName].ToString();

                double tmp = FontMetrics.GetTextWidth(rowName, defaultFont);
                if (tmp > maxColumnWidth)
                {
                    maxColumnWidth = tmp;
                }
            }

            return maxColumnWidth;
        }


        //Default fonts & colors
        static private Font defaultFont = new Font(Font.Helvetica, 9);
        static private Font defaultBoldFont = new Font(Font.HelveticaBold, 9);
        static private string cellBackgroundColor = Color.Silver;

        //Page layout
        static private PageLayout pageLayout = new PageLayout();

        static private List<PDFContentStream> CreateContentStreams(DataSet data)
        {
            List<PDFContentStream> contentStreams = new List<PDFContentStream>();

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

                List<PDFObject> pdfColumnTitles = new List<PDFObject>();

                List<PDFObject> pdfRowBox = new List<PDFObject>();

                //
                foreach (DataColumn column in table.Columns)
                {
                    double maxColumnWidth = GetMaxColumnWidth(column, table);

                    Console.WriteLine("column: " + column.ColumnName);

                    //Write all the column titles inside pdfColumnTitles
                    PDFText text = new PDFText(column.ColumnName, defaultFont);
                    double width = maxColumnWidth;
                    int margin = 1;
                    int padding = 1;
                    PDFBox box = new PDFBox(text, margin, padding, 0, 0, cellBackgroundColor, width, height);
                    pdfColumnTitles.Add(new PDFRow(box, countRowWidth));
                    ////


                    //Loop over the rows
                    {
                        List<PDFObject> pdfRows = new List<PDFObject>();

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
                                //A positive number should be blue
                                font.Color = Color.Blue;
                            }
                            else
                            {
                                double rowNameDouble;
                                result = Double.TryParse(rowName, out rowNameDouble);
                                if (result)
                                {
                                    //A positive number should be blue
                                    font.Color = Color.Blue;
                                }
                            }
                            text = new PDFText(rowName, font);
                            box = new PDFBox(text, margin, padding, 0, yPosBox);
                            pdfRows.Add(box);

                            yPosBox -= height;
                        }

                        pdfRowBox.Add(new PDFAppendBox(pdfRows, 1, countRowWidth, -height));
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

                PDFAppendBox appendBox = new PDFAppendBox(pdfColumnTitles, scaling, initXPosBox, initYPosBox);
                PDFContentStream contentStream = new PDFContentStream();
                contentStream.AddChild(appendBox);

                appendBox = new PDFAppendBox(pdfRowBox, scaling, initXPosBox, initYPosBox);
                contentStream.AddChild(appendBox);

                contentStreams.Add(contentStream);
            }

            return contentStreams;
        }

        static void Main(string[] args)
        {
            //Root
            PDFRoot root = new PDFRoot();
            ///

            //Info
            PDFInfo info = new PDFInfo("Report", "PDFWR", "PDFWR");
            root.Info = info;
            root.AddChild(info);
            ///

            //Fonts
            List<PDFFont> fonts = new List<PDFFont>();
            Dictionary<string, string> fontDictionary = Font.PDFFonts;
            foreach (KeyValuePair<string, string> pair in fontDictionary)
            {
                PDFFont font = new PDFFont(pair.Key, pair.Value);
                fonts.Add(font);
                root.AddChild(font);
            }
            ///

            //Outlines
            PDFOutlines outlines = new PDFOutlines();
            root.AddChild(outlines);
            ///

            //Pages
            PDFPages pages = new PDFPages();
            root.AddChild(pages);
            ///

            //DataSet
            DataSet data = CreateDataSet();
            ///

            //Content streams
            int contentStreamsCount = 1;
            List<PDFContentStream> contentStreams = CreateContentStreams(data);
            foreach (PDFContentStream contentStream in contentStreams)
            {
                //Header and footer
                PDFText header = new PDFText("Report", defaultFont);
                PDFMark mark = new PDFMark(header, pageLayout.HeaderLeftXPos, pageLayout.HeaderYPos);
                contentStream.AddChild(mark);

                string tmp = DateTime.Now.ToShortDateString();
                header = new PDFText(tmp, defaultFont);
                mark = new PDFMark(header, pageLayout.GetHeaderRightXPos(tmp, defaultFont), pageLayout.HeaderYPos);
                contentStream.AddChild(mark);

                PDFText footer = new PDFText("Source: PDFWR (www.pdfwr.com)", defaultFont);
                mark = new PDFMark(footer, pageLayout.FooterLeftXPos, pageLayout.FooterYPos);
                contentStream.AddChild(mark);

                tmp = string.Format("Page {0} out of {1}", contentStreamsCount++, contentStreams.Count);
                footer = new PDFText(tmp, defaultFont);
                mark = new PDFMark(footer, pageLayout.GetFooterRightXPos(tmp, defaultFont), pageLayout.FooterYPos);
                contentStream.AddChild(mark);
                ///

                root.AddChild(contentStream);

                //Page
                PDFPage page = new PDFPage(fonts, contentStream);
                pages.AddChild(page);
                root.AddChild(page);
                ///
            }
            ///

            //Catalog
            PDFCatalog catalog = new PDFCatalog(outlines, pages);
            root.Catalog = catalog;
            root.AddChild(catalog);
            ///

            StringBuilder pdf = new StringBuilder();
            pdf.Append(root.ToInnerPDF());

            //Write the PDF to a file
            StreamWriter file = new StreamWriter(@"C:\Users\Krotoff\Desktop\pdfwriter.pdf");
            file.Write(pdf);
            file.Close();
            ////








            /*CrossReferenceTable xref = new CrossReferenceTable();

            _pdf.AppendLine("%PDF-1.3");

            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.Info));
            _pdf.AppendFormat(@"
{0} 0 obj
    <<
        /Title (Report)
        /Creator (PDFWR)
        /Producer (PDFWR)
    >>
endobj", xref.Count
            );
            _pdf.AppendLine();

            List<string> fonts = Font.GetPDFFonts();
            foreach (string font in fonts)
            {
                xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.Font));
                _pdf.AppendFormat(@"
{0} 0 obj
    <<{1}
    >>
endobj", xref.Count, font
                );
                _pdf.AppendLine();
            }

            // Catalog Dictionnary
            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.Catalog));
            _pdf.AppendFormat(@"
% Catalog Dictionnary.
% Contains references to other objects defining the document's
% contents, outline, article threads ect...
{0} 0 obj
    <<
        /Type /Catalog
        /Outlines 17 0 R
        /Pages 18 0 R
    >>
endobj", xref.Count
            );
            _pdf.AppendLine();

            // Document Outline
            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.Outlines));
            _pdf.AppendFormat(@"
% Document Outline.
% Allows the user to navigate interactively from one part of
% the document to another.
% The outline consists of a tree-structured hierarchy of
% outline items (sometimes called bookmarks), which serve as a
% 'visual table of contents' to display the document's
% structure to the user.
{0} 0 obj
    <<
        /Type /Outlines
        /Count 0
    >>
endobj", xref.Count
            );
            _pdf.AppendLine();

            // Page Tree
            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.Pages));
            _pdf.AppendFormat(@"
% Page Tree.
{0} 0 obj
    <<
        /Type /Pages

        % Kids: an array of indirect references to the immediate children of this node.
        % The children may be page objects or other page tree nodes.
        /Kids [20 0 R]

        % Count: the number of leaf nodes (page objects) that are descendants of this
        % node within the page tree.
        /Count 1
    >>
endobj", xref.Count
            );
            _pdf.AppendLine();

            PageLayout pageLayout = new PageLayout();

            // Page Node
            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.Page));
            _pdf.AppendFormat(@"
% Page Node.
{0} 0 obj
    <<
        /Type /Page
        /Parent 16 0 R
        /MediaBox [0 0 {1} {2}]
        /Contents 20 0 R
        /Resources
            <<
                /ProcSet 7 0 R
                /Font
                    <<

                    >>
            >>
    >>
endobj", xref.Count, pageLayout.Width, pageLayout.Height
            );
            _pdf.AppendLine();

            // Content
            StringBuilder content = new StringBuilder();

            //Default font
            Font defaultFont = new Font(Font.Helvetica, 9);
            Font defaultBoldFont = new Font(Font.HelveticaBold, 9);

            string text = CreateText("Report", 2, defaultFont);
            string mark = CreateMark(text, pageLayout.HeaderLeftXPos, pageLayout.HeaderYPos);
            content.AppendLine(mark);

            text = CreateText("Source: PDFWR (www.pdfwr.com)", 2, defaultFont);
            mark = CreateMark(text, pageLayout.FooterLeftXPos, pageLayout.FooterYPos);
            content.AppendLine(mark);

            string header = "04-Feb-2010";
            text = CreateText(header, 2, defaultFont);
            mark = CreateMark(text, pageLayout.GetHeaderRightXPos(header, defaultFont), pageLayout.HeaderYPos);
            content.AppendLine(mark);

            //
            string cellBackgroundColor = Color.Silver;




            //Length: the number of bytes from the beginning of the line following
            //the keyword stream to the last byte just before the keyword
            //endstream. (There may be an additional EOL marker, preceding endstream,
            //that is not included in the count and is not logically part of
            //the stream data.)
            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.ContentStream));
            _pdf.AppendFormat(@"
% Our stream that contains all the text
{0} 0 obj
    <<
        /Length {1}
    >>
stream", xref.Count, content.Length
            );

            _pdf.AppendLine(content.ToString());

            // End of stream
            _pdf.AppendLine(@"
endstream
endobj"
            );

            // Procedure set array
            xref.AddByteOffset(new ObjectReference(_pdf.Length, ObjectReference.ObjectType.ProcedureSetArray));
            _pdf.AppendFormat(@"
{0} 0 obj
    [/PDF /Text]
endobj", xref.Count
            );
            _pdf.AppendLine();

            // Cross-reference table
            _pdf.AppendLine(xref.GetXRefPDFString(_pdf.Length));

            _pdf.AppendLine(xref.GetTrailerPDFString());

            _pdf.AppendLine(xref.GetStartXRefPDFString());

            _pdf.AppendLine("%%EOF");*/
        }
    }
}
