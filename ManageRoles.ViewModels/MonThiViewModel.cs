using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.ViewModels
{
	public class MonThiViewModel
	{
		public int ID { get; set; }
		[Required(ErrorMessage = "Enter FirstName")]
		public string MaMonThi { get; set; }
		public string TenMonThi { get; set; }
		public bool? Status { get; set; }
	}
}
