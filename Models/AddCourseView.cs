namespace ElearningMVC.Models
{
    public class AddCourseView
    {
        public int Id { get; set; }

        public string? Cname { get; set; }

        public string? Subname { get; set; }

        public decimal? Price { get; set; }

        public IFormFile? Banner { get; set; }
    }
}
