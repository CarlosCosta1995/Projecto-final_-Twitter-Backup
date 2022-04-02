using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{
    public interface ICommentService
    {
        //================ Make a Comment ================//
        public abstract Comment CreateComment(Comment comment);

        //================ Edit a Comment ================//
        public abstract Comment EditComment(int commentId, Comment comment);

        //================ Delete a Comment ================//
        public abstract void DeleteComment(int id);

        //================ Give a Like ================//
        public abstract void Like(int commentId);

        //================ Give a follow ================//
        public abstract void Retweet(int commentId);

        //================ Vote for a Comment ================//
        public abstract void Vote(int commentId);


    }
}
