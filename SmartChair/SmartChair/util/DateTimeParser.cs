using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.controller
{
    class DateTimeParser
    {
        public static DateTime getDateTimeFromSQLiteString(string dt){
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(dt, "MM.dd.yyyy HH:mm:ss", provider);
        }
    }
}
