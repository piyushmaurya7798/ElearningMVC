using System.ComponentModel.DataAnnotations;

namespace ElearningMVC.Models
{
	public class Certificate
	{
		[Key]
		public int CertificateId { get; set; }

		public string suser { get; set; }

		public string CourseName { get; set; }

		public DateTime IssuseDate { get; set; }
	}
}
