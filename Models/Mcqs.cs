using System.ComponentModel.DataAnnotations;

namespace ElearningMVC.Models
{
    public class Mcqs
    {
        [Key]
        public int Id { get; set; }

        public string? question { get; set; }
        public string? option1 { get; set; }
        public string? option2 { get; set; }
        public string? option3 { get; set; }
        public string? option4 { get; set; }
        public string? Correct { get; set; }

        public int vid { get; set; }
    }
}
