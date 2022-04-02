using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{
    public interface IPostService
    {
        //============ Create A Post ========//
        public abstract Post CreatePost(Post post);

        //============ Get A Post ========//
        public abstract Post GetPost(int postid);

        //============ Get A Post By the Topic Tag ========//
        public abstract Post GetByTag(string postTag);

        //============ Get A Post By the User ========//
        public abstract IEnumerable<Post> GetPostByUser(int userid);

        //============ Get All Post and List ========//
        public abstract IEnumerable<Post> GetAllPosts();

        //============ Get the posts for the feed including it´s user and comments ========//
        public abstract IEnumerable<CommentService> GetAllPostComments(int postId);

        //============ Edit A Post Properties ========//
        public abstract Post EditPost(Post post, int postId);

        //============ Delete A Post ========//
        public abstract void DeletePost(int postid);

        //============ Add a Like to the corresponding Post ========//
        public abstract void Like(int postId);

        //============ Add a Retweet to the corresponding Post ========//
        public abstract void Retweet(int postId);

        //============ Add a Vote to the corresponding Post ========//
        public abstract void Vote(int postId);
    }
}
