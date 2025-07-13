using DataAccess.HeThong.BO;
using DataAccess.HeThong.Model;
using Main.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class FormKhachHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            KhachHangBO khachHangBO = new KhachHangBO();
            var dataKhachHang = khachHangBO.GetDataKhachHang();
            grvKhachHang.DataSource = dataKhachHang;
            grvKhachHang.DataBind();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvKhachHang;

            ExportShare ex = new ExportShare();
            ex.ExportToExcelHelper(fileName, grv);
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvKhachHang;
            ExportShare ex = new ExportShare();
            ex.ExportToExcelFromSelectedRows(fileName, grv);
        }

        private void GenerateNextEmployeeId()
        {
            try
            {
                using (var context = new QL_BatDongSanEntities1())
                {
                    // Lấy mã nhân viên cuối cùng (sắp xếp giảm dần)
                    var lastEmployeeId = context.KhachHangs
                                                 .OrderByDescending(e => e.MaKH)
                                                 .Select(e => e.MaKH)
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
                            txtMaKH.Text = nextId;
                        }
                    }
                    else
                    {
                        // Nếu không có bản ghi nào, gán giá trị mặc định
                        txtMaKH.Text = "KH001";
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

      

        protected void btnThemKH_Click(object sender, EventArgs e)
        {
            decimal tamGiaValue = 0;
            // Chuyển đổi an toàn từ string sang decimal
            if (!decimal.TryParse(txtTG.Text, out tamGiaValue))
            {
                // Xử lý nếu nhập không hợp lệ, ví dụ thông báo hoặc gán giá trị mặc định
                tamGiaValue = 0;
                // Có thể thêm thông báo lỗi cho người dùng tại đây
            }
            KhachHang newKhachHang = new KhachHang
            {

                MaKH = txtMaKH.Text,
                HoTen = txtHoTen.Text,
                SDT = txtSDT.Text,
                NhuCau = txtNhuCau.Text,
                TamGia = tamGiaValue,
                KhuVuc = txtKhuVuc.Text,
            };

            // Gọi phương thức Insert để lưu vào CSDL
            bool isInserted = KhachHangBO.Insert(newKhachHang);

            // Nếu thêm thành công thì hiển thị thông báo và làm mới form
            if (isInserted)
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowSuccess("Thêm thành công", "Khách hàng đã được thêm!");
                pnlThemKH.Visible = false;
                LoadData();
            }
            else
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowError("Thêm thất bại", "Vui lòng thử lại.");
            }
        }

        protected void btnCloseKH_Click(object sender, ImageClickEventArgs e)
        {
            pnlThemKH.Visible = false;
        }

        protected void igXoa_Click(object sender, ImageClickEventArgs e)
        {

            foreach (GridViewRow row in grvKhachHang.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkExp");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvKhachHang.DataKeys[row.RowIndex].Value);
                    bool isDel = KhachHangBO.Deleted(id);
                
                    if (isDel)
                    {
                        NotifyControl.Visible = true;
                        NotifyControl.ShowSuccess("Xóa thành công", "Khách hàng đã được xóa!");
                    }
                    else
                    {
                        NotifyControl.Visible = true;
                        NotifyControl.ShowError("Xóa thất bại", "Vui lòng thử lại.");
                    }
                }

            }

            LoadData();

        }

        protected void imgThemUser_Click(object sender, ImageClickEventArgs e)
        {
            pnlThemKH.Visible = true;
            GenerateNextEmployeeId();
        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            pnlSuaKH.Visible=true;
            ImageButton imgEdit = (ImageButton)sender;
            int id = Convert.ToInt32(imgEdit.CommandArgument);
            LoadDataToEditForm(id);
        }

        protected void LoadDataToEditForm(int id)
        {
            try
            {
                using (var db = new QL_BatDongSanEntities1())
                {
                    var bds = db.KhachHangs.FirstOrDefault(x => x.Id == id);
                    if (bds != null)
                    {
                        pnlSuaKH.Visible = true;

                        // Populate fields with data from the selected BDS  
                        hfKh.Value = bds.Id.ToString();
                        txtMaKH_Sua.Text = bds.MaKH;
                        txtHoTen_Sua.Text = bds.HoTen;
                        txtSDT_Sua.Text = bds.SDT;
                        txtNhuCau_Sua.Text = bds.NhuCau;
                        txtTG_Sua.Text = bds.TamGia.ToString();
                        txtKhuVuc_Sua.Text = bds.KhuVuc;


                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSuaKH_Click(object sender, EventArgs e)
        {
            try
            {

                KhachHang updatedChuSoHuu = new KhachHang
                {
                    Id = int.Parse(hfKh.Value),
                    MaKH = txtMaKH_Sua.Text,
                    HoTen = txtHoTen_Sua.Text,
                    SDT = txtSDT_Sua.Text,
                    NhuCau = txtNhuCau_Sua.Text,
                    TamGia = decimal.TryParse(txtTG_Sua.Text, out decimal tamGiaValue) ? tamGiaValue : 0,
                    KhuVuc = txtKhuVuc_Sua.Text
                };

                // Gọi phương thức cập nhật từ tầng BO
                bool isUpdated = KhachHangBO.UpdateKhachHang(updatedChuSoHuu);

                if (isUpdated)
                {
                    NotifyControl.Visible = true;
                    NotifyControl.ShowSuccess("Cập nhật chủ sở hữu thành công!", "Dữ liệu đã được lưu.");
                    pnlSuaKH.Visible = false;
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

        protected void btnCloseSuaKH_Click(object sender, ImageClickEventArgs e)
        {
            pnlSuaKH.Visible = false;
        }

        protected void igSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchText = search.Text.Trim();
            using (var db = new QL_BatDongSanEntities1())
            {
                var result = db.KhachHangs
                               .Where(x => x.MaKH.Contains(searchText) ||
                                           x.HoTen.Contains(searchText) ||
                                           x.KhuVuc.Contains(searchText))
                               .ToList();
                grvKhachHang.DataSource = result;
                grvKhachHang.DataBind();
                search.Text = string.Empty;
            }
        }
    }
}