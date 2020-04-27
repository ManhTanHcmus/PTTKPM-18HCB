using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageRoles.Filters;
using ManageRoles.Repository;
using Newtonsoft.Json;

namespace ManageRoles.Controllers
{
    [AuthorizeUser]
    public class UserDashboardController : Controller
    {
        private readonly IMenu _iMenu;
		private readonly IToChucThi _iToChucThi;
		private readonly IDeThi _iDethi;
		private readonly ICauHoi _iCauHoi;

		public UserDashboardController(IMenu menu, ISubMenu subMenu, IToChucThi tochucthi,IDeThi dethi, ICauHoi cauhoi)
        {
            _iMenu = menu;
			_iToChucThi = tochucthi;
			_iDethi = dethi;
			_iCauHoi = cauhoi;
		}

        // GET: UserDashboard
        public ActionResult Dashboard()
        {
            return View();
        }
		public ActionResult UserQuiz()
		{
			return View();
		}
		public ActionResult ShowMenus()
        {
            try
            {
                var menuList = _iMenu.GetAllActiveMenu(Convert.ToInt32(Session["Role"]));
                return PartialView("ShowMenu", menuList);
            }
            catch (Exception)
            {
                throw;
            }
        }
		[HttpPost]
		public ActionResult AcLoadQuiz()
		{
			var i = ((long?)Session["UserID"] ?? 0);
			var c = _iToChucThi.GetListIDUser((int)i);
			var l = _iDethi.GetDeThiById(c.LastOrDefault().IDDeThi);
			var ch = _iCauHoi.GetGetListCauHoiDeThiById(l.ID);
			string json = JsonConvert.SerializeObject(ch);
			return Json(json);
		}
	}
}