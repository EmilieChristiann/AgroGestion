using System;
using System.ComponentModel.DataAnnotations;

namespace AgroGestion.Models
{
	public class Service
	{
        [Key] public int Id { get; set; }
        [StringLength(64)] public string Type { get; set; } = "";
    }
}

