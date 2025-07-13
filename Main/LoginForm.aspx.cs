using DataAccess.HeThong.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{

    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDowlist();
        
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
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string strUsername = username.Text.Trim();
            string strPassword = password.Text.Trim();
            string strCaptcha = txtCaptchaInput.Text.Trim();
            string sessionCaptcha = Session["CaptchaCode"] as string;
            bool isValid = true;

            // Validate: Username  
            if (string.IsNullOrEmpty(strUsername))
            {
                lblErrUserName.Visible = true;
                lblErrUserName.Text = "Tên đăng nhập không được để trống";
                isValid = false;
            }
            else
            {
                lblErrUserName.Visible = false;
            }

            // Validate: Password  
            if (string.IsNullOrEmpty(strPassword))
            {
                lblErrPassword.Visible = true;
                lblErrPassword.Text = "Mật khẩu không được để trống";
                isValid = false;
            }
            else if (strPassword.Length <= 8)
            {
                lblErrPassword.Visible = true;
                lblErrPassword.Text = "Mật khẩu phải lớn hơn 8 kí tự";
                isValid = false;
            }
            else
            {
                lblErrPassword.Visible = false;
            }

            // Validate: Captcha  
            if (string.IsNullOrEmpty(strCaptcha))
            {
                lblErrCaptcha.Visible = true;
                lblErrCaptcha.Text = "Mã captcha không được để trống";
                isValid = false;
            }
            else if (strCaptcha != sessionCaptcha)
            {
                lblErrCaptcha.Visible = true;
                lblErrCaptcha.Text = "Mã captcha không đúng";
                isValid = false;
            }
            else
            {
                lblErrCaptcha.Visible = false;
            }

            // Nếu không có lỗi thì tiếp tục truy vấn DB  
            if (isValid)
            {
                string hashPassword = WebHelper.LoginAcc.HashPassword(strPassword);

                using (QL_BatDongSanEntities1 ht = new QL_BatDongSanEntities1())
                {
                    var user = ht.TaiKhoanDangNhaps.FirstOrDefault(tk => tk.TenDangNhap == strUsername);

                    if (user == null)
                    {
                        lblErrUserName.Visible = true;
                        lblErrUserName.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                    else if (user != null && user.MatKhau == WebHelper.LoginAcc.HashPassword(strPassword))
                    {
                        if (user.VaiTro == "Nhân Viên")
                        {
                            Session["UserRole"] = "Nhân Viên";
                            //Session["DangNhapThanhCong"] = true;
                           SpinnerLoading.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", @"
         setTimeout(function() {
             window.location.href = 'FormBDS.aspx';
         }, 3000);
     ", true);
                        }
                        else if (user.VaiTro == "Admin")
                        {
                            Session["UserRole"] = "Admin";
                            //Session["DangNhapThanhCong"] = true;
                            SpinnerLoading.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", @"
         setTimeout(function() {
             window.location.href = 'FormBDS.aspx';
         }, 3000);
     ", true);
                        }
                        else
                        {
                            lblErrUserName.Visible = true;
                            lblErrUserName.Text = "Bạn không có quyền đăng nhập";
                        }
                    }
                }
            }

        }

       
    }
}