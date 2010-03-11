using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using PDF;

namespace PDFWriterApp
{
    /// <summary>
    /// PDFWriter application (.exe above PDFWriter.dll).
    /// </summary>
    class Program
    {

        DataTable CreateDataTable()
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

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling1";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling2";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling3";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling4";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling5";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling6";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling7";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling8";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling9";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling10";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Scaling11";
            table.Columns.Add(column);

            List<string> rows = new List<string>();
            rows.Add("AAAA,AAAA,0.10,100,US,UNITED STATES,10,ALPHA,scaling1,scaling2,scaling3,scaling4,scaling5,scaling6,scaling7,scaling8,scaling9,scaling10,scaling11");
            rows.Add("BBBB,BBBB,0.20,150,CA,CANADA,20,BETA,scaling1,scaling2,scaling3,scaling4,scaling5,scaling6,scaling7,scaling8,scaling9,scaling10,scaling11");
            rows.Add("CCCC,CCCC,0.30,300,CA,CANADA,20,BETA,scaling1,scaling2,scaling3,scaling4,scaling5,scaling6,scaling7,scaling8,scaling9,scaling10,scaling11");
            rows.Add("DDDD,DDDD,0.40,450,US,UNITED STATES,30,DELTA,scaling1,scaling2,scaling3,scaling4,scaling5,scaling6,scaling7,scaling8,scaling9,scaling10,scaling11");

            return table;
        }


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
            //newDataSet.ReadXml("data.xml");
            newDataSet.ReadXml("MSPAENG.xml");

            return newDataSet;
        }

        static void Main(string[] args)
        {
            DataSet data = CreateDataSet();

            string pdf = PDFWriter.GetPDF(data);

            //Write the PDF to a file
            StreamWriter file = new StreamWriter("pdfwriter.pdf");
            file.Write(pdf);
            file.Close();
            ////
        }
    }
}
