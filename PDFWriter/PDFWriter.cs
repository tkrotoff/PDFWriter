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
        /// <param name="columnName"></param>
        /// <returns></returns>
        private double GetColumnWidth(string columnName, DataTable table)
        {
            double columnWidth = FontMetrics.GetTextWidth(columnName, defaultBoldFont);
            foreach (DataRow row in table.Rows)
            {
                string rowName = row[columnName].ToString();

                double tmp = FontMetrics.GetTextWidth(rowName, defaultFont);
                if (tmp > columnWidth)
                {
                    columnWidth = tmp;
                }
            }

            return columnWidth;
        }


        //Default fonts & colors
        private Font defaultFont = new Font(Font.Helvetica, 9);
        private Font defaultBoldFont = new Font(Font.HelveticaBold, 9);
        private string cellBackgroundColor = Color.Silver;

        //Page layout
        private PageLayout pageLayout = new PageLayout();

        //FIXME Height of a row
        private const double rowHeight = 13;


        private double GetTableWidth(DataTable table)
        {
            double tableWidth = 0;

            foreach (DataColumn column in table.Columns)
            {
                string columnName = column.ColumnName;

                double columnWidth = GetColumnWidth(columnName, table);

                tableWidth += columnWidth + 2;
            }

            return tableWidth;
        }

        private List<PDFGraphicObject> CreateColumns(DataTable table)
        {
            List<PDFGraphicObject> columns = new List<PDFGraphicObject>();

            double totalTableWidth = 0;

            foreach (DataColumn column in table.Columns)
            {
                string columnName = column.ColumnName;

                double columnWidth = GetColumnWidth(columnName, table);

                PDFTextBox columnBox = CreateColumn(columnName, columnWidth);
                columns.Add(new PDFTranslation(columnBox, totalTableWidth, 0));

                totalTableWidth += columnWidth + 2;
            }

            return columns;
        }

        private PDFTextBox CreateColumn(string columnName, double maxColumnWidth)
        {
            //Write all the column titles inside pdfColumnTitles
            PDFText text = new PDFText(columnName, defaultFont);
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

        public List<PDFGraphicObject> CreateHeader()
        {
            List<PDFGraphicObject> objects = new List<PDFGraphicObject>();

            PDFText header = new PDFText("Report", defaultFont);
            PDFTranslation mark = new PDFTranslation(header, pageLayout.HeaderLeftXPos, pageLayout.HeaderYPos);
            objects.Add(mark);

            string tmp = DateTime.Now.ToShortDateString();
            header = new PDFText(tmp, defaultFont);
            mark = new PDFTranslation(header, pageLayout.GetHeaderRightXPos(tmp, defaultFont), pageLayout.HeaderYPos);
            objects.Add(mark);

            return objects;
        }

        public List<PDFGraphicObject> CreateFooter(int currentPageNumber, int totalPageNumber)
        {
            List<PDFGraphicObject> objects = new List<PDFGraphicObject>();

            PDFText footer = new PDFText("Source: PDFWR (www.pdfwr.com)", defaultFont);
            PDFTranslation mark = new PDFTranslation(footer, pageLayout.FooterLeftXPos, pageLayout.FooterYPos);
            objects.Add(mark);

            string tmp = string.Format("Page {0} out of {1}", currentPageNumber, totalPageNumber);
            footer = new PDFText(tmp, defaultFont);
            mark = new PDFTranslation(footer, pageLayout.GetFooterRightXPos(tmp, defaultFont), pageLayout.FooterYPos);
            objects.Add(mark);

            return objects;
        }

        public PDFPage CreatePage(DataTable table, PDFDocument doc, List<PDFFont> fonts, List<PDFGraphicObject> rows)
        {
            //Scaling
            double tableWidth = GetTableWidth(table);
            double scaling = (pageLayout.Width - (pageLayout.RightMargin + pageLayout.LeftMargin)) / (tableWidth);
            if (scaling > 1)
            {
                scaling = 1;
            }
            ///

            //Position
            double initXPosBox = (pageLayout.Width / 2) - (tableWidth / 2);
            if (initXPosBox < pageLayout.LeftMargin)
            {
                initXPosBox = pageLayout.LeftMargin;
            }
            double initYPosBox = pageLayout.Height - pageLayout.TopMargin;
            ///

            PDFContentStream contentStream = new PDFContentStream();
            doc.AddChild(contentStream);

            //Rows
            PDFScaling rowsScaling = new PDFScaling(rows, scaling, initXPosBox, initYPosBox);
            contentStream.AddChild(rowsScaling);
            ///

            //Columns
            List<PDFGraphicObject> columns = CreateColumns(table);
            PDFScaling columnsScaling = new PDFScaling(columns, scaling, initXPosBox, initYPosBox);
            contentStream.AddChild(columnsScaling);
            ///

            PDFPage page = new PDFPage(fonts, contentStream);
            doc.AddChild(page);

            return page;
        }

        public PDFPages CreatePages(DataSet data, PDFDocument doc, List<PDFFont> fonts)
        {
            PDFPages pages = new PDFPages();

            //    ----------------------------
            //    | Column 1 | Column 2 | ...
            // y  ----------------------------
            // ^  | Row 10   | Row 11   | ...
            // |  | Row 20   | Row 21   | ...
            // |  | Row 30   | Row 31   | ...
            // |  ----------------------------
            // 0 ----> x

            foreach (DataTable table in data.Tables)
            {
                List<PDFGraphicObject> rows = new List<PDFGraphicObject>();

                double yPosBox = -rowHeight;

                for (int row = 0; row < table.Rows.Count; row++)
                {
                    double totalTableWidth = 0;

                    for (int column = 0; column < table.Columns.Count; column++)
                    {
                        bool endOfPage = (-(yPosBox - rowHeight) >= (pageLayout.Height - pageLayout.BottomMargin));

                        //Detects the end of the page
                        if (endOfPage)
                        {
                            PDFPage page = CreatePage(table, doc, fonts, rows);
                            pages.AddPage(page);

                            //Don't do a Clear() on the list, instead
                            //creates a new copy of the list otherwise
                            //objects referencing this list won't have their own copy
                            rows = new List<PDFGraphicObject>();

                            yPosBox = -rowHeight;
                        }

                        string rowName = table.Rows[row][column].ToString();
                        string columnName = table.Columns[column].ColumnName;

                        PDFTextBox text = CreateRow(rowName, yPosBox);
                        PDFTranslation translation = new PDFTranslation(text, totalTableWidth, 0);
                        rows.Add(translation);

                        double columnWidth = GetColumnWidth(columnName, table);
                        totalTableWidth += columnWidth + 2;
                    }

                    yPosBox -= rowHeight;
                }

                if (rows.Count > 0)
                {
                    PDFPage page = CreatePage(table, doc, fonts, rows);
                    pages.AddPage(page);
                }
            }

            return pages;
        }

        public PDFDocument GetPDFDocument(DataSet data)
        {
            //Root
            PDFDocument doc = new PDFDocument();
            ///

            //Info
            PDFInfo info = new PDFInfo("Report", "PDFWR", "PDFWR");
            doc.Info = info;
            doc.AddChild(info);
            ///

            //Fonts
            List<PDFFont> fonts = new List<PDFFont>();
            Dictionary<string, string> fontDictionary = Font.PDFFonts;
            foreach (KeyValuePair<string, string> pair in fontDictionary)
            {
                PDFFont font = new PDFFont(pair.Key, pair.Value);
                fonts.Add(font);
                doc.AddChild(font);
            }
            ///

            //Outlines
            PDFOutlines outlines = new PDFOutlines();
            doc.AddChild(outlines);
            ///

            //Pages
            PDFPages pages = CreatePages(data, doc, fonts);
            doc.AddChild(pages);
            int count = 1;
            foreach (PDFPage page in pages.Pages)
            {
                List<PDFGraphicObject> header = CreateHeader();
                page.ContentStream.AddRange(header);

                List<PDFGraphicObject> footer = CreateFooter(count, pages.Pages.Count);
                page.ContentStream.AddRange(footer);

                count++;
            }
            ///

            //Catalog
            PDFCatalog catalog = new PDFCatalog(outlines, pages);
            doc.Catalog = catalog;
            doc.AddChild(catalog);
            ///

            return doc;
        }
    }
}
