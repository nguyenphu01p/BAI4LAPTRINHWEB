using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAI4LAPTRINHWEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Đổi tên từ About thành vetoi cho khớp với Menu
        public ActionResult vetoi()
        {
            ViewBag.Message = "Đây là trang thông tin về tôi.";
            return View();
        }

        // Đổi tên từ Contact thành lienhe cho khớp với Menu
        public ActionResult lienhe()
        {
            ViewBag.Message = "Bạn có thể tìm tôi tại đây.";
            return View();
        }
    }
}