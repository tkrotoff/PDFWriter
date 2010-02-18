using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// Contains information about the PDF file (title, creator...).
    /// </summary>
    class PDFInfo : PDFStructureObject
    {
        private string _title;
        private string _creator;
        private string _producer;

        public PDFInfo(string title, string creator, string producer)
        {
            _title = title;
            _creator = creator;
            _producer = producer;
        }

        public override string ToInnerPDF()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFInfo (
{0} 0 obj
    <<
        /Title ({1})
        /Creator ({2})
        /Producer ({3})
    >>
endobj
% )
", ObjectNumber, _title, _creator, _producer
            );

        }
    }
}
