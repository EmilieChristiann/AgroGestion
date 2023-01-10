using System;
using System.ComponentModel.DataAnnotations;

namespace AgroGestion.Models
{
	public class ServiceDTO
	{
        public int? Id { get; set; }

        [Required]
        public string Type { get; set; } = "";
    }
}

