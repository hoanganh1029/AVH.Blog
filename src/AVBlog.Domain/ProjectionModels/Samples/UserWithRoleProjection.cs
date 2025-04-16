namespace AVBlog.Domain.ProjectionModels.Samples
{
    public class UserWithRoleProjection
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool AllowDownload { get; set; }
        public string Role { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
