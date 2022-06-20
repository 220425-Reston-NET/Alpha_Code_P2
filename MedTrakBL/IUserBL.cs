using MedTrakModel;

namespace MedTrakBL
{
    public interface IUserBL
    {
        void AddUser(User u_use);
        public List<Medicine> ViewAllMedicine(int p_userID);
        User SearchUserByEmail(string email);
    }
}

