using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.db
{
    interface DbController
    {
        DataTable Select(string Query);
        Object SelectScalar(string Query);
        void Insert(String Table, List<string> ColumnNames, List<string> Values);
        void Update();
        void Delete();
        void Close();
    }
}
