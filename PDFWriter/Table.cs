using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDF
{
    /// <summary>
    /// Functions related to a table.
    /// </summary>
    /// 
    /// <remarks>
    /// This class implements the algorithms that use PDFGraphicObjects and PDFStructureObjects
    /// in order to create a PDF file. The main difficulty is to split DataSet rows on several pages.
    /// <br/>
    /// Main method is Page.CreatePages(), other methods available inside classes Page and Table
    /// are just helper methods.
    /// </remarks>
    static class Table
    {
        /// <summary>
        /// Height of a row.
        /// </summary>
        public static double RowHeight
        {
            //TODO compute it using font metrics
            get { return 13; }
        }

        /// <summary>
        /// Gets the largest width (in the context of a PDF element of course) possible of a column.
        /// </summary>
        /// <remarks>
        /// This method could be merged with CreatePages() in order to avoid unwanted loops.
        /// Unfortunately this would make the source code less readable.
        /// </remarks>
        /// <param name="column">The column</param>
        /// <param name="table">The DataTable so we can iterates over the rows for the given column</param>
        /// <returns>Largest possible width of the given column</returns>
        public static double GetColumnWidth(DataColumn column, DataTable table)
        {
            double columnWidth = FontMetrics.GetTextWidth(column.ColumnName, PDFWriter.DefaultBoldFont);
            foreach (DataRow row in table.Rows)
            {
                string rowName = row[column].ToString();

                double tmp = FontMetrics.GetTextWidth(rowName, PDFWriter.DefaultFont);
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
        /// <remarks>
        /// This method could be merged with CreatePages() in order to avoid unwanted loops.
        /// Unfortunately this would make the source code less readable.
        /// </remarks>
        /// <param name="table">DataTable</param>
        /// <returns>Witdh of the DataTable</returns>
        public static double GetTableWidth(DataTable table)
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
        /// <remarks>
        /// This method could be merged with CreatePages() in order to avoid unwanted loops.
        /// Unfortunately this would make the source code less readable.
        /// </remarks>
        /// <param name="table">The DataTable from which to extract the columns</param>
        /// <returns>The DataTable columns</returns>
        public static List<PDFGraphicObject> CreateColumns(DataTable table)
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
        private static PDFTextBox CreateColumn(string columnName, double columnWidth)
        {
            PDFText text = new PDFText(columnName, PDFWriter.DefaultFont);
            double width = columnWidth;
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(
                text,
                margin,
                padding,
                0,
                0,
                PDFWriter.CellBackgroundColor,
                width,
                Table.RowHeight
            );
            return box;
        }

        /// <summary>
        /// Creates a row as a PDFGraphicObject.
        /// </summary>
        /// <param name="rowName">Title/name of the row</param>
        /// <param name="yPos">Y position of the row inside the PDF page</param>
        /// <returns>A PDFGraphicObject representing the row</returns>
        public static PDFTextBox CreateRow(string rowName, double yPos)
        {
            Font font = new Font(Font.Helvetica, 9, Color.Green);

            //A string should be green
            int rowNameInt32;
            bool resultInt32 = Int32.TryParse(rowName, out rowNameInt32);
            double rowNameDouble;
            bool resultDouble = Double.TryParse(rowName, out rowNameDouble);
            if (resultInt32 || resultDouble)
            {
                if (rowNameInt32 < 0 || rowNameDouble < 0)
                {
                    //A negative number should be red
                    font.Color = Color.Red;
                }
                else
                {
                    //A positive number should be blue
                    font.Color = Color.Blue;
                }
            }
            PDFText text = new PDFText(rowName, font);
            int margin = 1;
            int padding = 1;
            PDFTextBox box = new PDFTextBox(text, margin, padding, 0, yPos);
            return box;
        }
    }
}
