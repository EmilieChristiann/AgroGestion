using System;
using System.ComponentModel.DataAnnotations;

namespace AgroGestion.Models
{
	public class Site
	{
        [Key] public int Id { get; set; }
        [StringLength(64)] public string Ville { get; set; } = "";
	}
}

