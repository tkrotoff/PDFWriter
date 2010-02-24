using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// All the pages (a list of PDFPage) inside the PDF file.
    /// </summary>
    class PDFPages : PDFStructureObject
    {
        private List<PDFPage> _pages = new List<PDFPage>();

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
            System.Diagnostics.Trace.Assert(_pages.Count > 0);

            PageLayout layout = new PageLayout();

            //Faster when using StringBuilder instead of string
            //See http://dotnetperls.com/stringbuilder-1
            StringBuilder tmp = new StringBuilder();
            foreach (PDFPage page in _pages)
            {
                tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
                {0} 0 R", page.ObjectNumber);
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFPages (
{0} 0 obj
    <<
        /Type /Pages
        /Kids
            [{1}
            ]
        /Count {2}
        /MediaBox [0 0 {3} {4}]
    >>
endobj
% )
", ObjectNumber, tmp, _pages.Count, layout.Width, layout.Height
            );
        }

    }
}
