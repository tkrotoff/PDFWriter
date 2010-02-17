using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// Graphical primitives for displaying text and graphics inside a PDF file.
    /// This is used by PDFContentStream.
    /// </summary>
    abstract class PDFGraphicObject : IPDFSerialization
    {
        public abstract string ToInnerPDF();
    }
}
