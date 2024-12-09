using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDims.Models
{
    public class ModMasterBL: DbConnect
    {
        public ModMasterBL()
        {
        }
        public IEnumerable<ModMaster> GetAllMainModule()
        {
            var modMastList = new List<ModMaster>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from tbl_ModMaster order by ModId desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var modMast = new ModMaster();
                        modMast.ModId = (int)reader[0];
                        modMast.ModName = reader[1].ToString();
                        modMast.DisplayName = reader[2].ToString();
                        modMast.IsActive = (bool)reader[3];
                        modMast.CompanyCode = reader[4].ToString();                        
                        modMast.OrderNo = (int)reader[5];
                        modMastList.Add(modMast);
                    }
                }
            }
            return modMastList;
        }
    }
}
