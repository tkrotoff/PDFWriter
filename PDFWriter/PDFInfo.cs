using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFInfo : PDFObject
    {
        string _title;
        string _creator;
        string _producer;

        public PDFInfo(string title, string creator, string producer)
        {
            _title = title;
            _creator = creator;
            _producer = producer;
        }

        public override string ToInnerPDF()
        {
            return string.Format(@"
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
