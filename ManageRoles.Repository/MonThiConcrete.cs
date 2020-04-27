using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	//class MonThiConcrete
	//{
	//}
	public class MonThiConcrete : IMonThi
	{
		private readonly DatabaseContext _context;
		private bool _disposed = false;

		public MonThiConcrete(DatabaseContext context)
		{
			_context = context;
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this._disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		//public List<MonThi> GetAllUsers()
		//{
		//	try
		//	{
		//		return _context.Usermasters.ToList();
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}
		//}
		public List<MonThi> GetAllActiveMonThi()
		{
			try
			{
				var listofActiveMonThi = (from monthi in _context.MonThis
										where monthi.Status == true
										select monthi).ToList();

				listofActiveMonThi.Insert(0, new MonThi()
				{
					ID= -1,
					TenMonThi = "---Select---"
				});

				return listofActiveMonThi;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public MonThi GetMonThiById(int? monthiID)
		{
			try
			{
				return _context.MonThis.Find(monthiID);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long? AddMonThi(MonThi monthi)
		{
			try
			{
				long? result = -1;

				if (monthi != null)
				{
					monthi.Status = true;
					//usermaster.CreateDate = DateTime.Now;
					_context.MonThis.Add(monthi);
					_context.SaveChanges();
					result = monthi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long? UpdateMonThi(MonThi monthi)
		{
			try
			{

				long? result = -1;

				if (monthi != null)
				{
					//monthi.CreateDate = DateTime.Now;
					_context.Entry(monthi).State = EntityState.Modified;
					_context.SaveChanges();
					result = monthi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void DeleteMonThi(int? monthiId)
		{
			try
			{
				MonThi monthi = _context.MonThis.Find(monthiId);
				if (monthi != null) _context.MonThis.Remove(monthi);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public bool CheckMonThiExists(string monthi)
		{
			try
			{
				var result = (from mm in _context.MonThis
							  where mm.TenMonThi == monthi
							  select mm).Any();

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public MonThi GetMonThiByTenMonThi(string monthi)
		{
			try
			{
				var result = (from mm in _context.MonThis
							  where mm.TenMonThi == monthi
							  select mm).FirstOrDefault();

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IQueryable<MonThi> GetAllMonThi(string sortColumn, string sortColumnDir, string search)
		{
			try
			{
				var queryableMonThi = (from monthi in _context.MonThis
									   select monthi
					);

				if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
				{
					//queryableMonThi = queryableMonThi.OrderBy(sortColumn + " " + sortColumnDir);
				}
				if (!string.IsNullOrEmpty(search))
				{
					queryableMonThi = queryableMonThi.Where(m => m.TenMonThi.Contains(search) || m.TenMonThi.Contains(search));
				}

				return queryableMonThi;

			}
			catch (Exception)
			{
				throw;
			}
		}

		//public List<DropdownUsermaster> GetAllUsersActiveList()
		//{
		//	try
		//	{
		//		var listactiveUsers = (from usermaster in _context.Usermasters
		//							   where usermaster.Status == true
		//							   select new DropdownUsermaster
		//							   {
		//								   UserId = usermaster.UserId,
		//								   UserName = usermaster.UserName
		//							   }).ToList();

		//		listactiveUsers.Insert(0, new DropdownUsermaster()
		//		{
		//			UserId = -1,
		//			UserName = "---Select---"
		//		});

		//		return listactiveUsers;
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

	}
}
