using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// An outline item.
    /// </summary>
    /// 
    /// <see cref="PDFOutlines"/>
    /// 
    /// <remarks>
    /// <code>
    /// <![CDATA[
    /// 21 0 obj
    ///     <<
    ///         /Count 6
    ///         /First 22 0 R
    ///         /Last 29 0 R
    ///     >>
    /// endobj
    /// 
    /// 22 0 obj
    ///     <<
    ///         /Title (Chapter 1)
    ///         /Parent 21 0 R
    ///         /Next 26 0 R
    ///         /First 23 0 R
    ///         /Last 25 0 R
    ///         /Count 3
    ///         /Dest [3 0 R /XYZ 0 792 0]
    ///     >>
    /// endobj
    /// ]]>
    /// </code>
    /// </remarks>
    class PDFOutline : PDFStructureObject
    {
        private string _title;

        public PDFOutline(string title)
        {
            _title = "(" + title + ")";
        }

        /// <summary>
        /// Links to the list of outlines: PDFOutlines.
        /// </summary>
        public PDFOutlines Parent
        {
            get;
            set;
        }

        public PDFOutline PrevOutline
        {
            get;
            set;
        }

        public PDFOutline NextOutline
        {
            get;
            set;
        }

        public PDFPage Page
        {
            get;
            set;
        }

        public override string ToInnerPDF()
        {
            //At least one of previous / next outline must exist
            System.Diagnostics.Trace.Assert((PrevOutline != null) || (NextOutline != null));

            System.Diagnostics.Trace.Assert(Page != null);
            System.Diagnostics.Trace.Assert(Parent != null);

            //Gets the previous outline, sometimes there is simply no previous outline
            string prev = string.Empty;
            if (PrevOutline != null)
            {
                prev = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "/Prev {0} 0 R", PrevOutline.ObjectNumber);
            }

            //Gets the next outline, sometimes there is simply no next outline
            string next = string.Empty;
            if (NextOutline != null)
            {
                next = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "/Next {0} 0 R", NextOutline.ObjectNumber);
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFOutline (
{0} 0 obj
    <<
        /Title {1}
        /Parent {2} 0 R
        {3}
        {4}
        /Dest [ {5} 0 R /FitH {6} ]
    >>
endobj
% )
", ObjectNumber, _title, Parent.ObjectNumber, prev, next, Page.ObjectNumber, PDFWriter.PageLayout.Height
            );
        }
    }
}
