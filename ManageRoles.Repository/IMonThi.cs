using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public interface IMonThi 
	{
		MonThi GetMonThiById(int? monthiId);
		long? AddMonThi(MonThi monthi);
		long? UpdateMonThi(MonThi monthi);
		void DeleteMonThi(int? monthiId);
		bool CheckMonThiExists(string monthi);
		MonThi GetMonThiByTenMonThi(string monthi);
		List<MonThi> GetAllActiveMonThi();
	}
}
