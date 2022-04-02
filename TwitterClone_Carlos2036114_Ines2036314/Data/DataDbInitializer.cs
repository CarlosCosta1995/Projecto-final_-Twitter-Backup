using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Data
{
    public static class DataDbInitializer
    {
        public static void InsertData(DataContext context)
        {
            //====== Adds a User ======//
            var userProfile = new User
            {
                userTag = "admin",
                userName = "admin",
                profileImage = "default.jpg",
                Email = "admin@root.pt",
                Password = "123",
                creationDate = DateTime.Now,
                numberOfFollower = 1,
                numberOfFollowing = 1,
            };
            context.Users.Add(userProfile);

            //====== Adds a Comment ======//
            var comment = new Comment
            {
                user = userProfile,
                creationDate = DateTime.Now,
                contentText = "Bla Bla Bla",
                numberLike = 1,
                numberVote = 1,
                numberRetweet = 1,
            };
            context.Comments.Add(comment);

            //====== Adds a Post ======//
            context.Posts.Add(new Post
            {
                user = userProfile,
                topicTag = "Assunto Novo",
                contentText = "Bla Bla Bla",
                creationDate = DateTime.Now,
                comments = comment,
                numberLike = 1,
                numberVote = 1,
                numberRetweet = 1,
            });

            // Save changes
            context.SaveChanges();
        }
    }
}
