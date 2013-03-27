using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{
    class Program
    {
        static string insert = "INSERT INTO Weight (\"Timestamp\", \"WeightKg\", \"PersonRef\") VALUES ({0}, {1}, {2});";
        static int maxweight = 89, minweight = 80;
        static StringBuilder sql;

        static void Main(string[] args)
        {
            sql = new StringBuilder();
            DateTime dt2 = DateTime.Now.AddMonths(3);
            DateTime dt1 = DateTime.Now.AddMonths(-3);

            while (dt1 < dt2)
            {
                Console.WriteLine(dt1.ToString("dd.MM.yyyy"));
                Random rnd = new Random();

                if (dt1.DayOfWeek != DayOfWeek.Sunday && dt1.DayOfWeek != DayOfWeek.Saturday)
                {
                    
                    int random = rnd.Next(0, 2);
                    int hour = 8;
                    for (int i = 0; i <= random; i++)
                    {
                        string h;
                        if ((hour + (5 * i)) < 10)
                        {
                            h = "0" + (hour + (5 * i)).ToString();
                        }
                        else
                        {
                            h = (hour + (5 * i)).ToString();
                        }
                        sql.Append("INSERT INTO Weight (\"Timestamp\", \"WeightKg\", \"PersonRef\") VALUES (\"");
                        sql.Append(dt1.ToString("MM.dd.yyyy") + " " + h + ":00:00");
                        sql.Append("\", ");
                        rnd = new Random();
                        int weight = rnd.Next(minweight, maxweight);
                        rnd = new Random();
                        int comma = rnd.Next(1, 9);
                        sql.Append(weight.ToString()+"."+comma);
                        sql.Append(", ");
                        sql.Append("1");
                        sql.Append(");");
                        sql.Append("\n");
                    }
                }
                dt1 = dt1.AddDays(1);
            }

            File.WriteAllText("test.sql", sql.ToString());
        }
    }
}
