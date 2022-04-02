using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{
    public class CommentService: ICommentService
    {
        /*=================================================
         * Create An Instance to connect with the DbContext
         ==================================================*/
        private readonly DataContext context;

        /*===============================
         * CommentService Class Constructor
         ================================*/

        public CommentService(DataContext context)
        {
            this.context = context;
        }

        /*======================
         * Make a comment
         =======================*/
        public Comment CreateComment(Comment comment)
        {
            //Create a comment on the DB
            context.Comments.Add(comment);
            context.SaveChanges();
            return comment;
        }

        /*======================
         * Delete a comment
         =======================*/
        public void DeleteComment(int _commentId)
        {
            Comment comment = context.Comments.SingleOrDefault(c => c.commentId == _commentId);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
            else
            {
                throw new NotImplementedException("Comment couln't be found in the DataBase!");
            }

        }

        /*======================
         * Edit a comment
         =======================*/
        public Comment EditComment(int commentId, Comment comment)
        {
            var editComment = context.Comments.SingleOrDefault(c => c.commentId == commentId);
            if (editComment is null)
            {
                throw new NotImplementedException("Comment couln't be found in the DataBase!");
            }
            else
            {
                editComment.contentText = comment.contentText;
                context.SaveChanges();
                return editComment;
            }
        }


        /*======================
         * Retweet a comment
         =======================*/
        public void Retweet(int commentId)
        {
            var commentRetweet = context.Comments.SingleOrDefault(c => c.commentId == commentId);
            if (commentRetweet is not null)
            {
                var addFollowing = commentRetweet.numberRetweet + 1;
                commentRetweet.numberRetweet = addFollowing;
                context.SaveChanges();
                //return commentRetweet.numberRetweet;
            }
        }

        /*======================
         * Like a comment
         =======================*/
        public void Like(int commentId)
        {
            var commentLike = context.Comments.SingleOrDefault(c => c.commentId == commentId);
            if (commentLike is not null)
            {
                var addFollowing = commentLike.numberLike + 1;
                commentLike.numberLike = addFollowing;
                context.SaveChanges();
                //return commentLike.numberLike;
            }
        }

        /*======================
         * Vote a comment
         =======================*/
        public void Vote(int commentId)
        {
            var commentVote = context.Comments.SingleOrDefault(c => c.commentId == commentId);
            if (commentVote is not null)
            {
                var addFollowing = commentVote.numberVote + 1;
                commentVote.numberVote = addFollowing;
                context.SaveChanges();
                //return commentVote.numberVote;
            }
        }

    }
}
