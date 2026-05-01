using BAI4LAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAI4LAPTRINHWEB.Controllers
{
    public class BookStoreController : Controller
    {
        // Tạo đối tượng kết nối CSDL
        dbQLBanSACHDataContext data = new dbQLBanSACHDataContext("Data Source=Nguyễn-phú;Initial Catalog=QLBANSACH;Integrated Security=True;TrustServerCertificate=True");

        // Hàm lấy sách mới
        private List<SACH> LaySACHmoi(int count)
        {
            // Sắp xếp giảm dần theo ngày cập nhật, lấy số lượng sách theo count
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // Action Index
        public ActionResult Index()
        {
            // Lấy 5 quyển sách mới nhất
            var SACHmoi = LaySACHmoi(5);
            return View(SACHmoi);
        }

        // Action hiển thị chủ đề
        public ActionResult CHUDE()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        // Action hiển thị nhà xuất bản
        public ActionResult NHAXUATBAN()
        {
            var NXB = from cd in data.NHAXUATBANs select cd;
            return PartialView(NXB);
        }
    }
}
