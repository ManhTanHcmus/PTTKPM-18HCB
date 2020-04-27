using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public class CauHoiConcrete : ICauHoi
	{
		private readonly DatabaseContext _context;
		private bool _disposed = false;
		public CauHoiConcrete(DatabaseContext context)
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

		public CauHoi GetCauHoiById(int? cauhoiID)
		{
			try
			{
				return _context.CauHois.Find(cauhoiID);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public int? AddCauHoi(CauHoi cauhoi)
		{
			try
			{
				int? result = -1;

				if (cauhoi != null)
				{
					cauhoi.Status = true;
					//usermaster.CreateDate = DateTime.Now;
					_context.CauHois.Add(cauhoi);
					_context.SaveChanges();
					result = cauhoi.ID;
				}
				return result;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public long? UpdateCauHoi(CauHoi cauhoi)
		{
			try
			{

				long? result = -1;

				if (cauhoi != null)
				{
					//monthi.CreateDate = DateTime.Now;
					_context.Entry(cauhoi).State = EntityState.Modified;
					_context.SaveChanges();
					result = cauhoi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void DeleteCauHoi(int? cauhoiID)
		{
			try
			{
				CauHoi cauhoi = _context.CauHois.Find(cauhoiID);
				if (cauhoi != null) _context.CauHois.Remove(cauhoi);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		public List<CauHoi> GetGetListCauHoiDeThiById(int? ID)
		{
			try
			{
				var listCauHoi = (from dethi in _context.CauHois
										 where dethi.Status == true
										 select dethi).ToList();

				return listCauHoi;
			}
			catch (Exception)
			{

				throw;
			}
		}



	}
}
