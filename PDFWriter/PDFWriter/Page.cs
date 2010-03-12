using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDF
{
    static class Page
    {
        private static PageLayout _pageLayout;

        /// <summary>
        /// Page layout (margins, page size...).
        /// </summary>
        public static PageLayout PageLayout
        {
            get
            {
                //TODO use a style template to get this property
                if (_pageLayout == null)
                {
                    //Sets a default page layout if none provided by the user
                    _pageLayout = new PageLayout();
                }
                return _pageLayout;
            }

            set
            {
                _pageLayout = value;
            }
        }

        /// <summary>
        /// Creates the PDF page header (some text at the top of the page).
        /// </summary>
        /// <returns>The PDF page header as PDFGraphicObjects</returns>
        public static List<PDFGraphicObject> CreateHeader()
        {
            List<PDFGraphicObject> objects = new List<PDFGraphicObject>();

            PDFText header = new PDFText(PageLayout.LeftHeader, PDFWriter.DefaultFont);
            PDFTranslation mark = new PDFTranslation(
                header,
                PageLayout.HeaderLeftXPos,
                PageLayout.HeaderYPos
            );
            objects.Add(mark);

            header = new PDFText(PageLayout.RightHeader, PDFWriter.DefaultFont);
            mark = new PDFTranslation(
                header,
                PageLayout.GetHeaderRightXPos(PageLayout.RightHeader, PDFWriter.DefaultFont),
                PageLayout.HeaderYPos
            );
            objects.Add(mark);

            return objects;
        }

        /// <summary>
        /// Creates the PDF page footer (some text at the bottom of the page).
        /// </summary>
        /// <param name="currentPageNumber">current page number</param>
        /// <param name="totalPageNumber">total number of pages</param>
        /// <returns>The PDF page footer as PDFGraphicObjects</returns>
        public static List<PDFGraphicObject> CreateFooter(int currentPageNumber, int totalPageNumber)
        {
            List<PDFGraphicObject> objects = new List<PDFGraphicObject>();

            PDFText footer = new PDFText(PageLayout.LeftFooter, PDFWriter.DefaultFont);
            PDFTranslation mark = new PDFTranslation(
                footer,
                PageLayout.FooterLeftXPos,
                PageLayout.FooterYPos
            );
            objects.Add(mark);

            string tmp = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "Page {0} out of {1}",
                currentPageNumber, totalPageNumber
            );
            footer = new PDFText(tmp, PDFWriter.DefaultFont);
            mark = new PDFTranslation(
                footer,
                PageLayout.GetFooterRightXPos(tmp, PDFWriter.DefaultFont),
                PageLayout.FooterYPos
            );
            objects.Add(mark);

            return objects;
        }

        /// <summary>
        /// Creates a PDF page given the main PDF document and a list of rows.
        /// </summary>
        /// <param name="doc">PDFDocument: the main PDF object</param>
        /// <param name="tableWidth">width of the DataTable (needed for scaling the table to fit inside the page)</param>
        /// <param name="columns">columns to show inside the PDF</param>
        /// <param name="rows">rows to show inside the PDF</param>
        /// <param name="title">page title, can be on several lines</param>
        /// <returns>The PDF page created</returns>
        private static PDFPage CreatePage(PDFDocument doc, double tableWidth, List<PDFGraphicObject> columns, List<PDFGraphicObject> rows, List<string> title)
        {
            //Scaling
            double scaling =
                (PageLayout.Width - (PageLayout.RightMargin + PageLayout.LeftMargin)) / (tableWidth);
            if (scaling > 1)
            {
                scaling = 1;
            }
            ////

            //Top position
            double topXPos = (PageLayout.Width / 2) - (tableWidth / 2);
            if (topXPos < PageLayout.LeftMargin)
            {
                topXPos = PageLayout.LeftMargin;
            }
            double topYPos = PageLayout.Height - PageLayout.TopMargin;
            ////

            PDFContentStream contentStream = new PDFContentStream();
            doc.AddChild(contentStream);

            double lineYPos = 0;

            //Page title if any
            List<PDFGraphicObject> fakeList = new List<PDFGraphicObject>();
            foreach (string line in title)
            {
                //Title position
                double lineWidth = FontMetrics.GetTextWidth(line, PDFWriter.TitleFont);
                double lineXPos = (PageLayout.Width / scaling / 2) - (lineWidth / 2);
                if (lineXPos < PageLayout.LeftMargin)
                {
                    lineXPos = PageLayout.LeftMargin;
                }
                ////

                fakeList.Add(CreatePageTitle(line, lineXPos, lineYPos));

                lineYPos -= Table.RowHeight;
            }
            PDFScaling titleScaling = new PDFScaling(fakeList, scaling, 0, topYPos);
            contentStream.AddChild(titleScaling);
            ////

            //Rows and columns should be below the title
            topYPos -= Table.RowHeight * (title.Count + 1) * scaling;

            //Rows
            PDFScaling rowsScaling = new PDFScaling(rows, scaling, topXPos, topYPos);
            contentStream.AddChild(rowsScaling);
            ////

            //Columns
            PDFScaling columnsScaling = new PDFScaling(columns, scaling, topXPos, topYPos);
            contentStream.AddChild(columnsScaling);
            ////

            PDFPage page = new PDFPage();
            page.ContentStream = contentStream;
            page.Fonts = PDFWriter.Fonts;
            doc.AddChild(page);

            return page;
        }

        /// <summary>
        /// Creates the pages contained inside the PDF.
        /// </summary>
        /// 
        /// <remarks>
        /// This method contains the main PDF algorithm.
        /// It splits DataSet rows into several PDF pages.
        /// 
        /// <code>
        ///    ----------------------------
        ///    | Column 1 | Column 2 | ...
        /// y  ----------------------------
        /// ^  | Row 10   | Row 11   | ...
        /// |  | Row 20   | Row 21   | ...
        /// |  | Row 30   | Row 31   | ...
        /// |  ----------------------------
        /// 0 ----> x
        /// </code>
        /// </remarks>
        /// 
        /// <param name="data">DataSet</param>
        /// <param name="doc">Main PDF document</param>
        /// <param name="outlines">PDF outlines so we can add the page to the "bookmarks"/outlines</param>
        /// <returns>The PDF pages (a list of PDFPage)</returns>
        public static PDFPages CreatePages(DataSet data, PDFDocument doc, PDFOutlines outlines)
        {
            PDFPages pages = new PDFPages();

            foreach (DataTable table in data.Tables)
            {
                PDFOutline currentOutline = null;
                if (data.Tables.Count > 1)
                {
                    //More than 1 DataTable thus lets create outlines
                    currentOutline = new PDFOutline(table.TableName);
                    doc.AddChild(currentOutline);
                    outlines.AddOutline(currentOutline);
                }

                List<PDFGraphicObject> rows = new List<PDFGraphicObject>();

                double yPos = -Table.RowHeight;

                //Page title
                //FIXME this is hardcoded
                const double TITLE_HEIGHT = 30;
                List<string> title = new List<string>();
                title.Add("TITLE");
                title.Add("title");
                title.Add(table.TableName);
                ////

                for (int row = 0; row < table.Rows.Count; row++)
                {
                    double totalTableWidth = 0;

                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        double pageHeightLimit = PageLayout.Height - PageLayout.BottomMargin - PageLayout.TopMargin - TITLE_HEIGHT;

                        //Detects end of page
                        bool endOfPage = (-(yPos - Table.RowHeight) >= pageHeightLimit);
                        if (endOfPage)
                        {
                            //Creates the page
                            List<PDFGraphicObject> columns = Table.CreateColumns(table);
                            double tableWidth = Table.GetTableWidth(table);
                            PDFPage page = Page.CreatePage(doc, tableWidth, columns, rows, title);
                            pages.AddPage(page);
                            ////

                            //Add the page to the outline
                            if (currentOutline != null)
                            {
                                if (currentOutline.Page == null)
                                {
                                    currentOutline.Page = page;
                                }
                            }
                            ////

                            //Don't do a Clear() on the list, instead
                            //creates a new copy of the list otherwise
                            //objects referencing this list won't have their own copy
                            rows = new List<PDFGraphicObject>();

                            yPos = -Table.RowHeight;
                        }
                        ////

                        //Create a row
                        string rowName = table.Rows[row][col].ToString();
                        PDFTextBox text = Table.CreateRow(rowName, yPos);
                        PDFTranslation translation = new PDFTranslation(text, totalTableWidth, 0);
                        rows.Add(translation);
                        ////

                        DataColumn column = table.Columns[col];
                        double columnWidth = Table.GetColumnWidth(column, table);
                        totalTableWidth += columnWidth + 2;
                    }

                    //Change Y position inside the coordinate system of PDF
                    yPos -= Table.RowHeight;
                }

                if (rows.Count > 0)
                {
                    //Creates the page
                    List<PDFGraphicObject> columns = Table.CreateColumns(table);
                    double tableWidth = Table.GetTableWidth(table);
                    PDFPage page = Page.CreatePage(doc, tableWidth, columns, rows, title);
                    pages.AddPage(page);
                    ////

                    //Add the page to the outlines
                    if (currentOutline != null)
                    {
                        if (currentOutline.Page == null)
                        {
                            currentOutline.Page = page;
                        }
                    }
                    ////
                }
            }

            return pages;
        }

        private static PDFGraphicObject CreatePageTitle(string title, double xPos, double yPos)
        {
            PDFText pdfTitle = new PDFText(title, PDFWriter.TitleFont);
            PDFTranslation mark = new PDFTranslation(pdfTitle, xPos, yPos);

            return mark;
        }
    }
}
