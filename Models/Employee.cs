using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroGestion.Models
{
	public class Employee
	{
        [Key] public int Id { get; set; }
        [StringLength(64)] public string Name { get; set; } = "";
        [StringLength(64)] public string Surname { get; set; } = "";
        [StringLength(64)] public string Phone { get; set; } = "";
        [StringLength(64)] public string Email { get; set; } = "";

        [ForeignKey("Site")] public int SiteId { get; set; }
        public Site Site { get; set; } = new Site();

        [ForeignKey("Service")] public int ServiceId { get; set; }
        public Service Service { get; set; } = new Service();
    }
}