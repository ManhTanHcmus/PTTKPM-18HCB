using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public interface ICauHoi
	{
		CauHoi GetCauHoiById(int? cauhoiId);
		int? AddCauHoi(CauHoi cauhoi);
		long? UpdateCauHoi(CauHoi cauhoi);
		void DeleteCauHoi(int? cauhoiId);
		List<CauHoi> GetGetListCauHoiDeThiById(int? dethiId);
	}
}
