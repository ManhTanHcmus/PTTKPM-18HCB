using ManageRoles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Repository
{
	public class ToChucThiConcrete : IToChucThi
	{
		private readonly DatabaseContext _context;
		private bool _disposed = false;
		public ToChucThiConcrete(DatabaseContext context)
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

		public ToChucThi GetToChucThiById(int? tochucthiID)
		{
			try
			{
				return _context.ToChucThis.Find(tochucthiID);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long? AddToChucThi(ToChucThi tochucthi)
		{
			try
			{
				long? result = -1;

				if (tochucthi != null)
				{
					tochucthi.Status = true;
					//usermaster.CreateDate = DateTime.Now;
					_context.ToChucThis.Add(tochucthi);
					_context.SaveChanges();
					result = tochucthi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long? UpdateToChucThi(ToChucThi tochucthi)
		{
			try
			{

				long? result = -1;

				if (tochucthi != null)
				{
					//monthi.CreateDate = DateTime.Now;
					_context.Entry(tochucthi).State = EntityState.Modified;
					_context.SaveChanges();
					result = tochucthi.ID;
				}
				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void DeleteToChucThi(int? tochucthiId)
		{
			try
			{
				ToChucThi tochucthi = _context.ToChucThis.Find(tochucthiId);
				if (tochucthi != null) _context.ToChucThis.Remove(tochucthi);
				_context.SaveChanges();
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

	
		public List<ToChucThi> GetAllActiveToChucThi()
		{
			try
			{
				var listofActiveToChucThi = (from tochucthi in _context.ToChucThis
										 where tochucthi.Status == true
										 select tochucthi).ToList();

	

				return listofActiveToChucThi;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public List<ToChucThi> GetListIDUser(int? iduser)
		{
			try
			{
				var listoListoChucThi = (from tochucthi in _context.ToChucThis
											 where tochucthi.Status == true && tochucthi.IDUser == iduser
											 select tochucthi).ToList();



				return listoListoChucThi;
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
