using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATNMVC.Models
{
    public class ThongTin
    {
        public ThongTinGiangVien thongTinGiangVien { get; set; }
        public List<DuAn> DuAns { get; set; }
        public List<QuaTrinhCongTac> QuaTrinhCongTacs { get; set; }

        public List<QuaTrinhDaoTao> QuaTrinhDaos { get; set; }
    }
}