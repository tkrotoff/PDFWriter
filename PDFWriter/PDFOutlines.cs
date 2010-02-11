using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFOutlines : PDFStructureObject
    {
        public override string ToInnerPDF()
        {
            return string.Format(@"
% PDFOutlines (
{0} 0 obj
    <<
        /Type /Outlines
        /Count 0
    >>
endobj
% )
", ObjectNumber
            );
        }

    }
}
