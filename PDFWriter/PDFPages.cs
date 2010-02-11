using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFPages : PDFStructureObject
    {
        private List<PDFPage> _pages = new List<PDFPage>();

        public virtual void AddPage(PDFPage page)
        {
            page.Parent = this;
            _pages.Add(page);
        }

        public override string ToInnerPDF()
        {
            PageLayout layout = new PageLayout();

            string tmp = string.Empty;
            foreach (PDFPage page in _pages)
            {
                tmp += string.Format(@"
                {0} 0 R", page.ObjectNumber);
            }

            return string.Format(@"
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
