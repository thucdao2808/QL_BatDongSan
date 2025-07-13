<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignInForm.aspx.cs" Inherits="Main.SignInForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Contents/SignInStyle.css" />
</head>
<body>
    <form id="form1" runat="server" class="form">
        <div class="displayBg">
            <img src="Resource/bds.jpg" class="boxBg"></img>
        </div>
        <div class="form-container">
            <p class="form-title">Sign in to your account</p>
            <div class="input-container">
                <asp:TextBox ID="txtMaNhanVien" runat="server" CssClass="input-container-input"
                    Placeholder="Nhập họ tên"></asp:TextBox>
            </div>
            <div class="input-container">
                <asp:TextBox ID="txtHovaTen" runat="server" CssClass="input-container-input"
                    Placeholder="Nhập họ tên" ></asp:TextBox>
            </div>
            <div class="input-container">
                <asp:TextBox ID="txtSodienthoai" runat="server" CssClass="input-container-input"
                    Placeholder="Nhập số điện thoại" ></asp:TextBox>
            </div> 
            <div class="input-container">
                <asp:TextBox ID="txtPhongban" runat="server" CssClass="input-container-input"
                    Placeholder="Nhập phòng công tác" ></asp:TextBox>
            </div>
            <div class="input-container">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-container-input"
                    Placeholder="Enter email" TextMode="Email"></asp:TextBox>
            </div>
            <div class="input-container">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="input-container-input"
                    Placeholder="Enter password" TextMode="Password"></asp:TextBox>
            </div>

            <div class="input-container">
                <div class="input-Dropdow">
                    <asp:DropDownList ID="ddlPhanQuyen" runat="server" CssClass="custom-dropdown"></asp:DropDownList>
                    <span class="label">Quyền Đăng Nhập</span>
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

            <asp:Button ID="btnSignIn" runat="server" CssClass="submit" Text="Sign in" OnClick="btnSignIn_Click" />

            <p class="signup-link">
                No account?
        <asp:HyperLink ID="hlSignUp" runat="server" NavigateUrl="~/LoginForm.aspx" CssClass="">Sign up</asp:HyperLink>
            </p>
        </div>
    </form>
</body>
</html>
