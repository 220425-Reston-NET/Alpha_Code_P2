using MedTrakModel;
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
            String SQLQuery = @"insert into Patient
                                values(@pName, @pAddress, @pEmailAddress)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@pName", p_resource.Name);
                command.Parameters.AddWithValue("@pAddress", p_resource.Address);
                command.Parameters.AddWithValue("@pPhone", p_resource.Phone);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(User p_resource)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            String SQLQuery = @"select * from Patient";
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
                        Email = reader.GetString(3)
                    });
                }
                return listofUser;
            }


        }

        public async Task<List<User>> GetAllAsync()
        {
            String SQLQuery = @"select * from Patient";
            List<User> listofUser = new List<User>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ExecuteReaderAsync())
                {
                    listofUser.Add(new User()
                    {
                        userID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3)
                    });
                }
                return listofUser;
            }
        }

        public void Update(User p_resource)
        {
            throw new NotImplementedException();
        }
    }
}