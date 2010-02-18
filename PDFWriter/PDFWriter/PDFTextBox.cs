using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
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
    class PDFTextBox : PDFGraphicObject
    {
        private PDFText _text;
        private int _margin;
        private int _padding;
        private double _xPos;
        private double _yPos;

        private string _backgroundColor;
        private double _width;
        private double _height;

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
% PDFBox (
    q"
            );

            tmp.AppendFormat(@"
    % Margin
    1 0 0 1 {0} {0} cm", _margin
            );

            if (_width != 0 && _height != 0)
            {
                tmp.AppendFormat(@"
    % Background color
    {0} rg
    % Rectangle
    0 0 {1} {2} re
    f", _backgroundColor, _width, _height
                );
            }

            tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
    % Padding
    1 0 0 1 {0} {0} cm", _padding
            );

            tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
    % Position
    1 0 0 1 {0} {1} cm
    {2}", _xPos, _yPos, _text.ToInnerPDF()
            );

            tmp.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, @"
    Q
% )
"
            );

            return tmp.ToString();
        }

    }
}
