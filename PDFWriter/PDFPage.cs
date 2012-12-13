namespace PDF
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A page inside the PDF.
    /// </summary>
    internal class PDFPage : PDFStructureObject
    {
        /// <summary>
        /// Links to the list of pages: PDFPages.
        /// </summary>
        public PDFPages Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Links to the content stream that contains text, graphics...
        /// (so the real content of the PDF).
        /// </summary>
        public PDFContentStream ContentStream
        {
            get;
            set;
        }

        /// <summary>
        /// Fonts available for use inside this PDF page.
        /// </summary>
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

            StringBuilder fonts = new StringBuilder();
            foreach (PDFFont font in Fonts)
            {
                fonts.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
                        /{0} {1} 0 R", font.FontName, font.ObjectNumber);
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
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
", ObjectNumber, Parent.ObjectNumber, ContentStream.ObjectNumber, fonts);
        }
    }
}
