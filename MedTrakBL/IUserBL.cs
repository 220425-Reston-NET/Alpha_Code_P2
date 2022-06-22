using MedTrakModel;

namespace MedTrakBL
{
    public interface IUserBL
    {
        void AddUser(User u_use);
        public List<Medicine> ViewAllMedicine(int p_userID);

        User SearchUserByName(string p_userName);
        User SearchUserByEmailAndPassword(string Email, string Password);
    }
}

