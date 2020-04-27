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
	public class CauHoiController : Controller
    {

		private readonly ICauHoi _iCauHoi;
		private readonly IDeThi _iDeThi;
		private string S_LISTDETHI = "S_LISTDETHI";

		public CauHoiController(ICauHoi cauhoi, IDeThi dethi)
		{
			_iCauHoi = cauhoi;
			_iDeThi = dethi;
		}
		// GET: CauHoi

		public ActionResult Index()
        {
			
			return View();
        }
		public ActionResult Create()
		{
			var c = new List<CauHoiViewModel>();
			var item = new CauHoiViewModel();
			item.ListDeThi = _iDeThi.GetAllActiveDeThi();
			Session[S_LISTDETHI] = c;
			return View(item);
		}
		
		public ActionResult ListCauHoi(string cauhoi, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
		{
			List<CauHoiViewModel> listCauHoi = Session[S_LISTDETHI] as List<CauHoiViewModel>;

			return Json(new { Result = "OK", Records = listCauHoi, TotalRecordCount = listCauHoi.Count });
	

		}
		public ActionResult ListCauHoiDetail()
		{
			return null;  // câu hỏi đề thi
		}
		public ActionResult CauHoiList()
		{
			return View();
		}
		[HttpPost]//Gets the todo Lists.  
		public JsonResult ListCauHoiSave(string monthi, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
		{

			try
			{
				var monthiCount = GetCauHoiCount();

				var monthiList = GetCauHoiList(monthi, jtStartIndex, jtPageSize, jtSorting);
				return Json(new { Result = "OK", Records = monthiList, TotalRecordCount = monthiCount });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public List<CauHoiViewModel> GetCauHoiList(string cauhoi, int startIndex, int count, string sorting)
		{
			// Instance of DatabaseContext
			try
			{
				using (var db = new DatabaseContext())
				{
					var data = from cauhoii in db.CauHois
							   where cauhoii.Status == true
							   select new CauHoiViewModel()
							   {
								   ID = cauhoii.ID,		
								   IDDeThi=cauhoii.IDDeThi,
								   DeBai = cauhoii.DeBai,
								   PhuongAnA = cauhoii.PhuongAnA,
								   PhuongAnB = cauhoii.PhuongAnB,
								   PhuongAnC = cauhoii.PhuongAnC,
								   PhuongAnD = cauhoii.PhuongAnD,
								   DapAn = cauhoii.DapAn,
								   Status = cauhoii.Status
							   };


					IEnumerable<CauHoiViewModel> query = data.ToList();
					foreach (var item in query)
					{
						var cc = _iDeThi.GetDeThiById(item.IDDeThi);
						if (cc != null) item.TenDeThi = cc.TenDeThi;
					}
					//Search
					if (cauhoi != null)
					{
						query = query.Where(p => p.DapAn.Contains(cauhoi));
					}

					//Sorting Ascending and Descending
					if (string.IsNullOrEmpty(sorting) || sorting.Equals("AssignedRoleId ASC"))
					{
						query = query.OrderBy(p => p.ID);
					}

					else
					{
						query = query.OrderBy(p => p.DapAn); //Default!
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
		public JsonResult RemoveCauHoi(int ID)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					var cauhoiii = db.CauHois.Find(ID);
					if (cauhoiii != null) db.CauHois.Remove(cauhoiii);
					db.SaveChanges();
				}
				return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
			}
		}

		public int GetCauHoiCount()
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					return db.CauHois.Count();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpPost]
		public JsonResult AddCreateItem(CauHoiViewModel Model)
		{
			List<CauHoiViewModel> listCauHoi = Session[S_LISTDETHI] as List<CauHoiViewModel>;
			try
			{
				if (listCauHoi != null)
				{
					if (ModelState.IsValid)
					{
						int count = listCauHoi.Count(x => x.ID == Model.ID);
						if (count > 0)
						{
							//ViewData["EditError"] = MsgResources.DataExist;
						}
						else
						{
							Model.ID = listCauHoi.Count + 1;
							listCauHoi.Add(Model);
						}

						Session[S_LISTDETHI] = listCauHoi;
					}
					//else
					//	ViewData["EditError"] = MsgResources.InvalidValue;
				}

				return Json(new { Result = "OK", Record = Model }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
			}
		}
		[HttpPost]
		public JsonResult EditItem(CauHoiViewModel item)
		{
			
			try
			{
				List<CauHoiViewModel> listCauHoi = Session[S_LISTDETHI] as List<CauHoiViewModel>; 
				if (listCauHoi != null)
				{
					if (ModelState.IsValid)
					{
						CauHoiViewModel modelItem = listCauHoi.SingleOrDefault(x => x.ID == item.ID);
						if (modelItem != null)
						{
							
								modelItem.ID = item.ID;
								modelItem.DeBai = item.DeBai;
								modelItem.PhuongAnA = item.PhuongAnA;
								modelItem.PhuongAnB = item.PhuongAnB;
								modelItem.PhuongAnC = item.PhuongAnC;
								modelItem.PhuongAnD = item.PhuongAnD;
								modelItem.Status = item.Status;					
						}
						Session[S_LISTDETHI] = listCauHoi;
					}
					//else
					//	ViewData["EditError"] = MsgResources.InvalidValue;
				}
				return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
			}
		}

		[HttpPost]
		public JsonResult DeleteItem(int? ID)
		{
			List<CauHoiViewModel> listCauHoi = Session[S_LISTDETHI] as List<CauHoiViewModel>;
			try
			{
				if (ID > 0)
				{
					try
					{
						var modelItem = listCauHoi.FirstOrDefault(s => s.ID == ID);

						if (modelItem != null)
						{
							listCauHoi.Remove(modelItem);
						}
					}
					catch (Exception e)
					{
						
					}
				}
				//else
				//	ViewData["EditError"] = MsgResources.InvalidID;

				return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
			}
		}
		[HttpPost]
		public JsonResult SaveDate(int? IdDeThi)
		{
			List<CauHoiViewModel> listCauHoi = Session[S_LISTDETHI] as List<CauHoiViewModel>;
			foreach(var item in listCauHoi)
			{
			//	item.IDDeThi = (int)IdDeThi;
				var cauhoi = AutoMapper.Mapper.Map<CauHoi>(item);
				cauhoi.IDDeThi = (int)IdDeThi;
				if (cauhoi.DapAn=="A")
				{
					cauhoi.DapAn = cauhoi.PhuongAnA;
				}
				else if(cauhoi.DapAn == "B")
				{
					cauhoi.DapAn = cauhoi.PhuongAnB;
				}
				else if (cauhoi.DapAn == "C")
				{
					cauhoi.DapAn = cauhoi.PhuongAnC;
				}
				else if (cauhoi.DapAn == "D")
				{
					cauhoi.DapAn = cauhoi.PhuongAnD;
				}
				var monthiId = _iCauHoi.AddCauHoi(cauhoi);
			}
			return Json(new { Result = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);
		}
	}
}