using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFRow : PDFObject
    {
        private PDFBox _box;
        private double _width;

        public PDFRow(PDFBox box, double width)
        {
            _box = box;
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
%)", _width, _box.ToInnerPDF()
            );
        }

    }
}
