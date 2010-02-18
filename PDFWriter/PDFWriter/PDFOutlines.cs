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
    /// </remarks>
    class PDFOutlines : PDFStructureObject
    {
        public override string ToInnerPDF()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, @"
% PDFOutlines (
{0} 0 obj
    <<
        /Type /Outlines
        /Count 0
    >>
endobj
% )
", ObjectNumber
            );
        }

    }
}
