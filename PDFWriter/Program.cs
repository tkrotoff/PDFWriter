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

            rows.Add("0,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("1,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("2,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("3,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("4,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("5,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("6,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("7,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("8,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("9,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("10,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("11,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("12,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("13,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("14,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("15,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("16,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("17,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("18,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("19,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("20,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("21,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("22,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("23,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("24,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("25,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("26,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("27,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("28,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("29,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("30,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("31,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("32,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("33,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("34,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("35,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("36,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("37,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("38,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("39,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("40,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("41,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("42,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("43,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("44,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("45,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("46,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("47,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("48,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("49,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("50,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("51,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("52,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("53,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("54,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("55,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("56,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("57,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("58,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("59,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("60,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("61,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("62,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("63,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("64,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("65,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("66,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("67,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("68,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("69,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("70,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("71,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("72,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("73,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("74,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("75,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("76,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("77,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("78,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("79,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("80,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("81,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("82,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("83,DDDD,0.40,450,US,UNITED STATES,30,DELTA");

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

            PDFWriter pdf = new PDFWriter();
            PDFDocument doc = pdf.GetPDFDocument(data);

            //Write the PDF to a file
            StreamWriter file = new StreamWriter(@"C:\Users\Krotoff\Desktop\pdfwriter.pdf");
            file.Write(doc.ToInnerPDF());
            file.Close();
            ////
        }
    }
}
