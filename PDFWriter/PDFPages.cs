namespace PDF
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// All the pages (a list of PDFPage) inside the PDF file.
    /// </summary>
    internal class PDFPages : PDFStructureObject
    {
        private readonly List<PDFPage> _pages = new List<PDFPage>();

        public void AddPage(PDFPage page)
        {
            page.Parent = this;
            _pages.Add(page);
        }

        public List<PDFPage> Pages
        {
            get { return _pages; }
        }

        public override string ToInnerPDF()
        {
            // 0 pages is possible
            //System.Diagnostics.Trace.Assert(_pages.Count > 0);

            PageLayout layout = new PageLayout();

            StringBuilder tmp = new StringBuilder();
            foreach (PDFPage page in _pages)
            {
                tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0} 0 R ", page.ObjectNumber);
            }

            string kids = string.Empty;
            if (tmp.Length > 0)
            {
                kids = string.Format("/Kids[{0}]", tmp);
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFPages (
{0} 0 obj
    <<
        /Type /Pages
        {1}
        /Count {2}
        /MediaBox [0 0 {3} {4}]
    >>
endobj
% )
", ObjectNumber, kids, _pages.Count, layout.Width, layout.Height);
        }
    }
}
