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
	public class ToChucThiController : Controller
    {
		private readonly IToChucThi _tochucthi;
		private readonly IUserMaster _userMaster;
		private readonly IDeThi _deThi;
		public ToChucThiController(IUserMaster userMaster, IToChucThi tochucthi, IDeThi dethi)
		{
			_userMaster = userMaster;
			_tochucthi = tochucthi;
			_deThi = dethi;
		}
		// GET: ToChucThi
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult Create()
		{
			try
			{
				ToChucThiViewModel tochucthi = new ToChucThiViewModel()
				{
					ListUsers = _userMaster.GetAllUsersActiveList(),
					ListDeThi = _deThi.GetAllActiveDeThi()

				};
				return View(tochucthi);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost]
		public ActionResult Create(ToChucThiViewModel tochucthiView)
		{
			try
			{
				if (ModelState.IsValid)
				{
					ToChucThiViewModel tochucthi = new ToChucThiViewModel()
					{
						ListUsers = _userMaster.GetAllUsersActiveList(),
						ListDeThi = _deThi.GetAllActiveDeThi()

					};
					return View(tochucthi);
				}

				else
				{
					var tochucthi = AutoMapper.Mapper.Map<ToChucThi>(tochucthiView);
					tochucthi.Status = true;

					tochucthi.ID = 0;

					_tochucthi.AddToChucThi(tochucthi);
				
					return RedirectToAction("Create", "ToChucThi");
				}

			}
			catch (Exception)
			{
				throw;
			}
		}
		public ActionResult ListThi()
		{
			return View();
		}

		[HttpPost]//Gets the todo Lists.  
		public JsonResult DetailListThi(string tochucthi, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
		{

			try
			{
				var monthiCount = GetToChucThiCount();

				var monthiList = GetToChucThiList(tochucthi, jtStartIndex, jtPageSize, jtSorting);
				return Json(new { Result = "OK", Records = monthiList, TotalRecordCount = monthiCount });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public List<ToChucThiViewModel> GetToChucThiList(string monthi, int startIndex, int count, string sorting)
		{
			// Instance of DatabaseContext
			try
			{
				using (var db = new DatabaseContext())
				{
					var data = from tochuc in db.ToChucThis
							   where tochuc.Status == true
							   select new ToChucThiViewModel()
							   {
								   ID = tochuc.ID,
								   IDUser = tochuc.IDUser,
								   IDDeThi = tochuc.IDDeThi,
								   Status = tochuc.Status
							   };
		
					

					IEnumerable<ToChucThiViewModel> query = data.ToList();
					foreach (var item in query)
					{
						var c = _userMaster.GetUserById(item.IDUser);
						var d = _deThi.GetDeThiById(item.IDDeThi);
						item.TenUser = c != null ? c.UserName : "";
						item.TenDeThi = d != null ? d.TenDeThi : "";
					}
					//Search


					//Sorting Ascending and Descending
					if (string.IsNullOrEmpty(sorting) || sorting.Equals("AssignedRoleId ASC"))
					{
						query = query.OrderBy(p => p.ID);
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


		public int GetToChucThiCount()
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					return db.ToChucThis.Count();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public JsonResult RemoveMonThi(int ID)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					var td = db.ToChucThis.Find(ID);
					if (td != null) db.ToChucThis.Remove(td);
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