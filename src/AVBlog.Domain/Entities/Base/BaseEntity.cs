namespace AVBlog.Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public string? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
