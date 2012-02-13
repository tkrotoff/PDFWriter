using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDF
{
    /// <summary>
    /// A page layout contains information about margins, width, height... of a PDF page.
    /// </summary>
    public class PageLayout
    {
        /// <summary>
        /// Creates a default page layout.
        /// </summary>
        public PageLayout()
        {
            Width = 612;
            Height = 792;

            LeftMargin = 25;
            RightMargin = LeftMargin;

            TopMargin = 55;
            BottomMargin = TopMargin;

            HeaderYPos = 766;
            HeaderLeftXPos = 20;
            HeaderRightXLimit = Width - HeaderLeftXPos;

            FooterYPos = 15;
            FooterLeftXPos = HeaderLeftXPos;
            FooterRightXLimit = HeaderRightXLimit;

            LeftHeader = "Report";
            RightHeader = DateTime.Now.ToShortDateString();
            LeftFooter = "Source: PDFWR (www.pdfwr.com)";
        }

        /// <summary>
        /// Width of a PDF page.
        /// </summary>
        internal double Width
        {
            get;
            set;
        }

        /// <summary>
        /// Height of a PDF page.
        /// </summary>
        internal double Height
        {
            get;
            set;
        }

        #region Margins

        /// <summary>
        /// Left margin inside a PDF page, default is 25.
        /// </summary>
        internal double LeftMargin
        {
            get;
            set;
        }

        /// <summary>
        /// Right margin inside a PDF page, default is 25.
        /// </summary>
        internal double RightMargin
        {
            get;
            set;
        }

        /// <summary>
        /// Top margin inside a PDF page, default is 55.
        /// </summary>
        internal double TopMargin
        {
            get;
            set;
        }

        /// <summary>
        /// Bottom margin inside a PDF page, default is 55.
        /// </summary>
        internal double BottomMargin
        {
            get;
            set;
        }
        #endregion

        #region Header
        internal double HeaderYPos
        {
            get;
            set;
        }

        internal double HeaderLeftXPos
        {
            get;
            set;
        }

        internal double HeaderRightXLimit
        {
            get;
            set;
        }

        internal double GetHeaderRightXPos(string text, Font font)
        {
            double textWidth = FontMetrics.GetTextWidth(text, font);
            return HeaderRightXLimit - textWidth;
        }

        /// <summary>
        /// String used as the page header, left part.
        /// </summary>
        public string LeftHeader
        {
            get;
            set;
        }

        /// <summary>
        /// String used as the page header, right part.
        /// </summary>
        public string RightHeader
        {
            get;
            set;
        }
        #endregion

        #region Footer
        internal double FooterYPos
        {
            get;
            set;
        }

        internal double FooterLeftXPos
        {
            get;
            set;
        }

        internal double FooterRightXLimit
        {
            get;
            set;
        }

        internal double GetFooterRightXPos(string text, Font font)
        {
            double textWidth = FontMetrics.GetTextWidth(text, font);
            return FooterRightXLimit - textWidth;
        }

        /// <summary>
        /// String used as the page footer, left part.
        /// </summary>
        public string LeftFooter
        {
            get;
            set;
        }

        /*TODO not used
        public string RightFooter
        {
            get;
            set;
        }*/
        #endregion
    }
}
