namespace PDF
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Performs a scaling on a PDF graphical object.
    /// </summary>
    internal class PDFScaling : PDFGraphicObject
    {
        private readonly List<PDFGraphicObject> _graphicObjects;
        private readonly double _scaling;
        private readonly double _xPos;
        private readonly double _yPos;

        public PDFScaling(List<PDFGraphicObject> graphicObjects, double scaling, double xPos, double yPos)
        {
            _graphicObjects = graphicObjects;
            _scaling = scaling;
            _xPos = xPos;
            _yPos = yPos;
        }

        public override string ToInnerPDF()
        {
            StringBuilder tmp = new StringBuilder();
            foreach (PDFGraphicObject graphicObject in _graphicObjects)
            {
                tmp.Append("    " + graphicObject.ToInnerPDF());
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFScaling (
    q {0} 0 0 {0} {1} {2} cm
{3}
    Q
% )
", _scaling, _xPos, _yPos, tmp);
        }
    }
}
