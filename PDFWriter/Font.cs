using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class Font
    {
        public static readonly string Courier = "FC";
        public static readonly string CourierBold = "FCB";
        public static readonly string CourierOblique = "FCI";
        public static readonly string CourierBoldOblique = "FCBI";
        public static readonly string Helvetica = "FH";
        public static readonly string HelveticaBold = "FHB";
        public static readonly string HelveticaOblique = "FHI";
        public static readonly string HelveticaBoldOblique = "FHBI";
        public static readonly string TimesRoman = "FT";
        public static readonly string TimesBold = "FTB";
        public static readonly string TimesItalic = "FTI";
        public static readonly string TimesBoldItalic = "FTBI";
        public static readonly string Symbol = "FS";
        public static readonly string ZapfDingbats = "FZ";

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

        //Key = abreviation
        //Value = PDF font name
        static private Dictionary<string, string> _fonts = new Dictionary<string, string>();

        static private void InitPDFFonts()
        {
            if (_fonts.Count == 0)
            {
                //Lazy initialization
                _fonts.Add(Courier, "Courier");
                _fonts.Add(CourierBold, "Courier-Bold");
                _fonts.Add(CourierOblique, "Courier-Oblique");
                _fonts.Add(CourierBoldOblique, "Courier-BoldOblique");
                _fonts.Add(Helvetica, "Helvetica");
                _fonts.Add(HelveticaBold, "Helvetica-Bold");
                _fonts.Add(HelveticaOblique, "Helvetica-Oblique");
                _fonts.Add(HelveticaBoldOblique, "Helvetica-BoldOblique");
                _fonts.Add(TimesRoman, "Times-Roman");
                _fonts.Add(TimesBold, "Times-Bold");
                _fonts.Add(TimesItalic, "Times-Italic");
                _fonts.Add(TimesBoldItalic, "Times-BoldItalic");
                _fonts.Add(Symbol, "Symbol");
                _fonts.Add(ZapfDingbats, "ZapfDingbats");
            }
        }

        static public Dictionary<string, string> PDFFonts
        {
            get
            {
                InitPDFFonts();
                return _fonts;
            }
        }
    }
}
