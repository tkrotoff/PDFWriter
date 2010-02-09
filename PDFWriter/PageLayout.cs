using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PageLayout
    {
        public PageLayout()
        {
            Width = 612;
            Height = 792;

            LeftMargin = 25;
            RightMargin = RightMargin;

            TopMargin = 55;
            BottomMargin = TopMargin;
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
    }
}
