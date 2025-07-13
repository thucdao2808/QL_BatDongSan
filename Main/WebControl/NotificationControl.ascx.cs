using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main.WebHelper;
using Main;
using System.Web.UI.HtmlControls;

namespace Main.WebControl
{
    public partial class NotificationControl : System.Web.UI.UserControl
    {
        // Declare the controls as protected fields  
      

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        public void ShowSuccess(string mainMessage, string subMessage)
        {
            
            messageText.InnerText = mainMessage;
            subText.InnerText = subMessage;
            boxSuccess.Style["display"] = "block";
        }
        public void ShowError(string mainMessage, string subMessage)
        {
            messageText.InnerText = mainMessage;
            subText.InnerText = subMessage;
            boxSuccess.Style["display"] = "block";
            boxSuccess.Style["background-color"] = "red";
        }

        public void Hide()
        {
            boxSuccess.Style["display"] = "none";
        }


    }
}