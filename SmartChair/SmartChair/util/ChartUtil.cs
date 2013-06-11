using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartChair.util
{
    class ChartUtil
    {
        public static DateTimeIntervalType SelectDateTimeInterval(TimeSpan? span)
        {
            if (span.Value.TotalMinutes < 60)
            {
                return DateTimeIntervalType.Minutes;
            }
            if (span.Value.TotalHours <= 24)
            {
                return DateTimeIntervalType.Hours;
            }

            if (span.Value.TotalDays < 15)
            {
                return DateTimeIntervalType.Days;
            }
            if (span.Value.TotalDays > 15)
            {
                return DateTimeIntervalType.Months;
            }

            return DateTimeIntervalType.Auto;
        }

        public static string SelectLabelStyle(TimeSpan? span)
        {
            if (span.Value.TotalMinutes < 60)
            {
                return "{0:t}";
            }
            if (span.Value.TotalHours <= 24)
            {
                return "{0:t}";
            }
            if (span.Value.TotalDays < 15)
            {
                return "{0:d}";
            }
            if (span.Value.TotalDays > 15)
            {
                return "{0:MMMM yyyy}";
            }
            return "{0:r}";
        }
    }
}
