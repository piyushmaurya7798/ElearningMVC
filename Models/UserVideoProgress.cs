using System.ComponentModel.DataAnnotations;

namespace ElearningMVC.Models
{
	public class UserVideoProgress
	{
		[Key]
		public int ProgressId { get; set; }
			public string Suser { get; set; }

			public int videoId { get; set; }

		public bool IsWatched { get; set; }

		public DateTime WatcheDate { get; set; }
	}
}
