using MedTrakModel;
using Microsoft.Data.SqlClient;

namespace MedTrakDL
{
    public class SqlUserRepository : IRepository<User>
    {
        // =============Dependancy injection================
        private readonly string _connectionString;

        public SqlUserRepository(string p_connectionString)
        {
            this._connectionString = p_connectionString;
        }
        // ================================================
        public void Add(User p_resource)
        {
            String SQLQuery = @"insert into Users
                                values(@userName, @userAddress, @userEmail, @userPassword)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@userName", p_resource.Name);
                command.Parameters.AddWithValue("@userAddress", p_resource.Address);
                command.Parameters.AddWithValue("@userEmail", p_resource.Email);
                command.Parameters.AddWithValue("@userPassword", p_resource.Password);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(User p_resource)
        {
            String SQLQuery = @"DELETE FROM Users WHERE userID = @userID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@Name", p_resource.Name);
                command.Parameters.AddWithValue("@Address", p_resource.Address);
                command.Parameters.AddWithValue("@Email", p_resource.Email);
                command.Parameters.AddWithValue("@Password", p_resource.Password);

                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            String SQLQuery = @"select * from Users";
            List<User> listofUser = new List<User>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofUser.Add(new User()
                    {
                        userID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3),
                        Password = reader.GetString(4),
                        Medicine = GiveMedicineToUser(reader.GetInt32(0))
                    });
                }
                return listofUser;
            }

        }

        private List<Medicine> GiveMedicineToUser(int userID)
        {
            string SQLquery = @"select * from Users u
                                inner join Medicine m on m.userID = u.userID
                                where u.userID = u.userID";

            List<Medicine> ListOfMedicine = new List<Medicine>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLquery, con);

                command.Parameters.AddWithValue("@userID", userID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListOfMedicine.Add(new Medicine(){
                        medID = reader.GetInt32(0),
                        medName = reader.GetString(1),
                        medDose = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3)
                    });
                    
                }
            }
            return ListOfMedicine;
        }

        public void Update(User p_resource)
        {
            string SQLquery = @"update User
                            set userName = @userName, userAddress = @userAddress, userEmail = @userEmail, userPassword = @userPassword
                            where userID = @userID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLquery, con);
                command.Parameters.AddWithValue("@userName", p_resource.Name);
                command.Parameters.AddWithValue("@userAddress", p_resource.Address);
                command.Parameters.AddWithValue("@userEmail", p_resource.Email);
                command.Parameters.AddWithValue("@userPassword", p_resource.Password);
                command.Parameters.AddWithValue("@userID", p_resource.userID);
                

                command.ExecuteNonQuery();
            }
        }
        
    }
}