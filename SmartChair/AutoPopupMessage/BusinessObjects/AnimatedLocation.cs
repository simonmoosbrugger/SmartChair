using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPopupMessage.BusinessObjects
{
    public class AnimatedLocation
    {
        private readonly double fromLeft;

        public double FromLeft
        {
            get { return fromLeft; }
        }

        private readonly double toLeft;

        public double ToLeft
        {
            get { return toLeft; }
        }

        private readonly double fromTop;

        public double FromTop
        {
            get { return fromTop; }
        }

        private readonly double toTop;

        public double ToTop
        {
            get { return toTop; }
        }

        public AnimatedLocation(double fromLeft, double toLeft, double fromTop, double toTop)
        {
            this.fromLeft = fromLeft;
            this.toLeft = toLeft;
            this.fromTop = fromTop;
            this.toTop = toTop;
        }
    }
}
