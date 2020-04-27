using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Models
{
	[Table("MonThi")]
	public class MonThi
	{	[Key]
		public int ID { get; set; }
		public string MaMonThi {get;set;}
		public string TenMonThi { get; set; }
		public bool? Status { get; set; }
	}
}
