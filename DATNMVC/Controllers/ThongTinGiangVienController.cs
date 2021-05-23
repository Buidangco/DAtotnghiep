using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        public ActionResult AddGiangVien(ThongTinGiangVien model)
        {
            string apiUrl = "https://localhost:44307/api";
            string data = JsonConvert.SerializeObject(model);
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl + string.Format("/ThongTinGiangViens"), content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
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
            //if (response.IsSuccessStatusCode)
            //{
                return RedirectToAction("ThongTinDuAn");
            //}
            //return View();
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
    }
}