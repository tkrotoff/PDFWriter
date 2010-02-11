using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFCatalog : PDFStructureObject
    {
        private PDFOutlines _outlines;
        private PDFPages _pages;

        public PDFCatalog(PDFOutlines outlines, PDFPages pages)
        {
            _outlines = outlines;
            _pages = pages;
        }

        public override string ToInnerPDF()
        {
            return string.Format(@"
% PDFCatalog (
{0} 0 obj
    <<
        /Type /Catalog
        /Outlines {1} 0 R
        /Pages {2} 0 R
    >>
endobj
% )
", ObjectNumber, _outlines.ObjectNumber, _pages.ObjectNumber
            );
        }

    }
}
