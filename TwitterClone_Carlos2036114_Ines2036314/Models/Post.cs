using System.ComponentModel.DataAnnotations;

namespace TwitterClone_Carlos2036114_Ines2036314.Models
{
    public class Post
    {
        public int postId { get; set; }
        public User user { get; set; }
        public string topicTag { get; set; }
        public string contentText { get; set; }

        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }
        public Comment comments { get; set; }
        public int numberLike { get; set; }
        public int numberVote { get; set; }
        public int numberRetweet { get; set; }
    }
}
