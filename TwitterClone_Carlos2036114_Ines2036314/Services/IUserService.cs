using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{
    public interface IUserService
    {
        //============ Create A User ========//
        public abstract User CreateUser(User user);

        //============ Get A User by the ID ========//
        public abstract User GetByUserId(int userId);
        //============ Get A User by the Name ========//
        public abstract User GetByUserName(string userName);

        //============ Get A User by the Email ========//
        public User GetByUserNameAndPass(string userName, string password);

        //============ Edit A User Propreties ========//
        public abstract User EditUser(int userId, User user);

        //============ Delete A User ========//
        public abstract void DeleteUser(int userId);

        //============ Edit/Update A User Email ========//
        //public abstract void PatchUserEmail(int userId, string email);

        //============ Edit/Update A User Password ========//
        //public abstract void PatchUserPassword(int userId, string password);

        //============ Add a follower to the corresponding User ========//
        public abstract void Following(int userId);

        //============ Add a following to the corresponding User  ========//
        public abstract void Followers(int userId);
    }
}
