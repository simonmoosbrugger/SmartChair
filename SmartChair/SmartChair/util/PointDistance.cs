using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.util
{
    class PointDistance
    {
        public static double GetDistanceBetweenPoints(double x, double y)
        {
            return Math.Sqrt(Math.Pow((0 - x), 2d) + Math.Pow((0 - y), 2d));
        }
    }
}
