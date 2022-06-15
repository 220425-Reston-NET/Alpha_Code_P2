using MedTrakModel;
using Microsoft.Data.SqlClient;

namespace MedTrakDL
{
    public class SqlMedicineRepository : IRepository<Medicine>
    {
        // =============Dependancy injection================
        private readonly string _connectionString;

        public SqlMedicineRepository(string p_connectionString)
        {
            this._connectionString = p_connectionString;
        }
         // ================================================
        public void Add(Medicine p_resource)
        {
            String SQLQuery = @"insert into Medicine
                                values(@medName, @medDose, @Quantity)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@userName", p_resource.medName);
                command.Parameters.AddWithValue("@userAddress", p_resource.medDose);
                command.Parameters.AddWithValue("@userEmail", p_resource.Quantity);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(Medicine p_resource)
        {
            String SQLQuery = @"DELETE FROM Medicine WHERE medID = @medID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

            SqlCommand command = new SqlCommand(SQLQuery, con);

            command.Parameters.AddWithValue("@medName", p_resource.medName);
            command.Parameters.AddWithValue("@medDose", p_resource.medDose);
            command.Parameters.AddWithValue("@Quantity", p_resource.Quantity);

            command.ExecuteNonQuery();
            }
        }

        public List<Medicine> GetAll()
        {
            String SQLQuery = @"select * from Medicine";
            List<Medicine> listofMedicine = new List<Medicine>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofMedicine.Add(new Medicine()
                    {
                        medID = reader.GetInt32(0),
                        medName = reader.GetString(1),
                        medDose = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3)
                    });
                }
                return listofMedicine;
            }
        }

        public void Update(Medicine p_resource)
        {
            string SQLquery = @"update Medicine
                            set medName = @medName, medDose = @medDose, Quantity = @Quantity
                            where medID = @medID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLquery, con);

                command.Parameters.AddWithValue("@medName", p_resource.medName);
                command.Parameters.AddWithValue("@medDose", p_resource.medDose);
                command.Parameters.AddWithValue("@Quantity", p_resource.Quantity);

                command.ExecuteNonQuery();
            }
        }
    }
}