using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroGestion.Models
{
	public class SearchDTO
	{
        public string? Name { get; set; }
        public int SiteId { get; set; }
        public int ServiceId { get; set; }
    }
}

