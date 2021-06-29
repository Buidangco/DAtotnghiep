using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DATNMVC.Models;
using Newtonsoft.Json;
using PagedList;

namespace DATNMVC.Areas.Area.Controllers
{
    public class UserController : Controller
    {
        private DA_TOTNGHIEPEntities3 db = new DA_TOTNGHIEPEntities3();
        // GET: Area/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        // GET: /Link/
        public ActionResult GetThongTinGiangVien(int Id, int? size, int? page)
        {
            // 1. Tạo list pageSize để người dùng có thể chọn xem để phân trang
            // Bạn có thể thêm bớt tùy ý --- dammio.com
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            // 1.1. Giữ trạng thái kích thước trang được chọn trên DropDownList
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }

            // 1.2. Tạo các biến ViewBag
            ViewBag.size = items; // ViewBag DropDownList
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại

            // 2. Nếu page = null thì đặt lại là 1.
            page = page ?? 1; //if (page == null) page = 1;


            // 4. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.

            List<ThongTinGiangVien> thongTinGiangViens = new List<ThongTinGiangVien>();
            string apiUrl = "https://localhost:44307/api";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/ThongTinTheoKhoa?id={0}", Id)).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                thongTinGiangViens = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }

            List<ChucVu> chucVus = new List<ChucVu>();
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = client1.GetAsync(apiUrl + string.Format("/ChucVus")).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                chucVus = JsonConvert.DeserializeObject<List<ChucVu>>(data);
            }
            ViewBag.Chucvu = chucVus;

            return View("Index1",thongTinGiangViens.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Khoas()
        {
            List<Khoa> khoas = new List<Khoa>();
            string apiUrl = "https://localhost:44307/api";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/Khoas")).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                khoas = JsonConvert.DeserializeObject<List<Khoa>>(data);
            }

            return PartialView("_PartialMenu", khoas);
        }

        public ActionResult GetThongTinGiangVien(int Id)
        {
            List<ThongTinGiangVien> thongTinGiangViens = new List<ThongTinGiangVien>();
            string apiUrl = "https://localhost:44307/api";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/ThongTinTheoKhoa?id={0}", Id)).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                thongTinGiangViens = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }

            List<ChucVu> chucVus = new List<ChucVu>();
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = client1.GetAsync(apiUrl + string.Format("/ChucVus")).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                chucVus = JsonConvert.DeserializeObject<List<ChucVu>>(data);
            }
            ViewBag.Chucvu = chucVus;
            return View("Index1", thongTinGiangViens);
        }

        public ActionResult GetThongTinGiangVien1(int Id)
        {
            ThongTinGiangVien thongTin = new ThongTinGiangVien();
            string apiUrl = "https://localhost:44307/api";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/ThongTinChiTiet?id={0}", Id)).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                thongTin = JsonConvert.DeserializeObject<ThongTinGiangVien>(data);
            }

            List<DuAn> duAns = new List<DuAn>();
            HttpResponseMessage response1 = client.GetAsync(apiUrl + string.Format("/ThongTinChiTietDuAn?id={0}", Id)).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                duAns = JsonConvert.DeserializeObject<List<DuAn>>(data);
            }

            List<QuaTrinhCongTac> quaTrinhCongTacs = new List<QuaTrinhCongTac>();
            HttpResponseMessage response2 = client.GetAsync(apiUrl + string.Format("/ThongTinChiTietQTCT?id={0}", Id)).Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                quaTrinhCongTacs = JsonConvert.DeserializeObject<List<QuaTrinhCongTac>>(data);
            }

            List<QuaTrinhDaoTao> quaTrinhDaoTaos = new List<QuaTrinhDaoTao>();
            HttpResponseMessage response3 = client.GetAsync(apiUrl + string.Format("/ThongTinChiTietQTDT?id={0}", Id)).Result;
            if (response3.IsSuccessStatusCode)
            {
                string data = response3.Content.ReadAsStringAsync().Result;
                quaTrinhDaoTaos = JsonConvert.DeserializeObject<List<QuaTrinhDaoTao>>(data);
            }
            List<ChucVu> chucVus = new List<ChucVu>();
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response4 = client1.GetAsync(apiUrl + string.Format("/ChucVus")).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response4.Content.ReadAsStringAsync().Result;
                chucVus = JsonConvert.DeserializeObject<List<ChucVu>>(data);
            }

            ViewBag.Chucvu = chucVus;
            ViewBag.Duan = duAns;
            ViewBag.QTCT = quaTrinhCongTacs;
            ViewBag.QTDT = quaTrinhDaoTaos;

            return View("ChiTiet", thongTin);
        }
    }
}