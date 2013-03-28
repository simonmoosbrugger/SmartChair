using System;
using System.Globalization;

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
