using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class ObjectReference
    {
        public enum ObjectType
        {
            Info,
            Catalog,
            Outlines,
            Pages,
            Page,
            ContentStream,
            ProcedureSetArray,
            Font
        };

        public ObjectReference(int byteOffset, ObjectType type)
        {
            ByteOffset = byteOffset;
            Type = type;
        }

        public int ByteOffset
        {
            get;
            set;
        }

        public ObjectType Type
        {
            get;
            set;
        }
    }

    class CrossReferenceTable
    {
        private List<ObjectReference> _xref = new List<ObjectReference>();

        public void AddByteOffset(ObjectReference reference)
        {
            _xref.Add(reference);
        }

        public int Count
        {
            get { return _xref.Count; }
        }

        private int _xrefByteOffset;

        public string GetXRefPDFString(int byteOffset)
        {
            // The cross-reference table contains information that permits
            // random access to indirect objects within the file
            // nnnnnnnnnn: 10-digit object number of the next free object
            // ggggg: 5-digit generation number
            // f: free entry
            // The maximum generation number is 65,535; when a cross-reference
            // entry reaches this value, it will never be reused.
            // Byte offset
            // n: in-use entry

            _xrefByteOffset = byteOffset;

            string xref = string.Empty;
            foreach (ObjectReference objectRef in _xref)
            {
                xref += string.Format("{0:0000000000} 00000 n\n", objectRef.ByteOffset);
            }

            return string.Format(@"
xref
0 {0}
0000000000 65535 f
{1}", _xref.Count, xref
            );
        }

        public string GetTrailerPDFString()
        {
            return string.Format(@"
trailer
    <<
        % Total number of entries in the file's cross-reference table[...] this
        % value is 1 greater than the highest object number used in the file.
        /Size {0}

        % The catalog object for the PDF document
        % contained in the file
        /Root 2 0 R

        /Info 1 0 R
    >>", _xref.Count + 1
);
        }

        public string GetStartXRefPDFString()
        {
            return string.Format(@"
% The byte offset from the beginning of the file to the beginning
% of the xref keyword in the last cross-reference section
startxref
{0}", _xrefByteOffset
            );
        }

    }
}
