using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DATNMVC.Models;
using Newtonsoft.Json;

namespace DATNMVC.Areas.Area.Controllers
{
    public class UserController : Controller
    {
        // GET: Area/User
        public ActionResult Index()
        {
            return View();
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