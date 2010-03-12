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
        public void TestEmptyDataSet()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../emptydataset.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            Page.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../emptydataset_generated.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

            StreamReader file = new StreamReader("../../emptydataset.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        [Test]
        public void TestMinimumDataSet()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../minimumdataset.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            Page.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../minimumdataset_generated.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

            StreamReader file = new StreamReader("../../minimumdataset.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        [Test]
        public void Test1PagePDF()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../1page.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            Page.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../1page_generated.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

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
            Page.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../2pages_generated.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

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
            Page.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../3pages_generated.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

            StreamReader file = new StreamReader("../../3pages.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        [Test]
        public void TestTableScaling()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../tablescaling.xml");

            PageLayout pageLayout = new PageLayout();
            pageLayout.RightHeader = "Current Date";
            Page.PageLayout = pageLayout;

            string tmp = PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../tablescaling_generated.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

            StreamReader file = new StreamReader("../../tablescaling.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }
    }
}
