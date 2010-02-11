using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFBox : PDFGraphicObject
    {
        private PDFText _text;
        private int _margin;
        private int _padding;
        private double _xPos;
        private double _yPos;

        private string _backgroundColor;
        private double _width;
        private double _height;

        public PDFBox(PDFText text, int margin, int padding, double xPos, double yPos,
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

        public PDFBox(PDFText text, int margin, int padding, double xPos, double yPos)
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

            if (_margin != 0)
            {
                tmp.AppendFormat(@"
    % translation margin
    1 0 0 1 {0} {0} cm", _margin
                );
            }

            if (_width != 0 && _height != 0)
            {
                tmp.AppendFormat(@"
    {0} rg
    0 0 {1} {2} re
    f", _backgroundColor, _width, _height
                );
            }

            if (_padding != 0)
            {
                tmp.AppendFormat(@"
    % translation padding
    1 0 0 1 {0} {0} cm", _padding
                );
            }

            tmp.AppendFormat(@"
    % translation position
    1 0 0 1 {0} {1} cm
    {2}", _xPos, _yPos, _text.ToInnerPDF()
            );

            tmp.AppendFormat(@"
    % remove translation margin, padding, position
    Q
% )
"
            );

            return tmp.ToString();
        }

    }
}
