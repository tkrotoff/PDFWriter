using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// Allows to serialize every PDF object.
    /// 
    /// PDF contains two types of object (both inheriting this class):
    /// - PDF object that represents the structure of the document
    /// - PDF graphic primitives (text, graphics, scaling, transformation...) used inside the content stream
    /// </summary>
    interface IPDFSerialization
    {
        /// <summary>
        /// PDF string of the PDF object.
        /// This method produces valid PDF for each PDF object.
        /// </summary>
        /// <returns>string of the PDF object</returns>
        string ToInnerPDF();
    }
}
