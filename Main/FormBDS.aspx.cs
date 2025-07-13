using DataAccess.HeThong.BO;
using DataAccess.HeThong.Model;
using DocumentFormat.OpenXml.Spreadsheet;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main.WebHelper;
using Main.WebControl;
using Irony;
using System.IO;

namespace Main
{
    public partial class RoleUser : System.Web.UI.Page
    {

        private QL_BatDongSanEntities1 ht = new QL_BatDongSanEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindDataForm();
                BindChuSoHuuToDropdown();
                GenerateNextEmployeeId();

                LoadChuSoHuu();
                LoadTinhTrang();
                //if (Session["DangNhapThanhCong"] != null && (bool)Session["DangNhapThanhCong"])
                //{

                //   {
                //      NotifyControl.ShowSuccess("Đăng nhập thành công", "Chào mừng bạn đến với phần mềm của tôi!");
                //      Session.Remove("DangNhapThanhCong");

                //  }


                //}
            }
            
        }
        public void BindDataForm()
        {
            BDSBO bds = new BDSBO();
            var dataBDS = bds.GetDataBDS();
            if (dataBDS != null && dataBDS.Count > 0)
            {
                grvFormBDS.DataSource = dataBDS;
                grvFormBDS.DataBind();
            }

        }
        public string LoadChuSoHuuName(int chuSoHuuId)
        {
            var chuSoHuu = ht.ChuSoHuus.FirstOrDefault(x => x.Id == chuSoHuuId);
            if (chuSoHuu != null)
            {
                return chuSoHuu.HoTen;
            }
            return "Chưa có chủ sở hữu";
        }
        public string GetChuSoHuuNameByBDSId(int bdsId)
        {
            var bds = ht.BDS.FirstOrDefault(x => x.Id == bdsId);
            if (bds != null && bds.ChuSoHuuId.HasValue)
            {
                var chuSoHuu = ht.ChuSoHuus.FirstOrDefault(x => x.Id == bds.ChuSoHuuId.Value);
                if (chuSoHuu != null)
                {
                    return chuSoHuu.HoTen;
                }
            }
            return null;
        }
        private void GenerateNextEmployeeId()
        {
            try
            {
                using (var context = new QL_BatDongSanEntities1())
                {
                    // Lấy mã nhân viên cuối cùng (sắp xếp giảm dần)
                    var lastEmployeeId = context.BDS
                                                 .OrderByDescending(e => e.MaBDS)
                                                 .Select(e => e.MaBDS)
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
                            txtMaBDS.Text = nextId;
                        }
                    }
                    else
                    {
                        // Nếu không có bản ghi nào, gán giá trị mặc định
                        txtMaBDS.Text = "BDS001";
                    }
                }
            }
            catch (Exception ex)
            {

               
            }
        }
        public List<ChuSoHuu> GetChuSoHuuList()
        {
            return ht.ChuSoHuus.ToList();
        }

        protected void BindChuSoHuuToDropdown()
        {
            var chuSoHuuList = GetChuSoHuuList();
            if (chuSoHuuList != null && chuSoHuuList.Count > 0)
            {
                ddlChuSoHuu.DataSource = chuSoHuuList;
                ddlChuSoHuu.DataTextField = "HoTen";
                ddlChuSoHuu.DataValueField = "Id";
                ddlChuSoHuu.DataBind();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            pnlThemBDS.Visible = false;
        }

        protected void imgThem_Click(object sender, ImageClickEventArgs e)
        {
            pnlThemBDS.Visible = true;
        }
        protected void FileUpload1_OnChanged(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                // Lấy tên tệp tin từ FileUpload
                string fileName = Path.GetFileName(FileUpload1.FileName);

                // Đường dẫn lưu ảnh trên máy chủ
                string destinationPath = Server.MapPath("~/UploadedImages/" + fileName);

                // Lưu tệp tin vào thư mục trên máy chủ
                FileUpload1.SaveAs(destinationPath);

                // Hiển thị ảnh trên form
                imgPreview.ImageUrl = "~/UploadedImages/" + fileName;
                imgPreview.Visible = true;
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
          

            // Create a new BDS object and populate its properties  
            BDS newBDS = new BDS
            {
                MaBDS = txtMaBDS.Text,
                Loai = txtLoai.Text,
                DiaChi = txtDiaChi.Text,
                Gia = string.IsNullOrEmpty(txtGia.Text) ? (decimal?)null : decimal.Parse(txtGia.Text),
                DienTich = string.IsNullOrEmpty(txtDienTich.Text) ? (double?)null : double.Parse(txtDienTich.Text),
                TinhTrang = ddlTinhTrang.SelectedValue,
                ChuSoHuuId = string.IsNullOrEmpty(ddlChuSoHuu.SelectedValue) ? (int?)null : int.Parse(ddlChuSoHuu.SelectedValue),
                HinhAnhBDS = imgPreview.ImageUrl
            };


            bool isInserted = BDSBO.Insert(newBDS);

            if (isInserted)
            {
                NotifyControl.ShowSuccess("Thêm bất động sản thành công!", "Dữ liệu đã được lưu.");
                BindDataForm();
                pnlThemBDS.Visible = false;
            }
         
        }

        protected void imgXem_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            LoadChiTietBDS(id);
        }
        private void LoadChiTietBDS(int id)
        {
            using (var db = new QL_BatDongSanEntities1())
            {
                var bds = db.BDS
                            .Include("ChuSoHuu") // sử dụng chuỗi tên navigation property  
                            .FirstOrDefault(x => x.Id == id);

                if (bds != null)
                {
                    pnlXemChiTietBDS.Visible = true;

                    IdBDS.Value = bds.Id.ToString();
                    lblMaBDS.Text = bds.MaBDS;
                    lblLoai.Text = bds.Loai;
                    lblDiaChi.Text = bds.DiaChi;
                    lblGia.Text = bds.Gia.ToString() + " VNĐ";
                    lblDienTich.Text = bds.DienTich + " m²";
                    lblTinhTrang.Text = bds.TinhTrang;

                    // Thông tin chủ sở hữu từ bảng liên kết  
                    lblChuSoHuu.Text = bds.ChuSoHuu?.HoTen ?? "Không rõ";
                    lblHoten.Text = "Chủ sở hữu: " + (bds.ChuSoHuu?.HoTen ?? "N/A");
                    lblSoDienThoai.Text = "SDT: " + (bds.ChuSoHuu?.SDT ?? "N/A");


                    Image1.ImageUrl = string.IsNullOrEmpty(bds.HinhAnhBDS)
                        ? ResolveUrl("~/UploadedImages/default-image.jpg")
                        : (bds.HinhAnhBDS.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                            ? bds.HinhAnhBDS
                            : ResolveUrl("~/UploadedImages/" + bds.HinhAnhBDS));


                }
                else
                {
                    NotifyControl.Visible = true;
                    NotifyControl.ShowError("Không tìm thấy bất động sản.", "no find");
                }
            }
        }


        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            pnlXemChiTietBDS.Visible = false;
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvFormBDS;

            ExportShare ex = new ExportShare();
            ex.ExportToExcelHelper(fileName, grv);

        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvFormBDS;

            ExportShare ex = new ExportShare();
            ex.ExportToExcelFromSelectedRows(fileName, grv);
        }

        protected void igSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchText = search.Text.Trim();
            using (var db = new QL_BatDongSanEntities1())
            {
                var result = db.BDS
                               .Where(x => x.MaBDS.Contains(searchText) ||
                                           x.Loai.Contains(searchText) ||
                                           x.DiaChi.Contains(searchText))
                               .ToList();
                grvFormBDS.DataSource = result;
                grvFormBDS.DataBind();
                search.Text = string.Empty;
            }
        }

        protected void igXoa_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in grvFormBDS.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkExp");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvFormBDS.DataKeys[row.RowIndex].Value);
                    bool isDel = BDSBO.Deleted(id);
                }

            }
            BindDataForm();
        }

 


        protected void LoadDataToEditForm(int id)
        {
            try
            {
                using (var db = new QL_BatDongSanEntities1())
                {
                    var bds = db.BDS.FirstOrDefault(x => x.Id == id);
                    if (bds != null)
                    {
                        pnlSuaBDS.Visible = true;

                        // Populate fields with data from the selected BDS  
                        hfBDS.Value = bds.Id.ToString();
                        txtMaBDSedit.Text = bds.MaBDS;
                        txtLoaiedit.Text = bds.Loai;
                        txtDiaChiedit.Text = bds.DiaChi;
                        txtGiaedit.Text = bds.Gia?.ToString();
                        txtDienTichedit.Text = bds.DienTich?.ToString();
                    

                        
                        Image2.ImageUrl = string.IsNullOrEmpty(bds.HinhAnhBDS)
                            ? ResolveUrl("~/UploadedImages/default-image.jpg")
                            : (bds.HinhAnhBDS.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                                ? bds.HinhAnhBDS
                                : ResolveUrl("~/UploadedImages/" + bds.HinhAnhBDS));
                        imgPreview.Visible = true;
                    }
                    else
                    {
                        NotifyControl.ShowError("Không tìm thấy bất động sản.", "Vui lòng thử lại.");
                    }
                }
            }
            catch (Exception ex)
            {
                NotifyControl.ShowError("Đã xảy ra lỗi.", ex.Message);
            }
        }

        private void LoadChuSoHuu()
        {
            using (var db = new QL_BatDongSanEntities1())
            {
                var list = db.ChuSoHuus
                      .Select(x => new { x.Id, x.HoTen }) 
                      .Distinct()
                      .ToList();

                ddlChuSoHuuedit.DataSource = list;
                ddlChuSoHuuedit.DataTextField = "HoTen";     
                ddlChuSoHuuedit.DataValueField = "Id";        
                ddlChuSoHuuedit.DataBind();


            }
        }
        private void LoadTinhTrang()
        {
            using (var db = new QL_BatDongSanEntities1())
            {
                var list = db.BDS.ToList();
                ddlTinhTrangedit.DataSource = list;
                ddlTinhTrangedit.DataTextField = "TinhTrang";
                ddlTinhTrangedit.DataValueField = "TinhTrang";
                ddlTinhTrangedit.DataBind();
            }
        }
        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            pnlSuaBDS.Visible = true;
            ImageButton btn = (ImageButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            LoadDataToEditForm(id);
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            pnlSuaBDS.Visible =false;
        }

        protected void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new BDS object and populate its properties with updated values  
                BDS updatedBDS = new BDS
                {
                    
                    Id = int.Parse(hfBDS.Value),
                    MaBDS = txtMaBDSedit.Text,
                    Loai = txtLoaiedit.Text,
                    DiaChi = txtDiaChiedit.Text,
                    Gia = string.IsNullOrEmpty(txtGiaedit.Text) ? (decimal?)null : decimal.Parse(txtGiaedit.Text),
                    DienTich = string.IsNullOrEmpty(txtDienTichedit.Text) ? (double?)null : double.Parse(txtDienTichedit.Text),
                    TinhTrang = ddlTinhTrangedit.SelectedItem.Text,
                    ChuSoHuuId = string.IsNullOrEmpty(ddlChuSoHuuedit.SelectedItem.Text) ? (int?)null : int.Parse(ddlChuSoHuuedit.SelectedValue),
                    HinhAnhBDS = Image2.ImageUrl
                };

                // Call the UpdateBDS method from the BO layer  
                bool isUpdated = BDSBO.UpdateBDS(updatedBDS);

                if (isUpdated)
                {
                    NotifyControl.ShowSuccess("Cập nhật bất động sản thành công!", "Dữ liệu đã được lưu.");
                    BindDataForm();
                    pnlSuaBDS.Visible = false;
                }
                else
                {
                    NotifyControl.ShowError("Cập nhật bất động sản thất bại.", "Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                NotifyControl.ShowError("Đã xảy ra lỗi.", ex.Message);
            }
        }
    }
}
