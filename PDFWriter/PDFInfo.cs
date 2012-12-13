namespace PDF
{
    /// <summary>
    /// Contains information about the PDF file (title, creator...).
    /// </summary>
    internal class PDFInfo : PDFStructureObject
    {
        private readonly string _title;
        private readonly string _creator;
        private readonly string _producer;

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
", ObjectNumber, _title, _creator, _producer);
        }
    }
}
