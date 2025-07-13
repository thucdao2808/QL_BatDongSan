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
    public partial class FormChuSoHuu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrvCSH();
                GenerateNextEmployeeId();
            }
        }
        public void LoadGrvCSH()
        {
            ChuSoHuuBO csh = new ChuSoHuuBO();
            var db = csh.GetDataChuSoHuu();
            grvChuSoHuu.DataSource = db;
            grvChuSoHuu.DataBind();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvChuSoHuu;

            ExportShare ex = new ExportShare();
            ex.ExportToExcelHelper(fileName, grv);

        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            GridView grv = new GridView();
            grv = grvChuSoHuu;
            ExportShare ex = new ExportShare();
            ex.ExportToExcelFromSelectedRows(fileName, grv);
        }

        protected void imgThem_Click(object sender, ImageClickEventArgs e)
        {
            pnlPopup.Visible = true;
        }

        protected void btnDong_Click(object sender, EventArgs e)
        {
            pnlPopup.Visible = false;
        }
        private void GenerateNextEmployeeId()
        {
            try
            {
                using (var context = new QL_BatDongSanEntities1())
                {
                    // Lấy mã nhân viên cuối cùng (sắp xếp giảm dần)
                    var lastEmployeeId = context.ChuSoHuus
                                                 .OrderByDescending(e => e.MaChu)
                                                 .Select(e => e.MaChu)
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
                            txtMaChu.Text = nextId;
                        }
                    }
                    else
                    {
                        // Nếu không có bản ghi nào, gán giá trị mặc định
                        txtMaChu.Text = "CSH001";
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng Chủ Sở Hữu mới và gán dữ liệu từ form
            ChuSoHuu newChuSoHuu = new ChuSoHuu
            {
                MaChu = txtMaChu.Text,
                HoTen = txtHoTen.Text,
                SDT = txtSDT.Text,
                DiaChi = txtDiaChi.Text
            };

            // Gọi phương thức Insert để lưu vào CSDL
            bool isInserted = ChuSoHuuBO.Insert(newChuSoHuu);

            // Nếu thêm thành công thì hiển thị thông báo và làm mới form
            if (isInserted)
            {
                NotifyControl.Visible = true;
                NotifyControl.ShowSuccess("Thêm thành công", "Chủ sở hữu đã được thêm!");
                pnlPopup.Visible = false;
                LoadGrvCSH();
            }

        }


        protected void igSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            using (var db = new QL_BatDongSanEntities1())
            {
                var result = db.ChuSoHuus
                               .Where(x => x.MaChu.Contains(searchText) ||
                                           x.HoTen.Contains(searchText) ||
                                           x.DiaChi.Contains(searchText))
                               .ToList();
                grvChuSoHuu.DataSource = result;
                grvChuSoHuu.DataBind();
                txtSearch.Text = string.Empty;
            }
        }

        protected void igXoa_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in grvChuSoHuu.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkExp");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvChuSoHuu.DataKeys[row.RowIndex].Value);
                    bool isDel = ChuSoHuuBO.Deleted(id);
                }

            }
            
            LoadGrvCSH();
            
            

        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            pnlSua.Visible = true;
            ImageButton btn = (ImageButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            LoadDataToEditForm(id);
        }

        protected void LoadDataToEditForm(int id)
        {
            try
            {
                using (var db = new QL_BatDongSanEntities1())
                {
                    var bds = db.ChuSoHuus.FirstOrDefault(x => x.Id == id);
                    if (bds != null)
                    {
                        pnlSua.Visible = true;

                        // Populate fields with data from the selected BDS  
                        hdChuSoHuuId.Value = bds.Id.ToString();
                        txtma.Text = bds.MaChu;
                        txtten.Text = bds.HoTen;
                        txtsdteddit.Text = bds.SDT;
                        txtDc.Text = bds.DiaChi;
                    

                    }
                  
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                
                ChuSoHuu updatedChuSoHuu = new ChuSoHuu
                {
                    Id = int.Parse(hdChuSoHuuId.Value),
                    MaChu = txtma.Text,
                    HoTen = txtten.Text,
                    SDT = txtsdteddit.Text,
                    DiaChi = txtDc.Text
                };

                // Gọi phương thức cập nhật từ tầng BO
                bool isUpdated = ChuSoHuuBO.UpdateChuSoHuu(updatedChuSoHuu);

                if (isUpdated)
                {
                    NotifyControl.Visible = true;
                    NotifyControl.ShowSuccess("Cập nhật chủ sở hữu thành công!", "Dữ liệu đã được lưu.");
                    pnlSua.Visible = false;
                    LoadGrvCSH(); 
                    
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            pnlSua.Visible = false;
        }
    }
}