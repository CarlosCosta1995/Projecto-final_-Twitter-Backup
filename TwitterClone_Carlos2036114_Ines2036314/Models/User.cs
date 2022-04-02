using System.ComponentModel.DataAnnotations;

namespace TwitterClone_Carlos2036114_Ines2036314.Models
{
    public class User
    {
        public int userId { get; set; }
        public string userTag { get; set; }
        public string userName { get; set; }
        public string profileImage { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }
        public int numberOfFollower { get; set; }
        public int numberOfFollowing { get; set; }
    }
}
