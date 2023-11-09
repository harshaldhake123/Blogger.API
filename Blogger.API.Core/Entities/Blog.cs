namespace Blogger.Entities
{
    public class Blog
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime LastUpdatedDateTimeUtc { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<UserBlogMapping> UserBlogMappings { get; set; }
    }
}