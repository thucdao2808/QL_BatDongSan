using DataAccess.HeThong.Model; // EF Model
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace Main
{
    public partial class FormThongKe : System.Web.UI.Page
    {
        private QL_BatDongSanEntities1 db = new QL_BatDongSanEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilterData();
        

                txtDenNgay.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtTuNgay.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            }
        }

        private void LoadFilterData()
        {
            var nhanVienList = db.NhanViens.OrderBy(nv => nv.HoTen).ToList();
            ddlNhanVien.DataSource = nhanVienList;
            ddlNhanVien.DataTextField = "HoTen";
            ddlNhanVien.DataValueField = "Id";
            ddlNhanVien.DataBind();
            ddlNhanVien.Items.Insert(0, new ListItem("Tất cả nhân viên", "0"));

            var loaiGiaoDichList = db.GiaoDiches
                .Select(g => g.LoaiGD)
                .Distinct()
                .ToList();

            ddlLoaiGD.DataSource = loaiGiaoDichList;
            ddlLoaiGD.DataBind();
            ddlLoaiGD.Items.Insert(0, new ListItem("Tất cả loại giao dịch", ""));
        }

        protected void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = DateTime.Parse(txtTuNgay.Text);
            DateTime denNgay = DateTime.Parse(txtDenNgay.Text);
            int nhanVienId = int.Parse(ddlNhanVien.SelectedValue);
            string loaiGD = ddlLoaiGD.SelectedValue;

            var giaoDichQuery = db.GiaoDiches
                .Include("BD")
                .Include("NhanVien")
                .Include("KhachHang")
                .Where(gd => gd.NgayGD >= tuNgay && gd.NgayGD <= denNgay);

            if (nhanVienId > 0)
                giaoDichQuery = giaoDichQuery.Where(gd => gd.NhanVienId == nhanVienId);

            if (!string.IsNullOrEmpty(loaiGD))
                giaoDichQuery = giaoDichQuery.Where(gd => gd.LoaiGD == loaiGD);

            // Tổng doanh thu
            decimal tongDoanhThu = giaoDichQuery.Any()
                ? giaoDichQuery.Sum(gd => gd.GiaTri).GetValueOrDefault()
                : 0;
            lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0") + " VNĐ";

            // Số giao dịch
            int soGD = giaoDichQuery.Count();
            lblTongSoGiaoDich.Text = "Số giao dịch: " + soGD;

            // Số bất động sản
            int soBDS = giaoDichQuery.Select(gd => gd.BDSId).Distinct().Count();
            lblBdsDaGiaoDich.Text = "Số BĐS đã giao dịch: " + soBDS;

            // Chi tiết giao dịch
            var chiTiet = giaoDichQuery.Select(gd => new
            {
                gd.Id,
                gd.MaGD,
                gd.NgayGD,
                gd.GiaTri,
                gd.LoaiGD,
                gd.GhiChu,
                TenNhanVien = gd.NhanVien.HoTen,
                TenKhachHang = gd.KhachHang.HoTen
            }).ToList();

            gvGiaoDich.DataSource = chiTiet;
            gvGiaoDich.DataBind();
            LoadDoanhThuTheoNhanVien();
            LoadTyTrongTheoLoaiBDS();
            pnlKetQua.Visible = true;
        }

        private void LoadDoanhThuTheoNhanVien()
        {
            var data = db.GiaoDiches
                .Where(gd => gd.NhanVienId != null && gd.GiaTri != null)
                .GroupBy(gd => gd.NhanVien.HoTen)
                .Select(g => new
                {
                    TenNhanVien = g.Key,
                    TongDoanhThu = g.Sum(x => x.GiaTri) ?? 0
                })
                .ToList();

            // Đảm bảo có series
            if (!chartDoanhThuNhanVien.Series.Any(s => s.Name == "DoanhThu"))
            {
                chartDoanhThuNhanVien.Series.Add(new Series("DoanhThu")
                {
                    ChartType = SeriesChartType.Column
                });
            }

            var series = chartDoanhThuNhanVien.Series["DoanhThu"];
            series.Points.Clear();

            foreach (var item in data)
            {
                series.Points.AddXY(item.TenNhanVien, item.TongDoanhThu);
            }

            chartDoanhThuNhanVien.ChartAreas[0].AxisX.Interval = 1;
        }

        private void LoadTyTrongTheoLoaiBDS()
        {
            var data = db.GiaoDiches
                .Where(gd => gd.BD != null && gd.GiaTri != null)
                .GroupBy(gd => gd.BD.Loai)
                .Select(g => new
                {
                    Loai = g.Key,
                    TongGiaTri = g.Sum(x => x.GiaTri) ?? 0
                })
                .ToList();

            if (!chartTyTrongBDS.Series.Any(s => s.Name == "TyTrong"))
            {
                chartTyTrongBDS.Series.Add(new Series("TyTrong")
                {
                    ChartType = SeriesChartType.Pie
                });
            }

            var series = chartTyTrongBDS.Series["TyTrong"];
            series.Points.Clear();

            foreach (var item in data)
            {
                series.Points.AddXY(item.Loai, item.TongGiaTri);
            }
        }
    }
}
