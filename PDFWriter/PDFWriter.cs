using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDFWriter
{
    /// <summary>
    /// Library PDFWriter.
    /// Takes a DataSet and generates a PDF file from it.
    /// </summary>
    /// 
    /// Wikipedia page: http://en.wikipedia.org/wiki/PDF_format
    /// For more technical informations about the PDF format, see PDFDocument.
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
        /// Gets the largest width (in the context of a PDF element of course) possible of a column.
        /// </summary>
        /// <param name="column">The column</param>
        /// <param name="table">The DataTable so we can iterates over the rows for the given column</param>
        /// <returns>Largest possible width of the given column</returns>
        static private double GetColumnWidth(DataColumn column, DataTable table)
        {
            double columnWidth = FontMetrics.GetTextWidth(column.ColumnName, DefaultBoldFont);
            foreach (DataRow row in table.Rows)
            {
                string rowName = row[column].ToString();

                double tmp = FontMetrics.GetTextWidth(rowName, DefaultFont);
                if (tmp > columnWidth)
                {
                    columnWidth = tmp;
                }
            }

            return columnWidth;
        }

        /// <summary>
        /// Gets the width (width of the PDF element of course) of a given DataTable.
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>Witdh of the DataTable</returns>
        static private double GetTableWidth(DataTable table)
        {
            double tableWidth = 0;

            foreach (DataColumn column in table.Columns)
            {
                double columnWidth = GetColumnWidth(column, table);

                tableWidth += columnWidth + 2;
            }

            return tableWidth;
        }

        /// <summary>
        /// Gets the columns as PDFGraphicObjects from a given DataTable.
        /// </summary>
        /// <param name="table">The DataTable from which to extract the columns</param>
        /// <returns>The DataTable columns</returns>
        static private List<PDFGraphicObject> CreateColumns(DataTable table)
        {
            List<PDFGraphicObject> columns = new List<PDFGraphicObject>();

            double totalTableWidth = 0;

            foreach (DataColumn column in table.Columns)
            {
                string columnName = column.ColumnName;

                double columnWidth = GetColumnWidth(column, table);

                PDFTextBox columnBox = CreateColumn(columnName, columnWidth);
                columns.Add(new PDFTranslation(columnBox, totalTableWidth, 0));

                totalTableWidth += columnWidth + 2;
            }

            return columns;
        }

        /// <summary>
        /// Creates a column as a PDFGraphicObject.
        /// </summary>
        /// <param name="columnName">Title/name of the column</param>
        /// <param name="columnWidth">Column width</param>
        /// <returns>A PDFGraphicObject representing the column</returns>
        static private PDFTextBox CreateColumn(string columnName, double columnWidth)
        {
            PDFText text = new PDFText(columnName, DefaultFont);
            double width = columnWidth;
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(text, margin, padding, 0, 0, CellBackgroundColor, width, RowHeight);
            return box;
        }

        /// <summary>
        /// Creates a row as a PDFGraphicObject.
        /// </summary>
        /// <param name="rowName">Title/name of the row</param>
        /// <param name="yPos">Y position of the row inside the PDF page</param>
        /// <returns>A PDFGraphicObject representing the row</returns>
        static private PDFTextBox CreateRow(string rowName, double yPos)
        {
            Font font = new Font(Font.Helvetica, 9, Color.Green);

            //A string should be green
            int rowNameInt32;
            bool result = Int32.TryParse(rowName, out rowNameInt32);
            if (result)
            {
                if (rowNameInt32 >= 0)
                {
                    //A positive number should be blue
                    font.Color = Color.Blue;
                }
                else
                {
                    //A negative number should be red
                    font.Color = Color.Red;
                }
            }
            PDFText text = new PDFText(rowName, font);
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(text, margin, padding, 0, yPos);
            return box;
        }

        /// <summary>
        /// Creates the PDF page header (some text at the top of the page).
        /// </summary>
        /// <returns>The PDF page header as PDFGraphicObjects</returns>
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

        /// <summary>
        /// Creates the PDF page footer (some text at the bottom of the page).
        /// </summary>
        /// <param name="currentPageNumber">current page number</param>
        /// <param name="totalPageNumber">total number of pages</param>
        /// <returns>The PDF page footer as PDFGraphicObjects</returns>
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

        /// <summary>
        /// Creates a PDF page given the main PDF document and a list of rows.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tableWidth">width of the DataTable (needed for scaling the table to fit inside the page)</param>
        /// <param name="columns">columns to show inside the PDF</param>
        /// <param name="rows">rows to show inside the PDF</param>
        /// <returns>The PDF page created</returns>
        static public PDFPage CreatePage(PDFDocument doc, double tableWidth, List<PDFGraphicObject> columns, List<PDFGraphicObject> rows)
        {
            //Scaling
            double scaling = (PageLayout.Width - (PageLayout.RightMargin + PageLayout.LeftMargin)) / (tableWidth);
            if (scaling > 1)
            {
                scaling = 1;
            }
            ////

            //Position
            double initXPosBox = (PageLayout.Width / 2) - (tableWidth / 2);
            if (initXPosBox < PageLayout.LeftMargin)
            {
                initXPosBox = PageLayout.LeftMargin;
            }
            double initYPosBox = PageLayout.Height - PageLayout.TopMargin;
            ////

            PDFContentStream contentStream = new PDFContentStream();
            doc.AddChild(contentStream);

            //Rows
            PDFScaling rowsScaling = new PDFScaling(rows, scaling, initXPosBox, initYPosBox);
            contentStream.AddChild(rowsScaling);
            ////

            //Columns
            PDFScaling columnsScaling = new PDFScaling(columns, scaling, initXPosBox, initYPosBox);
            contentStream.AddChild(columnsScaling);
            ////

            PDFPage page = new PDFPage();
            page.ContentStream = contentStream;
            page.Fonts = Fonts;
            doc.AddChild(page);

            return page;
        }

        /// <summary>
        /// Creates the pages contained inside the PDF.
        /// </summary>
        /// <param name="data">DataSet</param>
        /// <param name="doc">Main PDF document</param>
        /// <returns>The PDF pages (a list of PDFPage)</returns>
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

                double yPos = -RowHeight;

                for (int row = 0; row < table.Rows.Count; row++)
                {
                    double totalTableWidth = 0;

                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        //Detects the end of the page
                        bool endOfPage = (-(yPos - RowHeight) >= (PageLayout.Height - PageLayout.BottomMargin));
                        if (endOfPage)
                        {
                            //Creates the page
                            List<PDFGraphicObject> columns = CreateColumns(table);
                            double tableWidth = GetTableWidth(table);
                            PDFPage page = CreatePage(doc, tableWidth, columns, rows);
                            pages.AddPage(page);
                            ////

                            //Don't do a Clear() on the list, instead
                            //creates a new copy of the list otherwise
                            //objects referencing this list won't have their own copy
                            rows = new List<PDFGraphicObject>();

                            yPos = -RowHeight;
                        }
                        ////

                        //Create a row
                        string rowName = table.Rows[row][col].ToString();
                        PDFTextBox text = CreateRow(rowName, yPos);
                        PDFTranslation translation = new PDFTranslation(text, totalTableWidth, 0);
                        rows.Add(translation);
                        ////

                        DataColumn column = table.Columns[col];
                        double columnWidth = GetColumnWidth(column, table);
                        totalTableWidth += columnWidth + 2;
                    }

                    //Change Y position inside the coordinate system of PDF
                    yPos -= RowHeight;
                }

                if (rows.Count > 0)
                {
                    //Creates the page
                    List<PDFGraphicObject> columns = CreateColumns(table);
                    double tableWidth = GetTableWidth(table);
                    PDFPage page = CreatePage(doc, tableWidth, columns, rows);
                    pages.AddPage(page);
                    ////
                }
            }

            return pages;
        }

        /// <summary>
        /// Main function: gets the PDF given a DataSet.
        /// </summary>
        /// <param name="data">DataSet to convert into a PDF</param>
        /// <returns>The PDF</returns>
        static public PDFDocument GetPDFDocument(DataSet data)
        {
            //Root
            PDFDocument doc = new PDFDocument();
            ////

            //Info
            PDFInfo info = new PDFInfo("Report", "PDFWR", "PDFWR");
            doc.Info = info;
            doc.AddChild(info);
            ////

            //Fonts
            foreach (PDFFont font in Fonts)
            {
                doc.AddChild(font);
            }
            ////

            //Outlines
            PDFOutlines outlines = new PDFOutlines();
            doc.AddChild(outlines);
            ////

            //Pages
            PDFPages pages = CreatePages(data, doc);
            doc.AddChild(pages);
            ////

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
            ////

            //Catalog
            PDFCatalog catalog = new PDFCatalog(outlines, pages);
            doc.Catalog = catalog;
            doc.AddChild(catalog);
            ////

            return doc;
        }
    }
}
