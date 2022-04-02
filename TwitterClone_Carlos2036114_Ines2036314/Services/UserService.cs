using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{
    public class UserService: IUserService
    {
        /*=================================================
          * Create An Instance to connect with the DbContext
          ==================================================*/
        private readonly DataContext context;

        /*===============================
         * UserService Class Constructor
         ================================*/
        public UserService(DataContext context)
        {
            this.context = context;
        }

        /*======================
         * Create A User
         =======================*/
        public User CreateUser(User user)
        {
            //Create a user on the DB
            //Returns the same User to be place in the View
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        /*======================
         * Get A User by the ID
         =======================*/
        public User GetByUserId(int userId)//
        {
            var userById = context.Users.SingleOrDefault(user => user.userId == userId);
            if (userById is not null)
            {
                return userById;
            }
            else
            {
                return null;
            }
        }

        public User GetByUserName(string userName)
        {
            var userByName = context.Users.SingleOrDefault(user => user.userName == userName);
            if (userByName is not null)
            {
                return userByName;
            }
            else
            {
                return null;
            }
        }
        //public User GetByUserByEmail(string userEmail)//
        //{
        //    var userByEmail = context.Users.SingleOrDefault(user => user.Email == userEmail);
        //    return userByEmail;
        //}
        public User GetByUserNameAndPass(string userName, string password)//
        {
            var userByName = context.Users.SingleOrDefault(user => user.userName == userName);
            var userByPass = context.Users.SingleOrDefault(user => user.Password == password);
            if (userByName is not null && userByPass is not null)
            {
                return userByName;
            }
            else
            {
                return null;
            }
            
        }



        /*=======================
         * Edit A User Propreties
         ========================*/
        public User EditUser(int userId, User user)
        {
            var userToVerify = context.Users.SingleOrDefault(user => user.userId == userId);
            if (userToVerify is null)
            {
                throw new NotImplementedException("This user couldn't be found!");
            }
            else
            {
                userToVerify.userName = user.userName;
                //userToVerify.userTag = user.userTag;
                userToVerify.profileImage = user.profileImage;
                context.SaveChanges();
                return userToVerify;
            }
        }

        /*======================
         * Delete A User 
         =======================*/
        public void DeleteUser(int userId)
        {
            User user = context.Users.Find(userId);
            if (user is not null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            else
            {
                throw new NotImplementedException("User couln't be found in the DataBase!");
            }
        }

        /*=========================
         * Edit/Update A User Email
         ==========================*/
        //public void PatchUserEmail(int userId, string email)
        //{
        //    var emailToUpdate = context.Users.SingleOrDefault(user => user.userId == userId);
        //    if (emailToUpdate is null)
        //    {
        //        throw new NotImplementedException("A user couldnt be found in de Data1");
        //    }
        //    else
        //    {
        //        emailToUpdate.Email = email;
        //        context.SaveChanges();
        //    }
        //}

        /*============================
         * Edit/Update A User Password
         =============================*/
        //public void PatchUserPassword(int userId, string password)
        //{
        //    var passwordToUpdate = context.Users.SingleOrDefault(user => user.userId == userId);
        //    if (passwordToUpdate is null)
        //    {
        //        throw new NotImplementedException("A user couldnt be found in de Data1");
        //    }
        //    else
        //    {
        //        passwordToUpdate.Password = password;
        //        context.SaveChanges();
        //    }
        //}

        /*=========================================
         * Add a follower to the corresponding User 
         ==========================================*/
        public void Followers(int userId)
        {
            var userById = context.Users.SingleOrDefault(user => user.userId == userId);
            if (userById is not null)
            {
                var addFollowers = userById.numberOfFollower + 1;
                userById.numberOfFollower = addFollowers;
                context.SaveChanges();
                //return userById.addFollowers;
            }
        }

        /*=========================================
         * Add a following to the corresponding User 
         ==========================================*/
        public void Following(int userId)
        {
            var userById = context.Users.SingleOrDefault(user => user.userId == userId);
            if (userById is not null)
            {
                var addFollowing = userById.numberOfFollowing + 1;
                userById.numberOfFollowing = addFollowing;
                context.SaveChanges();
                //return userById.numberOfFollowing;
            }
        }
    }
}
