using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.db
{
    class DbUtil
    {
        public static List<string> GetColumnNames(string table)
        {
            List<string> columnnames = new List<string>();

            switch (table)
            {
                case "Weight":
                    columnnames.Add("Timestamp");
                    columnnames.Add("WeightKg");
                    columnnames.Add("PersonRef");
                    break;
                case "CenterOfGravityData":
                    columnnames.Add("Timestamp");
                    columnnames.Add("X");
                    columnnames.Add("Y");
                    columnnames.Add("PersonRef");
                    break;
                case "Person":
                    columnnames.Add("Fname");
                    columnnames.Add("Lname");
                    break;
                case "SensorData":
                    columnnames.Add("Timestamp");
                    columnnames.Add("BottomLeft");
                    columnnames.Add("BottomRight");
                    columnnames.Add("TopLeft");
                    columnnames.Add("TopRight");
                    columnnames.Add("PersonRef");
                    break;
                case "Log":
                    columnnames.Add("Ref_Person");
                    columnnames.Add("Message");
                    break;
                default:
                    break;
            }
            return columnnames;
        }
    }
}
