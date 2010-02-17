using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// A PDF font.
    /// </summary>
    /// <see cref="PDFText"/>
    class Font
    {
        public const string Courier = "FC";
        public const string CourierBold = "FCB";
        public const string CourierOblique = "FCI";
        public const string CourierBoldOblique = "FCBI";
        public const string Helvetica = "FH";
        public const string HelveticaBold = "FHB";
        public const string HelveticaOblique = "FHI";
        public const string HelveticaBoldOblique = "FHBI";
        public const string TimesRoman = "FT";
        public const string TimesBold = "FTB";
        public const string TimesItalic = "FTI";
        public const string TimesBoldItalic = "FTBI";
        public const string Symbol = "FS";
        public const string ZapfDingbats = "FZ";

        public Font(string name, double size)
            : this(name, size, PDFWriter.Color.Black)
        {
        }

        public Font(string name, double size, string color)
        {
            Name = name;
            Size = size;
            Color = color;
            CharSpace = 0;
            WordSpace = 0;
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

        /// <summary>
        /// Character spacing: a number expressed in unscaled text space units.
        /// </summary>
        public double CharSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Word spacing: a number expressed in unscaled text space units.
        /// </summary>
        public double WordSpace
        {
            get;
            set;
        }

        /// <summary>
        /// The list of available fonts.
        /// </summary>
        /// 
        /// <remarks>
        /// Key = abreviation, Value = PDF font name
        /// </remarks>
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
