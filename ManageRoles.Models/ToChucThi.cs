using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Models
{
	[Table("ToChucThi")]
	public class ToChucThi
	{
		[Key]
		public int ID { get; set; }
		public int? IDUser { get; set; }
		public int? IDDeThi { get; set; }
		public int? SoCauHoiDung { get; set; }
		public bool? Status { get; set; }
	}
}
