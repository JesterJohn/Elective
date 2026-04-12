using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
        internal class db_connection
        {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;
                  Initial Catalog=InventoryDB;
                  Integrated Security=True");
        }
    }
    }

