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
    /// For more technical informations about the PDF format, see PDFDocument.
    /// 
    /// This class implements the algorithms that use PDFGraphicObjects and PDFStructureObjects
    /// in order to create a PDF file.
    /// The main difficulty is to split DataSet rows on several pages.
    /// 
    /// Main method is CreatePages(), other methods are just helper methods.
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
            int count = 1;
            foreach (PDFPage page in pages.Pages)
            {
                List<PDFGraphicObject> header = Page.CreateHeader();
                page.ContentStream.AddRange(header);

                List<PDFGraphicObject> footer = Page.CreateFooter(count, pages.Pages.Count);
                page.ContentStream.AddRange(footer);

                count++;
            }
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
