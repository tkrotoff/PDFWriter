using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class Font
    {
        public static readonly string Helvetica = "FH";
        public static readonly string HelveticaBold = "FHB";

        public Font(string name, double size)
            : this(name, size, PDFWriter.Color.Black)
        {
        }

        public Font(string name, double size, string color)
        {
            Name = name;
            Size = size;
            Color = color;
        }

        /// <summary>
        /// Font size.
        /// </summary>
        public double Size
        {
            get;
            set;
        }

        /// <summary>
        /// Font name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Font color.
        /// </summary>
        public string Color
        {
            get;
            set;
        }

        public double CharSpace
        {
            get;
            set;
        }

        public double WordSpace
        {
            get;
            set;
        }
    }
}
