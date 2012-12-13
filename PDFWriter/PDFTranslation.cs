namespace PDF
{
    /// <summary>
    /// Performs a translation on a PDF graphical object.
    /// </summary>
    internal class PDFTranslation : PDFGraphicObject
    {
        private readonly PDFGraphicObject _graphicObject;
        private readonly double _xPos;
        private readonly double _yPos;

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
    q 1 0 0 1 {0} {1} cm
    {2}
    Q
% )
", _xPos, _yPos, _graphicObject.ToInnerPDF());
        }
    }
}
