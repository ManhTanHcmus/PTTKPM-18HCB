using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public class DeThiConcrete : IDeThi
	{
		private readonly DatabaseContext _context;
		private bool _disposed = false;
		public DeThiConcrete(DatabaseContext context)
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
		
		public DeThi GetDeThiById(int? dethiID)
		{
			try
			{
				return _context.DeThis.Find(dethiID);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long? AddDeThi(DeThi dethi)
		{
			try
			{
				long? result = -1;

				if (dethi != null)
				{
					dethi.Status = true;
					//usermaster.CreateDate = DateTime.Now;
					_context.DeThis.Add(dethi);
					_context.SaveChanges();
					result = dethi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long? UpdateDeThi(DeThi dethi)
		{
			try
			{

				long? result = -1;

				if (dethi != null)
				{
					//monthi.CreateDate = DateTime.Now;
					_context.Entry(dethi).State = EntityState.Modified;
					_context.SaveChanges();
					result = dethi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void DeleteDeThi(int? dethiId)
		{
			try
			{
				DeThi dethi = _context.DeThis.Find(dethiId);
				if (dethi != null) _context.DeThis.Remove(dethi);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public bool CheckDeThiExits(string dethi)
		{
			try
			{
				var result = (from mm in _context.DeThis
							  where mm.TenDeThi == dethi
							  select mm).Any();

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DeThi GetDeThiByTenDeThi(string dethi)
		{
			try
			{
				var result = (from mm in _context.DeThis
							  where mm.TenDeThi == dethi
							  select mm).FirstOrDefault();

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IQueryable<DeThi> GetAllDeThi(string sortColumn, string sortColumnDir, string search)
		{
			try
			{
				var queryableDeThi = (from dethi in _context.DeThis
									   select dethi
					);

				if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
				{
					//queryableMonThi = queryableMonThi.OrderBy(sortColumn + " " + sortColumnDir);
				}
				if (!string.IsNullOrEmpty(search))
				{
					queryableDeThi = queryableDeThi.Where(m => m.TenDeThi.Contains(search) || m.TenDeThi.Contains(search));
				}

				return queryableDeThi;

			}
			catch (Exception)
			{
				throw;
			}
		}
		public List<DeThi> GetAllActiveDeThi()
		{
			try
			{
				var listofActiveDeThi = (from dethi in _context.DeThis
										  where dethi.Status == true
										  select dethi).ToList();

				listofActiveDeThi.Insert(0, new DeThi()
				{
					ID = -1,
					TenDeThi = "---Select---"
				});

				return listofActiveDeThi;
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
