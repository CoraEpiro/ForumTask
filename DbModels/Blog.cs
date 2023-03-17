using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumTask.DbModels
{
    public class StringListToStringConverter : ValueConverter<List<string>, string>
    {
        public StringListToStringConverter() : base(
            v => string.Join(",=,", v),
            v => v.Split(",=,", StringSplitOptions.RemoveEmptyEntries).ToList())
        { }
    }
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CoreImage { get; set; }
        public string? ShortContent { get; set; }
        public string? ContentOwner { get; set; }
        public string? LongContent { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Category { get; set; }
        public List<string>? Tags { get; set; }
    }
}