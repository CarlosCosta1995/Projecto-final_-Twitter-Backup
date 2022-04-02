using System.ComponentModel.DataAnnotations;

namespace TwitterClone_Carlos2036114_Ines2036314.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public User user { get; set; }

        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }
        public string contentText { get; set; }
        public int numberLike { get; set; }
        public int numberRetweet { get; set; }
        public int numberVote { get; set; }
    }
}
