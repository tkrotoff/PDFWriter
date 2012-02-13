namespace PDF
{
    /// <summary>
    /// A PDF object inside the PDF.
    /// Each of them is uniquely numbered inside the PDF file.
    /// </summary>
    public abstract class PDFStructureObject : IPDFSerialization
    {
        /// <summary>
        /// Unique number of the PDF object.
        /// </summary>
        public int ObjectNumber
        {
            get;
            set;
        }

        public abstract string ToInnerPDF();
    }
}
