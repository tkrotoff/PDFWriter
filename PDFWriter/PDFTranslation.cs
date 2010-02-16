using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFTranslation : PDFGraphicObject
    {
        private PDFGraphicObject _graphicObject;
        private double _xPos;
        private double _yPos;

        public PDFTranslation(PDFGraphicObject graphicObject, double xPos, double yPos)
        {
            _graphicObject = graphicObject;
            _xPos = xPos;
            _yPos = yPos;
        }

        public override string ToInnerPDF()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFTranslation (
    % Translation
    q 1 0 0 1 {0} {1} cm
    {2}
    Q
% )
", _xPos, _yPos, _graphicObject.ToInnerPDF()
            );
        }

    }
}
