using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DATNMVC.Models;
using Newtonsoft.Json;
namespace DATNMVC.Controllers
{
    public class ThongTinGiangVienController : Controller
    {
        DA_TOTNGHIEPEntities3 db = new DA_TOTNGHIEPEntities3();

        // GET: ThongTinGiangVien
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongTinGiangViens(string name)
        {
            List<ThongTinGiangVien> thongTinGiangViens = new List<ThongTinGiangVien>();
            string apiUrl = "https://localhost:44307/api";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/ThongTinGiangViens")).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                thongTinGiangViens = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }

            return View(thongTinGiangViens);
        }

        public ActionResult ThongTinDuAn()
        {
            List<DuAn> duAns = new List<DuAn>();
            string apiUrl = "https://localhost:44307/api";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/DuAns")).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                duAns = JsonConvert.DeserializeObject<List<DuAn>>(data);
            }

            List<ThongTinGiangVien> thongTinGiangViens = new List<ThongTinGiangVien>();
            HttpResponseMessage response1 = client.GetAsync(apiUrl + string.Format("/ThongTinGiangViens")).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                thongTinGiangViens = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }

            ViewBag.ThongTin = thongTinGiangViens;

            return View(duAns);
        }

        public ActionResult ThongTinQTCT()
        {
            List<QuaTrinhCongTac> quaTrinhCongTacs = new List<QuaTrinhCongTac>();
            string apiUrl = "https://localhost:44307/api";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/QuaTrinhCongTacs")).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                quaTrinhCongTacs = JsonConvert.DeserializeObject<List<QuaTrinhCongTac>>(data);
            }

            return View(quaTrinhCongTacs);
        }

        public ActionResult ThongTinQTDT()
        {
            List<QuaTrinhDaoTao> quaTrinhDaoTaos = new List<QuaTrinhDaoTao>();
            string apiUrl = "https://localhost:44307/api";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/QuaTrinhDaoTaos")).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                quaTrinhDaoTaos = JsonConvert.DeserializeObject<List<QuaTrinhDaoTao>>(data);
            }

            return View(quaTrinhDaoTaos);
        }

        [HttpGet]
        public ActionResult Create(ThongTinGiangVien model)
        {
            string apiUrl = "https://localhost:44307/api";
            HttpClient client = new HttpClient();
            List<ChucVu> chucVus = new List<ChucVu>();
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response4 = client1.GetAsync(apiUrl + string.Format("/ChucVus")).Result;
            if (response4.IsSuccessStatusCode)
            {
                string data = response4.Content.ReadAsStringAsync().Result;
                chucVus = JsonConvert.DeserializeObject<List<ChucVu>>(data);
            }
            List<Khoa> khoas = new List<Khoa>();
            HttpResponseMessage response5 = client1.GetAsync(apiUrl + string.Format("/Khoas")).Result;
            if (response5.IsSuccessStatusCode)
            {
                string data = response5.Content.ReadAsStringAsync().Result;
                khoas = JsonConvert.DeserializeObject<List<Khoa>>(data);
            }
            ViewBag.chucvu = new MultiSelectList(chucVus, "ID", "tenChucVu");
            ViewBag.khoa = new MultiSelectList(khoas, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddGiangVien(ThongTinGiangVien model,bool Checkbox, int IDChuc, int IDKhoa)
        {
            model.idChucVu = (byte)(byte?)IDChuc;
            model.IdKhoa = (byte?)IDKhoa;
            model.GioiTinh = Checkbox;
            string Date = DateTime.Now.ToString();
            string Code = Date.Replace("/", "").Replace(" ", "").Replace(":", "").Replace("PM", "").Replace("AM","");
            model.Code = Code;
            string apiUrl = "https://localhost:44307/api";
            string data = JsonConvert.SerializeObject(model);
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl + string.Format("/ThongTinGiangViens"), content).Result;

            TaiKhoan model1 = new TaiKhoan();
            model1.Name = model.Email;
            model1.Role = Code;
            model1.PassWord = "12345";
            string data1 = JsonConvert.SerializeObject(model1);
            HttpClient client1 = new HttpClient();
            StringContent content1 = new StringContent(data1, Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = client1.PostAsync(apiUrl + string.Format("/TaiKhoans"), content1).Result;
            return RedirectToAction("ThongTinGiangViens");
        }


        [HttpGet]
        public ActionResult CreateDuAn()
        {
            List<ThongTinGiangVien> thongTinGiangViens = new List<ThongTinGiangVien>();
            string apiUrl = "https://localhost:44307/api";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/ThongTinGiangViens")).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                thongTinGiangViens = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }
            ViewBag.TypeItemList = new MultiSelectList(thongTinGiangViens, "ID", "hoTen");
            return View();
        }
        [HttpPost]
        public ActionResult AddDunAn(DuAn model, List<int> ID)
        {
            foreach(var item in ID)
            {
                model.idThongTinGiangVien = (byte?)item;
                string apiUrl = "https://localhost:44307/api";
                string data = JsonConvert.SerializeObject(model);
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiUrl + string.Format("/DuAns"), content).Result;
            }    
            return RedirectToAction("ThongTinDuAn");
        }

        [HttpGet]
        public ActionResult CreateQTCT()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddQTCT(DuAn model)
        {
            string apiUrl = "https://localhost:44307/api";
            string data = JsonConvert.SerializeObject(model);
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl + string.Format("/QuaTrinhCongTacs"), content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ThongTinQTCT");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Thongke()
        {
            string apiUrl = "https://localhost:44307/api";
            List<ThongTinGiangVien> thongTinGiangs = new List<ThongTinGiangVien>();
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response4 = client1.GetAsync(apiUrl + string.Format("/ThongKeNam")).Result;
            if (response4.IsSuccessStatusCode)
            {
                string data = response4.Content.ReadAsStringAsync().Result;
                thongTinGiangs = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }
            List<ThongTinGiangVien> thongTinGiangss = new List<ThongTinGiangVien>();
            HttpResponseMessage response5 = client1.GetAsync(apiUrl + string.Format("/ThongKeNu")).Result;
            if (response5.IsSuccessStatusCode)
            {
                string data = response5.Content.ReadAsStringAsync().Result;
                thongTinGiangss = JsonConvert.DeserializeObject<List<ThongTinGiangVien>>(data);
            }

            ViewBag.Nam = thongTinGiangs.Count();
            ViewBag.Nu = thongTinGiangss.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Update(int ID)
        {
            string apiUrl = "https://localhost:44307/api";
            ThongTinGiangVien thongTinGiangs = new ThongTinGiangVien();
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response3 = client1.GetAsync(apiUrl + string.Format("/ThongTinGiangViens?id={0}", ID)).Result;
            if (response3.IsSuccessStatusCode)
            {
                string data = response3.Content.ReadAsStringAsync().Result;
                thongTinGiangs = JsonConvert.DeserializeObject<ThongTinGiangVien>(data);
            }
            List<ChucVu> chucVus = new List<ChucVu>();
            HttpClient client2 = new HttpClient();
            HttpResponseMessage response4 = client2.GetAsync(apiUrl + string.Format("/ChucVus")).Result;
            if (response4.IsSuccessStatusCode)
            {
                string data = response4.Content.ReadAsStringAsync().Result;
                chucVus = JsonConvert.DeserializeObject<List<ChucVu>>(data);
            }
            List<Khoa> khoas = new List<Khoa>();
            HttpResponseMessage response5 = client1.GetAsync(apiUrl + string.Format("/Khoas")).Result;
            if (response5.IsSuccessStatusCode)
            {
                string data = response5.Content.ReadAsStringAsync().Result;
                khoas = JsonConvert.DeserializeObject<List<Khoa>>(data);
            }
            ViewBag.Id = thongTinGiangs.ID;
            ViewBag.chucvu = new MultiSelectList(chucVus, "ID", "tenChucVu", chucVus.Select(t => t.ID == thongTinGiangs.idChucVu));
            ViewBag.khoa = new MultiSelectList(khoas, "ID", "Name", khoas.Select(x=>x.Id == thongTinGiangs.IdKhoa));
            return View(thongTinGiangs);
        }

        public ActionResult UpdateThongTin(ThongTinGiangVien model)
        {
            var finPost = db.ThongTinGiangViens.Find(model.ID);
            finPost.Anh = model.Anh;
            finPost.chuyenNganh = model.chuyenNganh;
            finPost.DonViCongTac = model.DonViCongTac;
            finPost.Email = model.Email;
            finPost.hoTen = model.hoTen;
            finPost.ngaySinh = model.ngaySinh;
            finPost.phongBan = model.phongBan;
            finPost.SDT = model.SDT;
            db.SaveChanges();

            return RedirectToAction("ThongTinGiangViens");
        }
    }
}