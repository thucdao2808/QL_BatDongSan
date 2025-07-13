using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class Navigation : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in and retrieve their role from session  
            string userRole = Session["UserRole"] as string;

            if (string.IsNullOrEmpty(userRole))
            {
                // Redirect to login page if no role is found  
                Response.Redirect("~/LoginForm.aspx");
            }

            // Show/Hide menu items based on user role  
            if (userRole == "Admin")
            {
                formBatDongSan.Visible = true;
                formChuSoHuu.Visible = true;
                formKh.Visible = true;
                FormGiaoDich.Visible = true;
                formNV.Visible = true;
                formLogout.Visible = true;
                formReport.Visible = true;
            }
            else if (userRole == "Nhân Viên")
            {
                formBatDongSan.Visible = true;
                formChuSoHuu.Visible = true;
                formKh.Visible = true;
                FormGiaoDich.Visible = false;
                formNV.Visible =false;
                formLogout.Visible = true;
                formReport.Visible = true;
            }
        }
    }
}