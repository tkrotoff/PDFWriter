using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// Performs a scaling on a PDF graphical object.
    /// </summary>
    class PDFScaling : PDFGraphicObject
    {
        private List<PDFGraphicObject> _graphicObjects;
        private double _scaling;
        private double _xPos;
        private double _yPos;

        public PDFScaling(List<PDFGraphicObject> graphicObjects, double scaling, double xPos, double yPos)
        {
            _graphicObjects = graphicObjects;
            _scaling = scaling;
            _xPos = xPos;
            _yPos = yPos;
        }

        public override string ToInnerPDF()
        {
            string tmp = string.Empty;
            foreach (PDFGraphicObject graphicObject in _graphicObjects)
            {
                tmp += "    " + graphicObject.ToInnerPDF();
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFScaling (
    q {0} 0 0 {0} {1} {2} cm
{3}
    Q
% )
", _scaling, _xPos, _yPos, tmp
            );
        }

    }
}
