using System.ComponentModel.DataAnnotations;

namespace AgroGestion.Models
{
	public class SiteDTO
	{
        public int? Id { get; set; }

		[Required]
		public string Ville { get; set; } = "";
	}
}

