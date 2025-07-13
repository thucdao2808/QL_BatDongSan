<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Spinner.ascx.cs" Inherits="Main.WebControl.Spinner" %>
<link rel="stylesheet" href="../Contents/Spinner.css" />

<style>
    .loader {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 9999;
        display: flex;
        justify-content: center;
        align-items: center;
        background-color: rgba(111, 106, 106, 0.8);
    }

    .spinner.center {
        position: relative;
    }
</style>

<div>
    <div>Đang đăng nhập vui lòng chờ !</div>
    <div class="loader" id="LoaderSpin" runat="server">
        <div class="spinner center">
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
            <div class="spinner-blade"></div>
        </div>
    </div>
</div>
