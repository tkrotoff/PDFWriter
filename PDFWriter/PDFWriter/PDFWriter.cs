using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDF
{
    /// <summary>
    /// PDF writer library: generates a PDF file from a DataSet, see PDFWriter and PDFDocument for more documentation.
    /// </summary>
    /// <see cref="PDFWriter"/>
    /// <see cref="PDFDocument"/>
    /// 
    /// @mainpage
    public static class NamespaceDoc
    {
        //Special trick to document the namespace
        //See http://stackoverflow.com/questions/793210/c-xml-documentation-for-a-namespace
    }

    /// <summary>
    /// PDF writer library: generates a PDF file from a DataSet.
    /// </summary>
    /// 
    /// <remarks>
    /// See <a href="http://wiki/wiki/index.php/PDFWriter">Wiki page about PDFWriter</a><br/>
    /// See <a href="http://en.wikipedia.org/wiki/PDF_format">Wikipedia page about PDF</a><br/>
    /// For more technical informations about the PDF format, see PDFDocument.<br/>
    /// <br/>
    /// PDFWriter has been tested with Acrobat Reader 9.1.0, Foxit Reader 2.3 and Sumatra PDF 1.0.1<br/>
    /// <br/>
    /// 
    /// Other C# PDF libraries:<br/>
    /// <code>
    /// http://csharp-source.net/open-source/pdf-libraries
    /// 
    /// ** iTextSharp
    /// Affero GNU Public License/Commercial license
    /// http://sourceforge.net/projects/itextsharp/
    /// Last version: 5.0.0 2009-12-08
    /// Last commit: 2010-01-04
    /// 93% of 141 users recommend this project
    /// 
    /// ** Report.NET
    /// LGPL
    /// http://sourceforge.net/projects/report/
    /// Last version: 0.09.05 2006-11-13
    /// No repository
    /// 100% of 8 users recommend this project
    /// 
    /// ** PDFsharp
    /// MIT
    /// http://www.pdfsharp.net/
    /// http://sourceforge.net/projects/pdfsharp/
    /// http://pdfsharp.codeplex.com/
    /// Last version: 1.31 2009-12-09
    /// No repository?
    /// 90% of 20 users recommend this project
    /// 
    /// ** PDFjet for .NET
    /// http://pdfjet.com/net/index.html
    /// Evaluation License
    /// 
    /// ** ASP.NET FO PDF
    /// LGPL
    /// http://sourceforge.net/projects/npdf/
    /// Last version: 1.0.1439.19630 2003-12-16
    /// No repository
    /// 100% of 1 user recommends this project
    /// 
    /// ** PDF Clown
    /// LGPL
    /// http://sourceforge.net/projects/clown/
    /// Last version: 0.0.7-Alpha 2009-01-02
    /// No repository
    /// 75% of 4 users recommend this project
    /// 
    /// Export dataset to PDF in .Net
    /// http://www.eggheadcafe.com/community/aspnet/2/10041208/export-dataset-to-pdf-in.aspx
    /// </code>
    /// 
    /// <br/>
    /// This class implements the algorithms that use PDFGraphicObjects and PDFStructureObjects
    /// in order to create a PDF file. The main difficulty is to split DataSet rows on several pages.
    /// <br/>
    /// Main method is Page.CreatePages(), other methods available inside classes Page and Table
    /// are just helper methods.
    /// </remarks>
    public static class PDFWriter
    {
        /// <summary>
        /// Default font.
        /// </summary>
        public static Font DefaultFont
        {
            //TODO use a style template to get this property
            get { return new Font(Font.Helvetica, 9); }
        }

        /// <summary>
        /// Default bold font.
        /// </summary>
        public static Font DefaultBoldFont
        {
            //TODO use a style template to get this property
            get { return new Font(Font.HelveticaBold, 9); }
        }

        /// <summary>
        /// Title font.
        /// </summary>
        public static Font TitleFont
        {
            //TODO use a style template to get this property
            get { return new Font(Font.HelveticaBold, 14, Color.Green); }
        }

        /// <summary>
        /// A PDF cellule background color, see PDFTextBox.
        /// </summary>
        public static string CellBackgroundColor
        {
            get
            {
                //TODO use a style template to get this property
                return Color.Silver;
            }
        }

        /// <summary>
        /// Gets the list of available fonts as a list of PDF objects.
        /// </summary>
        public static List<PDFFont> Fonts
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
        /// Main function: gets the PDF given a DataSet.
        /// </summary>
        /// <param name="data">DataSet to convert into a PDF</param>
        /// <returns>The PDF</returns>
        public static string GetPDF(DataSet data)
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
            PDFPages pages = Page.CreatePages(data, doc, outlines);
            doc.AddChild(pages);
            ////

            //Add headers and footers
            /*int count = 1;
            foreach (PDFPage page in pages.Pages)
            {
                List<PDFGraphicObject> header = Page.CreateHeader();
                page.ContentStream.AddRange(header);

                List<PDFGraphicObject> footer = Page.CreateFooter(count, pages.Pages.Count);
                page.ContentStream.AddRange(footer);

                count++;
            }*/
            ////

            //Catalog
            PDFCatalog catalog = new PDFCatalog(outlines, pages);
            doc.Catalog = catalog;
            doc.AddChild(catalog);
            ////

            return doc.ToInnerPDF();
        }
    }
}
