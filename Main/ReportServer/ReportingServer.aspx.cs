using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.ReportServer
{
    public partial class ReportingServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    Microsoft.Reporting.WebForms.ServerReport serverReport = ReportViewer1.ServerReport;

                    // Cấu hình URL của Report Server
                    serverReport.ReportServerUrl = new Uri("http://thucdao/reportserver/");

                    // Cấu hình đường dẫn của báo cáo
                    serverReport.ReportPath ="/ThuVienReport";

                    // Dòng code gây lỗi sẽ hoạt động sau khi bạn thêm 'using' ở trên
                    Microsoft.Reporting.WebForms.IReportServerCredentials creds = new ReportServerCredentials("NgocThuc", "1234", "THUCDAO");
                    serverReport.ReportServerCredentials = creds;

                    serverReport.Refresh();
                }
                catch (Exception ex)
                {
                    Response.Write("<h2>LỖI:</h2><pre>" + ex.ToString() + "</pre>");
                }
            }
        }
        
    }
}