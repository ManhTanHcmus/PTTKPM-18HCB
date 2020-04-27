using ManageRoles.Filters;
using ManageRoles.Models;
using ManageRoles.Repository;
using ManageRoles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ManageRoles.Controllers
{
	[AuthorizeSuperAdminandAdmin]
	public class DeThiController : Controller
    {
		private readonly IDeThi _iDeThi;
		private readonly IMonThi _iMonThi;
		public DeThiController(IDeThi dethi,IMonThi monthi)
		{
			_iDeThi = dethi;
			_iMonThi = monthi;
		}
		public ActionResult Create()
		{
			try
			{
				var cc = GetDeThiCount() + 1;
				var deThiViewModel = new DeThiViewModel();
				deThiViewModel.ListMonThi = _iMonThi.GetAllActiveMonThi();
				deThiViewModel.MaDeThi = "DE" + cc;
				return View(deThiViewModel);
			}
			catch (Exception)
			{
				throw;
			}
		}
		// GET: DeThi
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult ListDeThi()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Create(DeThiViewModel dethiViewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{

					var isDeThi = _iDeThi.CheckDeThiExits(dethiViewModel.TenDeThi);
					if (isDeThi)
					{
						ModelState.AddModelError("", "Tên đề thi đã tồn tại!");
					}


					var dethi = AutoMapper.Mapper.Map<DeThi>(dethiViewModel);
					dethi.Status = true;

					dethi.ID = 0;


					var monthiId = _iDeThi.AddDeThi(dethi);


					return RedirectToAction("Create", "CreateUsers");
				}
				else
				{
					dethiViewModel.ListMonThi = _iMonThi.GetAllActiveMonThi();
					return View("Create", dethiViewModel);
				}
			}
			catch
			{
				throw;
			}
		}

		[HttpPost]//Gets the todo Lists.  
		public JsonResult ListDeThi(string dethi, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
		{

			try
			{
				var dethiCount = GetDeThiCount();

				var monthiList = GetDeThiList(dethi, jtStartIndex, jtPageSize, jtSorting);
				return Json(new { Result = "OK", Records = monthiList, TotalRecordCount = dethiCount });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public List<DeThiViewModel> GetDeThiList(string dethi, int startIndex, int count, string sorting)
		{
			// Instance of DatabaseContext
			try
			{
				using (var db = new DatabaseContext())
				{
					var data = from dethii in db.DeThis
							   where dethii.Status == true
							   select new DeThiViewModel()
							   {
								   ID = dethii.ID,
								   MaDeThi = dethii.MaDeThi,
								   TenDeThi = dethii.TenDeThi,
								   Status = dethii.Status
							   };


					IEnumerable<DeThiViewModel> query = data.ToList();

					//Search
					if (dethi != null)
					{
						query = query.Where(p => p.TenDeThi.Contains(dethi));
					}

					////Sorting Ascending and Descending
					//if (string.IsNullOrEmpty(sorting) || sorting.Equals("AssignedRoleId ASC"))
					//{
					//	query = query.OrderBy(p => p.ID);
					//}

					else
					{
						query = query.OrderBy(p => p.TenDeThi); //Default!
					}

					return count > 0
							   ? query.Skip(startIndex).Take(count).ToList()  //Paging
							   : query.ToList(); //No paging
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}


		public int GetDeThiCount()
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					return db.DeThis.Count();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public JsonResult RemoveDeThi(int ID)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					var dethii = db.DeThis.Find(ID);
					if (dethii != null) db.DeThis.Remove(dethii);
					db.SaveChanges();
				}
				return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
			}
		}
	}
}