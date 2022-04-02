using Microsoft.EntityFrameworkCore;

namespace TwitterClone_Carlos2036114_Ines2036314.Models
{
    public class DataContext: DbContext
    {
        //public DbSet<Book> Books { get; set; }
        //public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=twitter;" +
                "user=root;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /* User:
                public int userId { get; set; }
                public string profileImage { get; set; }
                public string userTag { get; set; }
                public string userName { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public DateTime creationDate { get; set; }
             */
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.userId);
                entity.Property(e => e.profileImage);
                entity.Property(e => e.userTag).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.creationDate).IsRequired();
                entity.Property(e => e.numberOfFollower);
                entity.Property(e => e.numberOfFollowing);
            });

            /* Post:
                public int postId { get; set; }
                public User user { get; set; }
                public string topicTag { get; set; }
                public string contentText { get; set; }
                public DateTime creationDate { get; set; }
                public List<Comment> comments { get; set; }
                public Button button { get; set; }
             */
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.postId);
                entity.HasOne(e => e.user);
                entity.Property(e => e.topicTag);
                entity.Property(e => e.contentText).IsRequired();
                entity.Property(e => e.creationDate).IsRequired();
                entity.HasOne(e => e.comments); 
                entity.Property(e => e.numberLike);
                entity.Property(e => e.numberRetweet);
                entity.Property(e => e.numberVote);
            });

            /* Comment:
                public int commentId { get; set; }
                public User user { get; set; }
                public DateTime creationDate { get; set; }
                public string contentText { get; set; }
                public Button button { get; set; }
             */
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.commentId);
                entity.HasOne(e => e.user);
                entity.Property(e => e.contentText).IsRequired();
                entity.Property(e => e.creationDate).IsRequired();
                entity.Property(e => e.numberLike);
                entity.Property(e => e.numberRetweet);
                entity.Property(e => e.numberVote);
            });
        }
    }
}
