using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroGestion.Models
{
	public class EmployeeDTO
	{
        public int? Id { get; set; }

        [Required] public string Name { get; set; } = "";
        [Required] public string Surname { get; set; } = "";
        [Required] public string Phone { get; set; } = "";
        [Required] public string Email { get; set; } = "";
        [Required] public int SiteId { get; set; }
        [Required] public int ServiceId { get; set; }
    }
}

