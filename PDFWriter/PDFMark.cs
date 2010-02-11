using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFMark : PDFGraphicObject
    {
        PDFText _text;
        double _xPos;
        double _yPos;

        public PDFMark(PDFText text, double xPos, double yPos)
        {
            _text = text;
            _xPos = xPos;
            _yPos = yPos;
        }

        public override string ToInnerPDF()
        {
            return string.Format(@"
% PDFMark (
    % translation x=?,y=?
    q 1 0 0 1 {0} {1} cm
    {2}
    % remove translation x=?,y=?
    Q
% )
", _xPos, _yPos, _text.ToInnerPDF()
            );
        }

    }
}
