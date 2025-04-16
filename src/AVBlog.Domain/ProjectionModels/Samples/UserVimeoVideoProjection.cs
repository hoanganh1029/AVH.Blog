namespace AVBlog.Domain.ProjectionModels.Samples
{
    public class UserVimeoVideoProjection
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid VimeoVideoId { get; set; }
        public int VimeoId { get; set; }
        public string VideoTitle { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
