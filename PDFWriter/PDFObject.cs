using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    abstract class PDFObject
    {
        public int ObjectNumber
        {
            get;
            set;
        }

        public PDFObject Parent
        {
            get;
            set;
        }

        private List<PDFObject> _childs = new List<PDFObject>();

        public List<PDFObject> Childs
        {
            get
            {
                return _childs;
            }
        }

        public virtual void AddChild(PDFObject pdfObject)
        {
            pdfObject.Parent = this;
            _childs.Add(pdfObject);
        }

        public abstract string ToInnerPDF();
    }
}
