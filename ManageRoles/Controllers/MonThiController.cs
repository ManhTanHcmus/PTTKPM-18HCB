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
	public class MonThiController : Controller
    {
		private readonly IMonThi _iMonThi;
		public MonThiController(IMonThi monthi)
		{
			_iMonThi = monthi;
		}

		public ActionResult Create()
		{
			try
			{
				var cc = GetMonThiCount() + 1;
				var monthiViewModel = new MonThiViewModel()
				{
					//ListRole = _iMonThi.GetAllActiveRoles()
				};
				monthiViewModel.MaMonThi = "MON" + cc;
				return View(monthiViewModel);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public ActionResult DetailMonThi()
		{
			return View();
		}
		[HttpPost]//Gets the todo Lists.  
		public JsonResult DetailMonThiList(string monthi, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
		{

			try
			{
				var monthiCount = GetMonThiCount();

				var monthiList = GetMonThiList(monthi, jtStartIndex, jtPageSize, jtSorting);
				return Json(new { Result = "OK", Records = monthiList, TotalRecordCount = monthiCount });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public List<MonThiViewModel> GetMonThiList(string monthi, int startIndex, int count, string sorting)
		{
			// Instance of DatabaseContext
			try
			{
				using (var db = new DatabaseContext())
				{
					var data = from monthii in db.MonThis
							   where monthii.Status == true
							   select new MonThiViewModel()
							   {	ID = monthii.ID,
									MaMonThi = monthii.MaMonThi,
									TenMonThi = monthii.TenMonThi,
									Status = monthii.Status
							   };


					IEnumerable<MonThiViewModel> query = data.ToList();

					//Search
					if (monthi != null)
					{
						query = query.Where(p => p.TenMonThi.Contains(monthi));
					}

					//Sorting Ascending and Descending
					if (string.IsNullOrEmpty(sorting) || sorting.Equals("AssignedRoleId ASC"))
					{
						query = query.OrderBy(p => p.ID);
					}
				
					else
					{
						query = query.OrderBy(p => p.TenMonThi); //Default!
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


		public int GetMonThiCount()
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					return db.MonThis.Count();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost]
		public ActionResult Create(MonThiViewModel monthiViewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{

					//var isMonThi = _iMonThi.CheckUsernameExists(createUserViewModel.UserName);
					var isMonThi = _iMonThi.CheckMonThiExists(monthiViewModel.TenMonThi);
					if (isMonThi)
					{
						ModelState.AddModelError("", "Tên môn thi đã tồn tại!");
					}

					//AesAlgorithm aesAlgorithm = new AesAlgorithm();

					var monthi = AutoMapper.Mapper.Map<MonThi>(monthiViewModel);
					monthi.Status = true;
					
					monthi.ID = 0;


					var monthiId = _iMonThi.AddMonThi(monthi);
					

					return RedirectToAction("Create", "CreateUsers");
				}
				else
				{
					return View("Create", monthiViewModel);
				}
			}
			catch
			{
				throw;
			}
		}
		// GET: MonThi
		public ActionResult Index()
        {
            return View();
        }

		public JsonResult RemoveMonThi(int ID)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					var monthii = db.MonThis.Find(ID);
					if (monthii != null) db.MonThis.Remove(monthii);
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