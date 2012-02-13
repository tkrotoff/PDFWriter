using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// Allows to serialize every PDF object.
    /// </summary>
    /// 
    /// <remarks>
    /// PDF contains two types of object (both inheriting this class):<br/>
    /// - PDF object that represents the structure of the document<br/>
    /// - PDF graphic primitives (text, graphics, scaling, transformation...) used inside the content stream
    /// </remarks>
    interface IPDFSerialization
    {
        /// <summary>
        /// PDF string of the PDF object.
        /// This method produces a valid PDF piece of text for each PDF object.
        /// </summary>
        /// <returns>string of the PDF object</returns>
        string ToInnerPDF();
    }
}
