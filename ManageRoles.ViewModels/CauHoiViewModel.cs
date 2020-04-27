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
	public class CauHoiViewModel
	{
		public int ID { get; set; }
		public int? IDDeThi { get; set; }
		public string DeBai { get; set; }
		public string PhuongAnA { get; set; }
		public string PhuongAnB { get; set; }
		public string PhuongAnC { get; set; }
		public string PhuongAnD { get; set; }
		public string DapAn { get; set; }
		public string TenDeThi { get; set; }
		public bool? Status { get; set; }
		public List<DeThi> ListDeThi { get; set; }

	}
}
