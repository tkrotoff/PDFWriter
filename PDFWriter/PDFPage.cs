using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFPage : PDFStructureObject
    {
        private List<PDFFont> _fonts;
        private PDFContentStream _contentStream;

        public PDFPage(List<PDFFont> fonts, PDFContentStream contentStream)
        {
            _fonts = fonts;
            _contentStream = contentStream;
        }

        public PDFPages Parent
        {
            get;
            set;
        }

        public PDFContentStream ContentStream
        {
            get { return _contentStream; }
        }

        public override string ToInnerPDF()
        {
            string fonts = string.Empty;
            foreach (PDFFont font in _fonts)
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
", ObjectNumber, Parent.ObjectNumber, _contentStream.ObjectNumber, fonts
            );
        }

    }
}
