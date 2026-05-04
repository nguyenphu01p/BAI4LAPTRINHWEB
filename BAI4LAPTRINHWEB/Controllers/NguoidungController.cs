using BAI4LAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAI4LAPTRINHWEB.Controllers
{
    public class NguoidungController : Controller
    {
        // Khởi tạo đối tượng data (Kết nối CSDL)
        dbQLBansachDataContext data = new dbQLBansachDataContext("");

        // GET: Nguoidung/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Nguoidung/Dangky (Hiển thị trang đăng ký)
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        // POST: Nguoidung/Dangky (Xử lý khi nhấn nút Đăng ký)
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            // Lấy giá trị từ các thẻ input trong View (thuộc tính name)
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"]; // Nên có thêm ô này
            var email = collection["Email"];
            var diachi = collection["DiachiKH"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);

            // 1. Kiểm tra tính hợp lệ (Validation)
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi4"] = "Email không được bỏ trống";
            }
            // ... (Bạn có thể thêm kiểm tra mật khẩu nhập lại ở đây)
            else if (matkhau != matkhaunhaplai) { ViewData["Loi"] = "Mật khẩu không khớp"; }
            else
            {
                // 2. Nếu mọi thứ ok, gán dữ liệu vào object 'kh'
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);

                // 3. Lưu vào Database
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();

                // 4. Chuyển hướng sang trang đăng nhập
                return RedirectToAction("Dangnhap");
            }

            // Nếu có lỗi, load lại trang đăng ký và hiển thị thông báo lỗi
            return View();

        }
        // Code cho Đăng nhập
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // Gán giá trị cho đối tượng được tạo mới (kh)
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        // hết code cho đăng nhập
    }
}