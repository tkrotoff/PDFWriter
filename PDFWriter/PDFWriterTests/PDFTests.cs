using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using NUnit.Framework;

using PDF;

namespace PDFTests
{
    [TestFixture]
    class PDFTests
    {
        [Test]
        public void Test1PagePDF()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../1page.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            PDFWriter.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            /*StreamWriter fileWriter = new StreamWriter("../../1page.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();*/

            StreamReader file = new StreamReader("../../1page.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        [Test]
        public void Test2PagesPDF()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../2pages.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            PDFWriter.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            /*StreamWriter fileWriter = new StreamWriter("../../2pages.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();*/

            StreamReader file = new StreamReader("../../2pages.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        [Test]
        public void Test3PagesPDF()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../3pages.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            PDFWriter.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            PDFInfo info = new PDFInfo("toto", "toto", "toto");

            /*StreamWriter fileWriter = new StreamWriter("../../3pages.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();*/

            StreamReader file = new StreamReader("../../3pages.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        public void TestTableScaling()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../tablescaling.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            PDFWriter.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            /*StreamWriter fileWriter = new StreamWriter("../../tablescaling.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();*/

            StreamReader file = new StreamReader("../../tablescaling.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }
    }
}
