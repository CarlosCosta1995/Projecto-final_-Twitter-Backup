using Microsoft.EntityFrameworkCore;
using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{

    public class PostService: IPostService
    {
        /*=================================================
                 * Create An Instance to connect with the DbContext
                 ==================================================*/
        private readonly DataContext context;

        /*===============================
         * PostService Class Constructor
         ================================*/
        public PostService(DataContext context)
        {
            this.context = context;
        }

        /*======================
         * Create A Post
         =======================*/
        public Post CreatePost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
            return post;
        }

        /*============
         * Get A Post
         =============*/
        public Post GetPost(int postid)
        {
            var post = context.Posts.SingleOrDefault(p => p.postId == postid);
            if (post == null)
            {
                throw new NotImplementedException("Não existem posts");
            }
            else
            {
                return post;
            }
        }


        /*============
         * Get A Post by user
         =============*/
        public IEnumerable<Post> GetPostByUser(int userid)
        {
            var post = context.Posts.Where(p => p.user.userId == userid);
            if (post == null)
            {
                throw new NotImplementedException("Não existem posts");
            }
            else
            {
                return post;
            }
        }



        /*=============================
         * Get A Post By the Topic Tag
         ==============================*/
        public Post GetByTag(string postTag)
        {
            throw new NotImplementedException();
        }

        /*=========================
        * Get All Post and List
        ==========================*/
        public IEnumerable<CommentService> GetAllPostComments(int postId)
        {
            throw new NotImplementedException();
        }

        /*=============================================================
         * Get the posts for the feed including it´s user and comments
         ==============================================================*/
        public IEnumerable<Post> GetAllPosts()
        {
            //var posts = context.Posts
            //.Include(b => b.user)
            //.Include(b => b.comments);

            var posts =
                from p in context.Posts
                join u in context.Users
                 on p.user.userId equals u.userId
                //join c in context.Comments
                //on p.comments.commentId equals c.commentId
                select p;
                

            if (posts is null)
            {
                throw new NotImplementedException("Não existem posts neste feed");
            }
            else
            {
                return posts;
            }
        }

        /*========================
         * Edit A Post Properties
         =========================*/
        public Post EditPost(Post post, int postId)
        {
            var postToEdit = context.Posts.Find(postId); //context.Posts.SingleOrDefault(p => p.postId == postId);
            if (postToEdit is null)
            {
                throw new NullReferenceException("Post does not exist");
            }
            else
            {
                postToEdit.topicTag = post.topicTag;
                postToEdit.contentText = post.contentText;

                context.SaveChanges();
                return postToEdit;
            }
            throw new NotImplementedException();
        }

        /*======================
         * Delete A Post
         =======================*/
        public void DeletePost(int postid)
        {

            Post post = context.Posts.SingleOrDefault(p => p.postId == postid);
            if (post is not null)
            {
                context.Posts.Remove(post);
                context.SaveChanges();
            }
            else
            {
                throw new NotImplementedException("Post couln't be found in the DataBase!");
            }
        }

        /*=====================================
         * Add a Like to the corresponding Post 
         ======================================*/
        public void Like(int postId)
        {
            var likePost = context.Posts.SingleOrDefault(p => p.postId == postId);
            if (likePost is not null)
            {
                var makeLike = likePost.numberLike + 1;
                likePost.numberLike = makeLike;
                context.SaveChanges();
                //return likePost.numberLike;
            }
        }

        /*=======================================
         * Add a Retweet to the corresponding Post 
         ========================================*/
        public void Retweet(int postId)
        {
            var followPost = context.Posts.SingleOrDefault(p => p.postId == postId);
            if (followPost is not null)
            {
                var makeFollow = followPost.numberRetweet + 1;
                followPost.numberLike = makeFollow;
                context.SaveChanges();
                //return followPost.numberLike;
            }
        }

        /*=====================================
         * Add a Vote to the corresponding Post 
         ======================================*/
        public void Vote(int postId)
        {
            var votePost = context.Posts.SingleOrDefault(p => p.postId == postId);
            if (votePost is not null)
            {
                var makeFollow = votePost.numberVote + 1;
                votePost.numberVote = makeFollow;
                context.SaveChanges();
                //return votePost.numberVote;
            }
        }
    }
}
