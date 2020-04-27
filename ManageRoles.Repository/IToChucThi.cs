using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public interface IToChucThi
	{
		ToChucThi GetToChucThiById(int? tochucthiID);
		long? AddToChucThi(ToChucThi tochucthi);
		long? UpdateToChucThi(ToChucThi tochucthi);
		void DeleteToChucThi(int? tochucthiID);
		List<ToChucThi> GetAllActiveToChucThi();
		List<ToChucThi> GetListIDUser(int? IDUser);
	}
}
