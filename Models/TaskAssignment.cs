using System.ComponentModel.DataAnnotations;

namespace ElearningMVC.Models
{
    public class TaskAssignment
    {
        [Key]
        public int Id { get; set; }

        public int VideoId { get; set; }

        public string? taskUpload { get; set; }

        public DateTime? Finishdate { get; set; }

        public string? Score { get; set; } 

    }
}
