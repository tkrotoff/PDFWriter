using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// PDF outlines.
    /// </summary>
    /// 
    /// <remarks>
    /// From PDF 1.3 documentation:<br/>
    /// <br/>
    /// A PDF document may optionally display a document outline on the screen, allowing
    /// the user to navigate interactively from one part of the document to another.<br/>
    /// <br/>
    /// The outline consists of a tree-structured hierarchy of outline items (sometimes
    /// called bookmarks), which serve as a "visual table of contents" to display the document's
    /// structure to the user. The user can interactively open and close individual
    /// items by clicking them with the mouse. When an item is open, its immediate children
    /// in the hierarchy become visible on the screen; each child may in turn be
    /// open or closed, selectively revealing or hiding further parts of the hierarchy.<br/>
    /// <br/>
    /// When an item is closed, all of its descendants in the hierarchy are hidden. Clicking
    /// the text of any visible item with the mouse activates the item, causing the
    /// viewer application to jump to a destination or trigger an action associated with
    /// the item.<br/>
    /// <br/>
    /// The root of a document's outline hierarchy is an outline dictionary specified by
    /// the Outlines entry in the document catalog.<br/>
    /// <br/>
    /// Example:
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
    class PDFOutlines : PDFStructureObject
    {
        private List<PDFOutline> _outlines = new List<PDFOutline>();

        public void AddOutline(PDFOutline outline)
        {
            outline.Parent = this;
            if (_outlines.Count > 0)
            {
                outline.PrevOutline = _outlines.Last();
                _outlines.Last().NextOutline = outline;
            }
            _outlines.Add(outline);
        }

        public override string ToInnerPDF()
        {
            //There can be 0 outlines
            //System.Diagnostics.Trace.Assert(_outlines.Count > 0);

            string first = string.Empty;
            string last = string.Empty;
            if (_outlines.Count > 0)
            {
                System.Diagnostics.Trace.Assert(_outlines.First().ObjectNumber > 0);
                first = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "/First {0} 0 R", _outlines.First().ObjectNumber);

                System.Diagnostics.Trace.Assert(_outlines.Last().ObjectNumber > 0);
                last = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "/Last {0} 0 R", _outlines.Last().ObjectNumber);
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFOutlines (
{0} 0 obj
    <<
        /Type /Outlines
        {1}
        {2}
        /Count {3}
    >>
endobj
% )
", ObjectNumber, first, last, _outlines.Count
            );
        }
    }
}
