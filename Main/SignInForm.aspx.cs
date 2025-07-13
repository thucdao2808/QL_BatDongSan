using DataAccess.HeThong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class SignInForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDowlist();
                GenerateNextEmployeeId();
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
                            txtMaNhanVien.Text = nextId;
                        }
                    }
                    else
                    {
                        // Nếu không có bản ghi nào, gán giá trị mặc định
                        txtMaNhanVien.Text = "NV001";
                    }
                }
            }
            catch (Exception ex)
            {
                
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
        public List<object> GetVaiTro()
        {
            using (QL_BatDongSanEntities1 ht = new QL_BatDongSanEntities1())
            {

                return ht.TaiKhoanDangNhaps
          .GroupBy(tk => tk.VaiTro)
          .Select(group => group.FirstOrDefault())
          .ToList<object>();
            }
        }
        public void LoadDropDowlist()
        {
            ddlPhanQuyen.DataSource = GetVaiTro();
            ddlPhanQuyen.DataTextField = "VaiTro";
            ddlPhanQuyen.DataValueField = "ID";
            ddlPhanQuyen.DataBind();
        }

        public void createAcc(string tendangnhap, string matkhau)
        {
            string hashPassword = WebHelper.LoginAcc.HashPassword(matkhau);
            using (QL_BatDongSanEntities1 ht = new QL_BatDongSanEntities1())
            {
                // Add to TaiKhoanDangNhap table  
                var newAcc = new TaiKhoanDangNhap
                {
                    TenDangNhap = tendangnhap,
                    MatKhau = hashPassword,
                    VaiTro = ddlPhanQuyen.SelectedValue
                };
                ht.TaiKhoanDangNhaps.Add(newAcc);

                // Add to another table (e.g., UserProfile)  
                var newProfile = new NhanVien
                {
                    
                };
                ht.NhanViens.Add(newProfile);

                // Save changes to both tables  
                ht.SaveChanges();
            }

            // Display success message  
            lblMessage.Text = "Đăng ký tài khoản và hồ sơ thành công!";
            lblMessage.CssClass = "success-message";
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string hoVaTen = txtHovaTen.Text.Trim();
            string soDienThoai = txtSodienthoai.Text.Trim();
            string phongBan = txtPhongban.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtPassword.Text.Trim();
            string phanQuyen = ddlPhanQuyen.SelectedValue;

            try
            {
                using (var context = new QL_BatDongSanEntities1())
                {
                    // Insert into NhanVien table  
                    var nhanVien = new NhanVien
                    {
                        MaNV = maNhanVien,
                        HoTen = hoVaTen,
                        SDT = soDienThoai,
                        PhongBan = phongBan
                    };
                    context.NhanViens.Add(nhanVien);
                    context.SaveChanges(); // Save to generate ID for NhanVien  

                    // Insert into TaiKhoanDangNhap table  
                    var taiKhoan = new TaiKhoanDangNhap
                    {
                        TenDangNhap = email,
                        MatKhau = WebHelper.LoginAcc.HashPassword(matKhau),
                        VaiTro = phanQuyen,
                        NhanVienId = nhanVien.Id // Use the generated ID from NhanVien  
                    };
                    context.TaiKhoanDangNhaps.Add(taiKhoan);

                    // Save changes to the database  
                    context.SaveChanges();
                }

                lblMessage.Text = "Sign-in information saved successfully.";
                lblMessage.CssClass = "success-message";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "error-message";
            }
        }
    }
}