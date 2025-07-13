using DataAccess.HeThong.BO;
using DataAccess.HeThong.Model;
using DocumentFormat.OpenXml.Office2010.Drawing;
using Main.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class FormNhanVien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            DataAccess.HeThong.BO.NhanVienBO nhanVienBO = new DataAccess.HeThong.BO.NhanVienBO();
            var dataNhanVien = nhanVienBO.GetDataNhanVien();
            grvNhanVien.DataSource = dataNhanVien;
            grvNhanVien.DataBind();
        }

        protected void imgThem_Click(object sender, ImageClickEventArgs e)
        {
            pnlPopup.Visible = true;
            GenerateNextEmployeeId();
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            pnlPopup.Visible = false;
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            NhanVien NhanVien = new NhanVien
            {

                MaNV = txtMaNV.Text,
                HoTen = txtHoTen.Text,
                SDT = txtSDT.Text,
                PhongBan = txtPhongBan.Text,
            
            };

            // Gọi phương thức Insert để lưu vào CSDL
            bool isInserted = NhanVienBO.Insert(NhanVien);

            // Nếu thêm thành công thì hiển thị thông báo và làm mới form
            if (isInserted)
            {
                pnlPopup.Visible = false;
                LoadData();
                NotifyControl.Visible = true;
                NotifyControl.ShowSuccess("Chúc mừng bạn đã thêm thành công","Dữ liệu đã được thêm");
            }
            else
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowError("Thêm nhân viên thất bại", "Vui lòng thử lại.");
            }
        }
        private void GenerateNextEmployeeId()
        {
            try
            {
                using (var context = new QL_BatDongSanEntities1())
                {
                    // Lấy mã nhân viên cuối cùng (sắp xếp giảm dần)
                    var lastEmployeeId = context.NhanViens
                                                 .OrderByDescending(e => e.MaNV)
                                                 .Select(e => e.MaNV)
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
                            txtMaNV.Text = nextId;
                        }
                    }
                    else
                    {
                        // Nếu không có bản ghi nào, gán giá trị mặc định
                        txtMaNV.Text = "NV001";
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void imgXoa_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in grvNhanVien.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkExp");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvNhanVien.DataKeys[row.RowIndex].Value);
                    bool isDel = NhanVienBO.Deleted(id);
                 
                    if (isDel)
                    {
                        NotifyControl.Visible = true;
                        NotifyControl.ShowSuccess("Xóa nhân viên thành công!", "Dữ liệu đã được xóa.");
                    }
                    else
                    {
                        NotifyControl.Visible = true;
                        NotifyControl.ShowError("Xóa nhân viên thất bại.", "Vui lòng thử lại.");
                    }
                }

            }

            LoadData();
        }

        protected void LoadDataToEditForm(int id)
        {
            try
            {
                using (var db = new QL_BatDongSanEntities1())
                {
                    var bds = db.NhanViens.FirstOrDefault(x => x.Id == id);
                    if (bds != null)
                    {
                        pnlEditPopup.Visible = true;

                        // Populate fields with data from the selected BDS  
                        hfEditId.Value = bds.Id.ToString();
                        txtEditMaNV.Text = bds.MaNV;
                        txtEditHoTen.Text = bds.HoTen;
                        txtEditPhongBan.Text = bds.PhongBan;
                        txtEditSDT.Text = bds.SDT;
             


                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            pnlEditPopup.Visible = true;
            ImageButton imgEdit = (ImageButton)sender;
            int id = Convert.ToInt32(imgEdit.CommandArgument);
            LoadDataToEditForm(id);
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {

                NhanVien updatedChuSoHuu = new NhanVien
                {
                    Id = int.Parse(hfEditId.Value),
                    MaNV = txtEditMaNV.Text,
                    HoTen = txtEditHoTen.Text,
                    SDT = txtEditSDT.Text,
                    PhongBan = txtEditPhongBan.Text
                };

                // Gọi phương thức cập nhật từ tầng BO
                bool isUpdated = NhanVienBO.UpdateNhanVien(updatedChuSoHuu);

                if (isUpdated)
                {
                    NotifyControl.Visible = true;
                    NotifyControl.ShowSuccess("Cập nhật chủ sở hữu thành công!", "Dữ liệu đã được lưu.");
                    pnlEditPopup.Visible = false;
                    LoadData();

                }
                else
                {
                    NotifyControl.Visible = true;
                    NotifyControl.ShowError("Cập nhật chủ sở hữu thất bại.", "Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowError("Đã xảy ra lỗi.", ex.Message);
            }
        }

        protected void btnHuyEdit_Click(object sender, EventArgs e)
        {
            pnlEditPopup.Visible = false;
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvNhanVien;

            ExportShare ex = new ExportShare();
            ex.ExportToExcelHelper(fileName, grv);
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvNhanVien;
            ExportShare ex = new ExportShare();
            ex.ExportToExcelFromSelectedRows(fileName, grv);
        }

        protected void igSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchText = search.Text.Trim();
            using (var db = new QL_BatDongSanEntities1())
            {
                var result = db.NhanViens
                               .Where(x => x.MaNV.Contains(searchText) ||
                                           x.PhongBan.Contains(searchText))
                               .ToList();
                grvNhanVien.DataSource = result;
                grvNhanVien.DataBind();
                search.Text = string.Empty;
            }
        }
    }
}