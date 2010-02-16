using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDFWriter
{
    static class PDFWriter
    {
        /// <summary>
        /// Default font.
        /// </summary>
        static private Font DefaultFont
        {
            get { return new Font(Font.Helvetica, 9); }
        }

        /// <summary>
        /// Default bold font.
        /// </summary>
        static private Font DefaultBoldFont
        {
            get { return new Font(Font.HelveticaBold, 9); }
        }

        /// <summary>
        /// A PDF cellule background color, see PDFTextBox.
        /// </summary>
        static private string CellBackgroundColor
        {
            get
            {
                return Color.Silver;
            }
        }

        /// <summary>
        /// Page layout (margins, page size...).
        /// </summary>
        static private PageLayout PageLayout
        {
            get { return new PageLayout(); }
        }

        /// <summary>
        /// Height of a row.
        /// </summary>
        static private double RowHeight
        {
            get { return 13; }
        }

        /// <summary>
        /// Gets the list of fonts as a list of PDF objects.
        /// </summary>
        static private List<PDFFont> Fonts
        {
            get
            {
                List<PDFFont> fonts = new List<PDFFont>();
                Dictionary<string, string> fontDictionary = Font.PDFFonts;
                foreach (KeyValuePair<string, string> pair in fontDictionary)
                {
                    PDFFont font = new PDFFont(pair.Key, pair.Value);
                    fonts.Add(font);
                    
                }
                return fonts;
            }
        }

        /// <summary>
        /// Gets the largest width possible of a column.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        static private double GetColumnWidth(string columnName, DataTable table)
        {
            double columnWidth = FontMetrics.GetTextWidth(columnName, DefaultBoldFont);
            foreach (DataRow row in table.Rows)
            {
                string rowName = row[columnName].ToString();

                double tmp = FontMetrics.GetTextWidth(rowName, DefaultFont);
                if (tmp > columnWidth)
                {
                    columnWidth = tmp;
                }
            }

            return columnWidth;
        }

        static private double GetTableWidth(DataTable table)
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

        static private List<PDFGraphicObject> CreateColumns(DataTable table)
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

        static private PDFTextBox CreateColumn(string columnName, double maxColumnWidth)
        {
            //Write all the column titles inside pdfColumnTitles
            PDFText text = new PDFText(columnName, DefaultFont);
            double width = maxColumnWidth;
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(text, margin, padding, 0, 0, CellBackgroundColor, width, RowHeight);
            return box;
        }

        static private PDFTextBox CreateRow(string rowName, double yPosBox)
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

        static public List<PDFGraphicObject> CreateHeader()
        {
            List<PDFGraphicObject> objects = new List<PDFGraphicObject>();

            PDFText header = new PDFText("Report", DefaultFont);
            PDFTranslation mark = new PDFTranslation(header, PageLayout.HeaderLeftXPos, PageLayout.HeaderYPos);
            objects.Add(mark);

            string tmp = DateTime.Now.ToShortDateString();
            header = new PDFText(tmp, DefaultFont);
            mark = new PDFTranslation(header, PageLayout.GetHeaderRightXPos(tmp, DefaultFont), PageLayout.HeaderYPos);
            objects.Add(mark);

            return objects;
        }

        static public List<PDFGraphicObject> CreateFooter(int currentPageNumber, int totalPageNumber)
        {
            List<PDFGraphicObject> objects = new List<PDFGraphicObject>();

            PDFText footer = new PDFText("Source: PDFWR (www.pdfwr.com)", DefaultFont);
            PDFTranslation mark = new PDFTranslation(footer, PageLayout.FooterLeftXPos, PageLayout.FooterYPos);
            objects.Add(mark);

            string tmp = string.Format(System.Globalization.CultureInfo.InvariantCulture, "Page {0} out of {1}", currentPageNumber, totalPageNumber);
            footer = new PDFText(tmp, DefaultFont);
            mark = new PDFTranslation(footer, PageLayout.GetFooterRightXPos(tmp, DefaultFont), PageLayout.FooterYPos);
            objects.Add(mark);

            return objects;
        }

        static public PDFPage CreatePage(DataTable table, PDFDocument doc, List<PDFGraphicObject> rows)
        {
            //Scaling
            double tableWidth = GetTableWidth(table);
            double scaling = (PageLayout.Width - (PageLayout.RightMargin + PageLayout.LeftMargin)) / (tableWidth);
            if (scaling > 1)
            {
                scaling = 1;
            }
            ///

            //Position
            double initXPosBox = (PageLayout.Width / 2) - (tableWidth / 2);
            if (initXPosBox < PageLayout.LeftMargin)
            {
                initXPosBox = PageLayout.LeftMargin;
            }
            double initYPosBox = PageLayout.Height - PageLayout.TopMargin;
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

            PDFPage page = new PDFPage();
            page.ContentStream = contentStream;
            page.Fonts = Fonts;
            doc.AddChild(page);

            return page;
        }

        static public PDFPages CreatePages(DataSet data, PDFDocument doc)
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

                double yPosBox = -RowHeight;

                for (int row = 0; row < table.Rows.Count; row++)
                {
                    double totalTableWidth = 0;

                    for (int column = 0; column < table.Columns.Count; column++)
                    {
                        bool endOfPage = (-(yPosBox - RowHeight) >= (PageLayout.Height - PageLayout.BottomMargin));

                        //Detects the end of the page
                        if (endOfPage)
                        {
                            PDFPage page = CreatePage(table, doc, rows);
                            pages.AddPage(page);

                            //Don't do a Clear() on the list, instead
                            //creates a new copy of the list otherwise
                            //objects referencing this list won't have their own copy
                            rows = new List<PDFGraphicObject>();

                            yPosBox = -RowHeight;
                        }

                        string rowName = table.Rows[row][column].ToString();
                        string columnName = table.Columns[column].ColumnName;

                        PDFTextBox text = CreateRow(rowName, yPosBox);
                        PDFTranslation translation = new PDFTranslation(text, totalTableWidth, 0);
                        rows.Add(translation);

                        double columnWidth = GetColumnWidth(columnName, table);
                        totalTableWidth += columnWidth + 2;
                    }

                    yPosBox -= RowHeight;
                }

                if (rows.Count > 0)
                {
                    PDFPage page = CreatePage(table, doc, rows);
                    pages.AddPage(page);
                }
            }

            return pages;
        }

        static public PDFDocument GetPDFDocument(DataSet data)
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
            foreach (PDFFont font in Fonts)
            {
                doc.AddChild(font);
            }
            ///

            //Outlines
            PDFOutlines outlines = new PDFOutlines();
            doc.AddChild(outlines);
            ///

            //Pages
            PDFPages pages = CreatePages(data, doc);
            doc.AddChild(pages);
            ///

            //Add headers and footers
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
