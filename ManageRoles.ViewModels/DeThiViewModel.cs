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
	public class DeThiViewModel
	{
		public int ID { get; set; }
		[Required(ErrorMessage = "Mã đề thi không được trống!")]
		public string MaDeThi { get; set; }
		[Required(ErrorMessage = "Tên môn thi không được trống!")]
		public string TenDeThi { get; set; }
		[MonThiValidate]
		[DisplayName("MonThi")]
		[Required(ErrorMessage = "Không để trống môn thi!")]
		public int? IDMonThi { get; set; }
		public bool? Status { get; set; }

		public List<MonThi> ListMonThi { get; set; }
	}
}
