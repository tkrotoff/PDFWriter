using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDFWriter
{
    class PDFWriter
    {
        /// <summary>
        /// Gets the largest width possible of a column.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private double GetMaxColumnWidth(DataColumn column, DataTable table)
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
        private Font defaultFont = new Font(Font.Helvetica, 9);
        private Font defaultBoldFont = new Font(Font.HelveticaBold, 9);
        private string cellBackgroundColor = Color.Silver;

        //Page layout
        private PageLayout pageLayout = new PageLayout();

        //FIXME Height of a row
        private const double rowHeight = 13;

        private PDFTextBox CreateColumnTitle(DataColumn column, double maxColumnWidth)
        {
            //Write all the column titles inside pdfColumnTitles
            PDFText text = new PDFText(column.ColumnName, defaultFont);
            double width = maxColumnWidth;
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(text, margin, padding, 0, 0, cellBackgroundColor, width, rowHeight);
            return box;
        }

        private PDFTextBox CreateRow(string rowName, double yPosBox)
        {
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
            PDFText text = new PDFText(rowName, font);
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(text, margin, padding, 0, yPosBox);
            return box;
        }

        private List<PDFContentStream> CreateContentStreams(DataSet data)
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

                //Contains the column titles "Column 1", "Column 2"...

                List<PDFGraphicObject> pdfColumnTitles = new List<PDFGraphicObject>();

                List<PDFGraphicObject> pdfRowBox = new List<PDFGraphicObject>();

                //
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine("column: " + column.ColumnName);

                    double maxColumnWidth = GetMaxColumnWidth(column, table);
                    PDFTextBox columnTitleBox = CreateColumnTitle(column, maxColumnWidth);
                    //Write all the column titles inside pdfColumnTitles
                    pdfColumnTitles.Add(new PDFTranslation(columnTitleBox, countRowWidth, 0));
                    ////


                    //Loop over the rows
                    {
                        List<PDFGraphicObject> pdfRows = new List<PDFGraphicObject>();

                        double yPosBox = 0;

                        foreach (DataRow row in table.Rows)
                        {
                            string rowName = row[column.ColumnName].ToString();
                            Console.WriteLine("row: " + rowName);

                            PDFTextBox rowBox = CreateRow(rowName, yPosBox);
                            pdfRows.Add(rowBox);

                            yPosBox -= rowHeight;
                        }

                        pdfRowBox.Add(new PDFAppendBox(pdfRows, 1, countRowWidth, -rowHeight));
                    }
                    ////

                    countRowWidth += maxColumnWidth + 2;
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

        public PDFRoot GetPDFRoot(DataSet data)
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

            //Content streams
            int contentStreamsCount = 1;
            List<PDFContentStream> contentStreams = CreateContentStreams(data);
            foreach (PDFContentStream contentStream in contentStreams)
            {
                //Header and footer
                PDFText header = new PDFText("Report", defaultFont);
                PDFTranslation mark = new PDFTranslation(header, pageLayout.HeaderLeftXPos, pageLayout.HeaderYPos);
                contentStream.AddChild(mark);

                string tmp = DateTime.Now.ToShortDateString();
                header = new PDFText(tmp, defaultFont);
                mark = new PDFTranslation(header, pageLayout.GetHeaderRightXPos(tmp, defaultFont), pageLayout.HeaderYPos);
                contentStream.AddChild(mark);

                PDFText footer = new PDFText("Source: PDFWR (www.pdfwr.com)", defaultFont);
                mark = new PDFTranslation(footer, pageLayout.FooterLeftXPos, pageLayout.FooterYPos);
                contentStream.AddChild(mark);

                tmp = string.Format("Page {0} out of {1}", contentStreamsCount++, contentStreams.Count);
                footer = new PDFText(tmp, defaultFont);
                mark = new PDFTranslation(footer, pageLayout.GetFooterRightXPos(tmp, defaultFont), pageLayout.FooterYPos);
                contentStream.AddChild(mark);
                ///

                root.AddChild(contentStream);

                //Page
                PDFPage page = new PDFPage(fonts, contentStream);
                root.AddChild(page);
                pages.AddPage(page);
                ///
            }
            ///

            //Catalog
            PDFCatalog catalog = new PDFCatalog(outlines, pages);
            root.Catalog = catalog;
            root.AddChild(catalog);
            ///

            return root;
        }
    }
}
