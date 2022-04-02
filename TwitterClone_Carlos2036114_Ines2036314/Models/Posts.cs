namespace TwitterClone_Carlos2036114_Ines2036314.Models
{
    public class Posts_ :Post
    {
        public IEnumerable<Post> PostsList { get; set; }

        public Posts_()
        {
        }
        //public Posts_ (IEnumerable<Post> posts)
        //{
        //    this.PostsList = posts; 
        //}
    }
}
