using DATNMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DATNMVC.Controllers
{
    public class TaiKhoanController : Controller
    {
        DA_TOTNGHIEPEntities3 db = new DA_TOTNGHIEPEntities3();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap(TaiKhoan taiKhoan)
        {
            int taiKhoanNumber = db.TaiKhoans.Where(x => x.Name == taiKhoan.Name && x.PassWord == taiKhoan.PassWord).Count();
            TaiKhoan taiKhoan1 = db.TaiKhoans.Where(x => x.Name == taiKhoan.Name && x.PassWord == taiKhoan.PassWord).FirstOrDefault();
            Session["Taikhoan"] = taiKhoan1;
            if (taiKhoanNumber > 0)
            {
                return Redirect("/ThongTinGiangVien/ThongTinGiangViens");
            }    
            else
            {
                return RedirectToAction("Index");
            }    
        }
    }
}