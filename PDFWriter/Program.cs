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

        static void Main(string[] args)
        {
            DataSet data = CreateDataSet();

            PDFWriter pdf = new PDFWriter();
            PDFRoot root = pdf.GetPDFRoot(data);

            //Write the PDF to a file
            StreamWriter file = new StreamWriter(@"C:\Users\Krotoff\Desktop\pdfwriter.pdf");
            file.Write(root.ToInnerPDF());
            file.Close();
            ////
        }
    }
}
