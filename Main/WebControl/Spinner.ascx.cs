using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.WebControl
{
    public partial class Spinner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
        }

        public void ShowSpin()
        {
            LoaderSpin.Style["display"] = "block";
        }

        public void Hide()
        {
           
        }
    }
}