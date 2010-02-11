using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFTranslation : PDFGraphicObject
    {
        private PDFGraphicObject _graphicObject;
        private double _width;

        public PDFTranslation(PDFGraphicObject graphicObject, double width)
        {
            _graphicObject = graphicObject;
            _width = width;
        }

        public override string ToInnerPDF()
        {
            return string.Format(@"
% CreateRow (
    % translation width
    q 1 0 0 1 {0} 0 cm
    {1}
    % remove translation width
    Q
% )
", _width, _graphicObject.ToInnerPDF()
            );
        }

    }
}
