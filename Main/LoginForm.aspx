<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Main.LoginForm" %>
<%@ Register Src="~/WebControl/Spinner.ascx" TagPrefix="UCspinner" TagName="SpinnerLoading" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Contents/LoginStyle.css" />
    <style>
        .input-Dropdow {
    display: flex;
    gap: 4px;
    padding:10px 0;
    font-family: 'Segoe UI', sans-serif;
}
        .custom-dropdown{
            width:90px;
            height:20px;
        }
        .label{
            font-size:15px;
            align-content:center;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <div class="displayBg">
            <img src="Resource/bds.jpg" class="boxBg"/>
        </div>
        <div class="form-container">
            <p class="title">Login</p>
            <div class="form">
                <div class="input-group">
                    <label for="username">Username</label>
                    <asp:TextBox ID="username" runat="server" CssClass="" placeholder=""></asp:TextBox>
                    <asp:Label ID="lblErrUserName" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="input-group">
                    <div>
                        <label for="password">Password</label>
                        <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="" placeholder=""></asp:TextBox>
                        <asp:Label ID="lblErrPassword" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                </div>
                 <div class="input-Dropdow">
                     <asp:DropDownList ID="ddlPhanQuyen" runat="server" CssClass="custom-dropdown"  ></asp:DropDownList>
                     <span class="label"> Quyền Đăng Nhập</span>
                 </div>
                <div class="forgot">
                    <div class="note">
                        <div>
                            <asp:CheckBox ID="CheckBox1" runat="server" OnClientClick="saveCredentials()" />
                            <span>Nhớ Mật Khẩu</span>
                        </div>
                        <div>
                            <a rel="noopener noreferrer" href="#">Forgot Password ?</a>
                        </div>
                    </div>
                </div>
                <div class="">
                    <label for="captchaInput">Mã Xác Nhận</label><br />
                    <div class="boxRecapcha">
                        <asp:TextBox ID="txtCaptchaInput" runat="server" CssClass="ipRecaptcha"></asp:TextBox>
                        <img src="CapChaImageWeb.aspx" alt="CAPTCHA" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resource/refesh.jpg" CssClass="imgRefesh" />
                    </div>
                    <asp:Label ID="lblErrCaptcha" runat="server" ForeColor="Red" Visible="false" Text=""></asp:Label>
                </div>
                <asp:Button ID="btnSignIn" runat="server" Text="Sign in" CssClass="sign" OnClick="btnSignIn_Click" />
            </div>
            <div class="social-message">
                <div class="line"></div>
                <p class="message">Login with social accounts</p>
                <div class="line"></div>
            </div>
            <div class="linkSocial">
                <asp:LinkButton ID="btnGg" runat="server" CssClass="btn btn-outline-primary">
                   <img src="Resource/gg.png" alt="Đăng nhập bằng GooGle" style="width:18px; height:18px; vertical-align:middle; margin-right:5px;" />
                </asp:LinkButton>
                <asp:LinkButton ID="btnGit" runat="server" CssClass="btn btn-outline-primary">
                   <img src="Resource/git.jpg" alt="Đăng nhập bằng Git" style="width:18px; height:18px; vertical-align:middle; margin-right:5px;" />
                </asp:LinkButton>
                <asp:LinkButton ID="btnTwi" runat="server" CssClass="btn btn-outline-primary">
                   <img src="Resource/x.jpg" alt="Đăng nhập bằng Twiwer" style="width:18px; height:18px; vertical-align:middle; margin-right:5px;" />
                </asp:LinkButton>
            </div>
            <p class="signup">
                Don't have an account?
               <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="SignInForm.aspx">Sign In</asp:HyperLink>
            </p>
        </div>
       <UCspinner:SpinnerLoading ID="SpinnerLoading" runat="server" Visible="false" />
    </form>
    
</body>
</html>
