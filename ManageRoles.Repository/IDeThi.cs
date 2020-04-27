using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public interface IDeThi
	{
		DeThi GetDeThiById(int? dethiID);
		long? AddDeThi(DeThi dethi);
		long? UpdateDeThi(DeThi dethi);
		void DeleteDeThi(int? dethiId);
		bool CheckDeThiExits(string dethi);
		DeThi GetDeThiByTenDeThi(string dethi);
		List<DeThi> GetAllActiveDeThi();
	}
}
