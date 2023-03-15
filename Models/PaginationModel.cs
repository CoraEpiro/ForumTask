namespace ForumTask.Models
{
    public class PaginationModel<TModel>
    {
        public IEnumerable<TModel> blogs { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public string? UserRole { get; set; }

        public PaginationModel(IEnumerable<TModel> blogs, int page, int pageSize, int itemsCount)
        {
            this.blogs = blogs;
            Page = page;
            PageSize = pageSize;
            TotalPages = Convert.ToInt32(Math.Ceiling(1.0 * itemsCount / pageSize));
        }

    }
}
