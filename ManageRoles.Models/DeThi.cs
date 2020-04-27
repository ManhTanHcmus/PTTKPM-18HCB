using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Models
{	[Table("DeThi")]
	public class DeThi
	{	[Key]
		public int ID { get; set; }
		public string MaDeThi { get; set; }
		public string TenDeThi { get; set; } 
		public int IDMonThi { get; set; }
		public bool? Status { get; set; }
	}
}
