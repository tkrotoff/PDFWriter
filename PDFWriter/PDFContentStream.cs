using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFContentStream : PDFObject
    {
        public override string ToInnerPDF()
        {
            string tmp = string.Empty;
            foreach (PDFObject pdfObject in Childs)
            {
                tmp += pdfObject.ToInnerPDF();
            }

            return string.Format(@"
% PDFContentStream (
{0} 0 obj
    <<
        /Length {1}
    >>
stream
{2}
endstream
endobj
% )
", ObjectNumber, tmp.Length, tmp
            );
        }

    }
}
