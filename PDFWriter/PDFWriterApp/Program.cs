using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using PDFWriter;

namespace PDFWriterApp
{
    /// <summary>
    /// PDFWriter application (.exe above PDFWriter.dll).
    /// </summary>
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
            rows.Add("84,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("85,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("86,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("87,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("88,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("89,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("90,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("91,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("92,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("93,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("94,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("95,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("96,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("97,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("98,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("99,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("100,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("101,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("102,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("103,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("104,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("105,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("106,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("107,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("108,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("109,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("110,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("111,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("112,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("113,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("114,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("115,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("116,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("117,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("118,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("119,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("120,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("121,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("122,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("123,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("124,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("125,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("126,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("127,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("128,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("129,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("130,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("131,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("132,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("133,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("134,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("135,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("136,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("137,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("138,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("139,DDDD,0.40,450,US,UNITED STATES,30,DELTA");
            rows.Add("140,DDDD,0.40,450,US,UNITED STATES,30,DELTA");

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
            //newDataSet.ReadXml("MSPAENG.xml");

            return newDataSet;
        }

        static void Main(string[] args)
        {
            DataSet data = CreateDataSet();

            string pdf = PDFWriter.PDFWriter.GetPDF(data);

            //Write the PDF to a file
            StreamWriter file = new StreamWriter("pdfwriter.pdf");
            file.Write(pdf);
            file.Close();
            ////
        }
    }
}
