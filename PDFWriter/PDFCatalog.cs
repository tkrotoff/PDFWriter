using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// PDF catalog.
    /// </summary>
    /// <remarks>
    /// From PDF 1.3 documentation:<br/>
    /// The catalog contains references to other objects defining the document's contents, outline,
    /// article threads (PDF 1.1), named destinations, and other attributes.<br/>
    /// In addition, it contains information about how the document should be displayed
    /// on the screen, such as whether its outline and thumbnail page images should be
    /// displayed automatically and whether some location other than the first page
    /// should be shown when the document is opened.
    /// </remarks>
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
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
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
