using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    abstract class PDFGraphicObject : IPDFSerialization
    {
        public abstract string ToInnerPDF();
    }
}
