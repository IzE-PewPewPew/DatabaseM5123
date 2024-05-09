using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseM5
{
    public class M5
    {
        public SqlConnection Connection;
        public string str = "Server=KUONG;Database=M52024;Integrated Security=True;";// = "Data Source=.; Initial Catalog=M52024; Intergrated Security=true;";

        public void Connect()
        {
            SqlDependency.Stop(str); 
            SqlDependency.Start(str);

            Connection = new SqlConnection(str);
            Connection.Open();
        }
    }
}
