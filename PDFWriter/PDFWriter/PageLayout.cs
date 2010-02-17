using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// A page layout contains information about margins, width, height... of a PDF page.
    /// </summary>
    class PageLayout
    {
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
        }

        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        #region Margins
        public double LeftMargin
        {
            get;
            set;
        }

        public double RightMargin
        {
            get;
            set;
        }

        public double TopMargin
        {
            get;
            set;
        }

        public double BottomMargin
        {
            get;
            set;
        }
        #endregion

        #region Header
        public double HeaderYPos
        {
            get;
            set;
        }

        public double HeaderLeftXPos
        {
            get;
            set;
        }

        public double HeaderRightXLimit
        {
            get;
            set;
        }

        public double GetHeaderRightXPos(string text, Font font)
        {
            double textWidth = FontMetrics.GetTextWidth(text, font);
            return HeaderRightXLimit - textWidth;
        }
        #endregion

        #region Footer
        public double FooterYPos
        {
            get;
            set;
        }

        public double FooterLeftXPos
        {
            get;
            set;
        }

        public double FooterRightXLimit
        {
            get;
            set;
        }

        public double GetFooterRightXPos(string text, Font font)
        {
            double textWidth = FontMetrics.GetTextWidth(text, font);
            return FooterRightXLimit - textWidth;
        }
        #endregion
    }
}
