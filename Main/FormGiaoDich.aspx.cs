using DataAccess.HeThong.BO;
using DataAccess.HeThong.Model;
using Main.WebHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class FormGiaoDdich : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadFormThem();
            }

        }
        public void LoadData()
        {
            DataAccess.HeThong.BO.GiaoDichBO giaoDichBO = new DataAccess.HeThong.BO.GiaoDichBO();
            var dataGiaoDich = giaoDichBO.GetDataGiaoDich();
            grvGiaoDich.DataSource = dataGiaoDich;
            grvGiaoDich.DataBind();
        }

        protected void igSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchText = search.Text.Trim();
            using (var db = new QL_BatDongSanEntities1())
            {
                var result = db.GiaoDiches
                               .Where(x => x.MaGD.Contains(searchText) ||
                                           x.LoaiGD.Contains(searchText))
                               .ToList();
                grvGiaoDich.DataSource = result;
                grvGiaoDich.DataBind();
                search.Text = string.Empty;
            }
        }

        public string GetTenBDs(int BdsId)
        {
            using (QL_BatDongSanEntities1 context = new QL_BatDongSanEntities1())
            {
                var tenBDS = context.BDS
                           .Where(b => b.Id == BdsId)
                           .Select(b => b.MaBDS)
                           .FirstOrDefault();

                return tenBDS ?? "Không tìm thấy";
            }
        }

        public string GetTenKhachHang(int KhachId)
        {

            using (QL_BatDongSanEntities1 qr = new QL_BatDongSanEntities1())
            {
                var tenKh = qr.KhachHangs.Where(x => x.Id == KhachId).Select(x => x.MaKH).FirstOrDefault();
                return tenKh ?? "Không tìm thấy";
            }
        }

        public string GetTenNhanVien(int NhanVienId)
        {
            using (QL_BatDongSanEntities1 context = new QL_BatDongSanEntities1())
            {
                var tenNV = context.NhanViens
                           .Where(nv => nv.Id == NhanVienId)
                           .Select(nv => nv.MaNV)
                           .FirstOrDefault();

                return tenNV ?? "Không tìm thấy";
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvGiaoDich;

            ExportShare ex = new ExportShare();
            ex.ExportToExcelHelper(fileName, grv);
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvGiaoDich;
            ExportShare ex = new ExportShare();
            ex.ExportToExcelFromSelectedRows(fileName, grv);
        }

        protected void igXoa_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in grvGiaoDich.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkExp");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvGiaoDich.DataKeys[row.RowIndex].Value);
                    bool isDel = NhanVienBO.Deleted(id);

                    if (isDel)
                    {
                        NotifyControl.Visible = true;
                        NotifyControl.ShowSuccess("Xóa Giao dịch thành công!", "Dữ liệu đã được xóa.");
                    }
                    else
                    {
                        NotifyControl.Visible = true;
                        NotifyControl.ShowError("Xóa Giao Dịch thất bại.", "Vui lòng thử lại.");
                    }
                }

            }

            LoadData();
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
           
            DateTime ngayGD;
            bool isNgayGDValid = DateTime.TryParseExact(
                txtNgayGD.Text.Trim(),
                "dd/MM/yyyy",          
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out ngayGD);

            if (!isNgayGDValid)
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowError("Ngày giao dịch không hợp lệ.", "Vui lòng nhập đúng định dạng ngày (dd/MM/yyyy).");
                return; 
            }

         
            decimal giaTri;
            bool isGiaTriValid = decimal.TryParse(txtGiaTri.Text.Trim(), out giaTri);
            if (!isGiaTriValid)
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowError("Giá trị không hợp lệ.", "Vui lòng nhập giá trị tiền hợp lệ.");
                return; 
            }

           
            GiaoDich newGiaoDich = new GiaoDich
            {
                MaGD = txtMaGD.Text.Trim(),
                BDSId = int.Parse(ddlBDS.SelectedValue),
                KhachHangId = int.Parse(ddlKhachHang.SelectedValue),
                NhanVienId = int.Parse(ddlNhanVien.SelectedValue),
                NgayGD = ngayGD,
                LoaiGD = txtLoaiGD.Text.Trim(),
                GiaTri = giaTri,
                GhiChu = txtGhiChu.Text.Trim()
            };

            bool isInserted = GiaoDichBO.Insert(newGiaoDich);

            if (isInserted)
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowSuccess("Thêm thành công", "Giao Dịch đã được thêm!");
                pnlThemGiaoDich.Visible = false;
                LoadData();
            }
            else
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowError("Thêm Giao Dịch thất bại.", "Vui lòng thử lại.");
            }
        }

        public List<BDS> GetBDs()
        {
            using (QL_BatDongSanEntities1 db = new QL_BatDongSanEntities1())
            {
                var bdsList = db.BDS
            .GroupBy(x => x.MaBDS)         // Nhóm theo MaBDS
            .Select(g => g.FirstOrDefault()) // Lấy 1 dòng đại diện
            .ToList();
                return bdsList;
            }
        }
        public List<KhachHang> GetKhachHang()
        {
            using (QL_BatDongSanEntities1 db = new QL_BatDongSanEntities1())
            {
                return db.KhachHangs
                    .GroupBy(x => x.HoTen)
                    .Select(g => g.FirstOrDefault())
                    .ToList();
            }
        }
        public List<NhanVien> GetNhanVien()
        {
            using (QL_BatDongSanEntities1 db = new QL_BatDongSanEntities1())
            {
                return db.NhanViens
                    .GroupBy(x => x.HoTen)
                    .Select(g => g.FirstOrDefault())
                    .ToList();
            }
        }
        public void LoadFormThem()
        {
            using (QL_BatDongSanEntities1 db = new QL_BatDongSanEntities1())
            {
                ddlBDS.DataSource = GetBDs();
                ddlBDS.DataTextField = "MaBDS";
                ddlBDS.DataValueField = "Id";
                ddlBDS.DataBind();

                ddlKhachHang.DataSource = GetKhachHang();
                ddlKhachHang.DataTextField = "HoTen";
                ddlKhachHang.DataValueField = "Id";
                ddlKhachHang.DataBind();

                ddlNhanVien.DataSource = GetNhanVien();
                ddlNhanVien.DataTextField = "HoTen";
                ddlNhanVien.DataValueField = "Id";
                ddlNhanVien.DataBind();

            }
        }

        protected void imgThem_Click(object sender, ImageClickEventArgs e)
        {
            pnlThemGiaoDich.Visible = true;
            GenerateNextEmployeeId();
        }
        private void GenerateNextEmployeeId()
        {
            try
            {
                using (var context = new QL_BatDongSanEntities1())
                {
                    // Lấy mã nhân viên cuối cùng (sắp xếp giảm dần)
                    var lastEmployeeId = context.GiaoDiches
                                                 .OrderByDescending(e => e.MaGD)
                                                 .Select(e => e.MaGD)
                                                 .FirstOrDefault();

                    if (lastEmployeeId != null)
                    {
                        // Tách phần chữ và số từ mã nhân viên cuối cùng
                        string prefix = new string(lastEmployeeId.TakeWhile(char.IsLetter).ToArray());
                        string numericPart = new string(lastEmployeeId.SkipWhile(char.IsLetter).ToArray());

                        if (int.TryParse(numericPart, out int number))
                        {
                            // Tạo mã nhân viên mới
                            string nextId = prefix + (number + 1).ToString("D3");
                            txtMaGD.Text = nextId;
                        }
                    }
                    else
                    {
                        // Nếu không có bản ghi nào, gán giá trị mặc định
                        txtMaGD.Text = "GD001";
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            pnlThemGiaoDich.Visible = false;
        }
    }
}