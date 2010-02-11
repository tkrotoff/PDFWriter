using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    abstract class PDFStructureObject : IPDFSerialization
    {
        public int ObjectNumber
        {
            get;
            set;
        }

        public abstract string ToInnerPDF();
    }
}
