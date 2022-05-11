using ProjectCRUD.Library.Entities;
using ProjectCRUD.Library.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCRUD.Library.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private readonly string ConnectionString = "Data Source=.\\sqlexpress01;Initial Catalog=DBProjectCRUD;Integrated Security=True";

        public async Task<int> Delete(InformationEntity entity)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql_insert = "DELETE FROM dbo.informations WHERE InformationID = @InformationID";
                using (SqlCommand comm = new SqlCommand(sql_insert, conn))
                {
                    comm.Parameters.AddWithValue("@InformationID", entity.InformationID);
                    var result = await comm.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }

        public async Task<IEnumerable<InformationEntity>> GetInformations()
        {
            List<InformationEntity> entities = new List<InformationEntity>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql_get = "SELECT * FROM dbo.informations ORDER BY Lastname,Firstname ASC";
                using (SqlCommand comm = new SqlCommand(sql_get, conn))
                {
                    using (SqlDataReader reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            entities.Add(new InformationEntity()
                            {
                                InformationID = Convert.ToInt32(reader["InformationID"]),
                                Firstname = Convert.ToString(reader["Firstname"]),
                                Lastname = Convert.ToString(reader["Lastname"]),
                                Gender = Convert.ToString(reader["Gender"])
                            });
                        }
                    }
                }
            }
            return entities;
        }

        public async Task<int> Save(InformationEntity entity)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql_insert = "INSERT INTO dbo.informations (Firstname,Lastname,Gender) VALUES (@Firstname,@Lastname,@Gender)";
                using (SqlCommand comm = new SqlCommand(sql_insert, conn))
                {
                    comm.Parameters.AddWithValue("@Firstname", entity.Firstname);
                    comm.Parameters.AddWithValue("@Lastname", entity.Lastname);
                    comm.Parameters.AddWithValue("@Gender", entity.Gender);
                    var result = await comm.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }

        public async Task<int> Update(InformationEntity entity)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql_update = "UPDATE dbo.informations SET Firstname = @Firstname, Lastname = @Lastname, Gender = @Gender WHERE InformationID = @InformationID";
                using (SqlCommand comm = new SqlCommand(sql_update, conn))
                {
                    comm.Parameters.AddWithValue("@InformationID", entity.InformationID);
                    comm.Parameters.AddWithValue("@Firstname", entity.Firstname);
                    comm.Parameters.AddWithValue("@Lastname", entity.Lastname);
                    comm.Parameters.AddWithValue("@Gender", entity.Gender);
                    var result = await comm.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }
    }
}
