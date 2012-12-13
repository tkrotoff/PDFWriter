namespace PDF
{
    using System.Text;

    /// <summary>
    /// Displays a box (rectangle) with a text inside it.
    /// </summary>
    /// 
    /// <remarks>
    /// The box accepts a background color and other parameters.
    /// <code>
    /// -----------------------
    /// |                     |
    /// | Text inside the box |
    /// |                     |
    /// -----------------------
    /// </code>
    /// </remarks>
    internal class PDFTextBox : PDFGraphicObject
    {
        private readonly PDFText _text;
        private readonly int _margin;
        private readonly int _padding;
        private readonly double _xPos;
        private readonly double _yPos;

        private readonly string _backgroundColor;
        private readonly double _width;
        private readonly double _height;

        public PDFTextBox(PDFText text, int margin, int padding, double xPos, double yPos,
                string backgroundColor, double width, double height)
        {
            _text = text;
            _margin = margin;
            _padding = padding;
            _xPos = xPos;
            _yPos = yPos;

            _backgroundColor = backgroundColor;
            _width = width;
            _height = height;
        }

        public PDFTextBox(PDFText text, int margin, int padding, double xPos, double yPos)
            : this(text, margin, padding, xPos, yPos, Color.NoColor, 0, 0)
        {
        }

        public override string ToInnerPDF()
        {
            StringBuilder tmp = new StringBuilder();
            tmp.AppendLine(@"
% PDFTextBox (
    q");

            tmp.AppendFormat(@"
    1 0 0 1 {0} {0} cm", _margin);

            if (_width != 0 && _height != 0)
            {
                tmp.AppendFormat(@"
    {0} rg
    0 0 {1} {2} re
    f", _backgroundColor, _width, _height);
            }

            tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
    1 0 0 1 {0} {0} cm", _padding);

            tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
    1 0 0 1 {0} {1} cm
    {2}", _xPos, _yPos, _text.ToInnerPDF());

            tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
    Q
% )
");

            return tmp.ToString();
        }
    }
}
