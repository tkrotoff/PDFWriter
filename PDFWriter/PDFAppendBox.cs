using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFAppendBox : PDFObject
    {
        private List<PDFObject> _boxes;
        private double _scaling;
        private double _xPos;
        private double _yPos;

        public PDFAppendBox(List<PDFObject> boxes, double scaling, double xPos, double yPos)
        {
            _boxes = boxes;
            _scaling = scaling;
            _xPos = xPos;
            _yPos = yPos;
        }

        public override string ToInnerPDF()
        {
            string tmp = string.Empty;
            foreach (PDFObject pdfObject in _boxes)
            {
                tmp += "    " + pdfObject.ToInnerPDF();
            }

            return string.Format(@"
% PDFAppendBox (
    q {0} 0 0 {0} {1} {2} cm
{3}
    Q
%)", _scaling, _xPos, _yPos, tmp
            );
        }

    }
}
