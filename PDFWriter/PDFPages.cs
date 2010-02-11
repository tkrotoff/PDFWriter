using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFPages : PDFObject
    {
        public override string ToInnerPDF()
        {
            PageLayout layout = new PageLayout();

            string pages = string.Empty;
            foreach (PDFPage page in Childs)
            {
                pages += string.Format(@"
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
", ObjectNumber, pages, Childs.Count, layout.Width, layout.Height
            );
        }

    }
}
