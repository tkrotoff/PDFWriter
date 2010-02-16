using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFPage : PDFStructureObject
    {
        public PDFPages Parent
        {
            get;
            set;
        }

        public PDFContentStream ContentStream
        {
            get;
            set;
        }

        public List<PDFFont> Fonts
        {
            get;
            set;
        }

        public override string ToInnerPDF()
        {
            System.Diagnostics.Trace.Assert(Parent != null);
            System.Diagnostics.Trace.Assert(ContentStream != null);
            System.Diagnostics.Trace.Assert(Fonts.Count > 0);

            string fonts = string.Empty;
            foreach (PDFFont font in Fonts)
            {
                fonts += string.Format(@"
                        /{0} {1} 0 R", font.FontName, font.ObjectNumber);
            }

            return string.Format(@"
% PDFPage (
{0} 0 obj
    <<
        /Type /Page
        /Parent {1} 0 R
        /Contents {2} 0 R
        /Resources
            <<
                /ProcSet [/PDF /Text]
                /Font
                    <<{3}
                    >>
            >>
    >>
endobj
% )
", ObjectNumber, Parent.ObjectNumber, ContentStream.ObjectNumber, fonts
            );
        }

    }
}
