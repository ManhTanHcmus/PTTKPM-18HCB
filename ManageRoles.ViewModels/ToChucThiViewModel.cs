using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.ViewModels
{
	public class ToChucThiViewModel
	{
		public int ID { get; set; }
		public int? IDUser { get; set; }
		public int? IDDeThi { get; set; }
		public int? SoCauHoiDung { get; set; }
		public bool? Status { get; set; }
		[DisplayName("User")]
		[Required(ErrorMessage = "Choose User")]
		[UserValidate]
		public List<DropdownUsermaster> ListUsers { get; set; }
		[DisplayName("DeThi")]
		[Required(ErrorMessage = "Chọn đề thi")]
		[DeThiValidate]
		public List<DeThi> ListDeThi { get; set; }
		public string TenUser { get; set; }
		public string TenDeThi { get; set; }
	}

}
