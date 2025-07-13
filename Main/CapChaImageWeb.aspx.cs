using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class CapChaImageWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string captchaText = GenerateRandomCode(5); // Ví dụ 5 ký tự
            Session["CaptchaCode"] = captchaText;

            using (Bitmap bmp = new Bitmap(120, 40))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 20, FontStyle.Bold))
                {
                    g.DrawString(captchaText, font, Brushes.Black, new PointF(10, 5));
                }

                Response.ContentType = "image/jpeg";
                bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
            }
        }
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }
    }
}