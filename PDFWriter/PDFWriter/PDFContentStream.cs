using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// Contains all the graphic primitives (text, translation, graphics...) to display inside the PDF file.
    /// 
    /// This class is pretty important, it contains a list of all the PDFGraphicObjects to display.
    /// </summary>
    class PDFContentStream : PDFStructureObject
    {
        private List<PDFGraphicObject> _childs = new List<PDFGraphicObject>();

        public void AddChild(PDFGraphicObject pdfGraphicObject)
        {
            _childs.Add(pdfGraphicObject);
        }

        public void AddRange(List<PDFGraphicObject> pdfGraphicObjects)
        {
            _childs.AddRange(pdfGraphicObjects);
        }

        public override string ToInnerPDF()
        {
            System.Diagnostics.Trace.Assert(_childs.Count > 0);

            string tmp = string.Empty;
            foreach (PDFGraphicObject pdfGraphicObject in _childs)
            {
                tmp += pdfGraphicObject.ToInnerPDF();
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
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
