<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="FormNhanVien.aspx.cs" Inherits="Main.FormNhanVien" %>

<%@ Register Src="~/WebControl/NotificationControl.ascx" TagPrefix="uc" TagName="Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <style>
            /* Vùng tổng thể */
            form#form1 {
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                background: linear-gradient(to right, #e0f7fa, #ffffff);
                padding: 30px;
                border-radius: 20px;
                box-shadow: 0 8px 24px rgba(0,0,0,0.1);
                max-width: 1200px;
                margin: auto;
            }

            /* Tiêu đề */
            h1 {
                text-align: center;
                color: #0277bd;
                margin-bottom: 20px;
            }

            /* Header điều khiển */
            .headControl {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 30px;
                background: rgba(255, 255, 255, 0.5);
                padding: 20px;
                border-radius: 12px;
                backdrop-filter: blur(10px);
            }

            /* Nhóm tìm kiếm */
            .groupSearch {
                display: flex;
                align-items: center;
                gap: 10px;
            }

            .txtSearch {
                padding: 10px 15px;
                border: 1px solid #81d4fa;
                border-radius: 25px;
                font-size: 16px;
                width: 280px;
                transition: all 0.3s ease-in-out;
            }

                .txtSearch:focus {
                    outline: none;
                    box-shadow: 0 0 8px #29b6f6;
                    border-color: #29b6f6;
                }

            .imgSearch {
                width: 32px;
                height: 32px;
                cursor: pointer;
                transition: transform 0.2s ease;
            }

                .imgSearch:hover {
                    transform: scale(1.1);
                }

            /* Các nút hành động */
            .boxBtnaction .itemBtnHead {
                width: 40px;
                height: 40px;
                margin-left: 10px;
                border-radius: 50%;
                box-shadow: 0 2px 5px rgba(0,0,0,0.2);
                transition: transform 0.3s ease;
            }

                .boxBtnaction .itemBtnHead:hover {
                    transform: scale(1.1);
                }

            /* GridView */
            .grvForm{
                width:100%;
            }
            .grvForm td, .grvForm th {
                text-align: center;
                border: 2px solid black;
                padding: 5px;
            }

            .grvForm th {
                background-color: #0056b3;
                color: #f0f8ff;
            }

            .grvForm .checkbox-container {
                display: flex;
                justify-content: center;
                align-items: center;
            }

                .grvForm .checkbox-container input[type="checkbox"] {
                    appearance: none;
                    width: 20px;
                    height: 20px;
                    border: 1px solid #007bff;
                    border-radius: 4px;
                    background-color: #fff;
                    cursor: pointer;
                    transition: all 0.3s ease;
                }

                    .grvForm .checkbox-container input[type="checkbox"]:checked {
                        background-color: #007bff;
                        border-color: #0056b3;
                    }

                        .grvForm .checkbox-container input[type="checkbox"]:checked::after {
                            content: "✔";
                            color: #fff;
                            font-size: 14px;
                            display: flex;
                            justify-content: center;
                            align-items: center;
                            animation: checkboxCheckedAnimation 0.3s ease-in-out;
                        }

            @keyframes checkboxCheckedAnimation {
                0% {
                    transform: scale(0.8);
                    opacity: 0;
                }

                100% {
                    transform: scale(1);
                    opacity: 1;
                }
            }
            /* Nút hành động từng dòng */
            .itemBtn {
                width: 28px;
                height: 28px;
                margin: 0 5px;
                transition: transform 0.2s;
            }

                .itemBtn:hover {
                    transform: scale(1.15);
                }

            /*popup

*/
            .popupPanel {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                background-color: #fff;
                padding: 30px;
                border-radius: 12px;
                box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
                z-index: 10001;
                width: 400px;
            }

            .modalBackground {
                position: fixed;
                top: 0;
                left: 0;
                width: 100vw;
                height: 100vh;
                background-color: rgba(0, 0, 0, 0.5);
                z-index: 10000;
            }

            /* Khung popup */
            .popupPanel {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                background-color: #ffffff;
                padding: 30px 40px;
                border-radius: 12px;
                box-shadow: 0 20px 40px rgba(0, 0, 0, 0.25);
                width: 420px;
                z-index: 10001;
                font-family: 'Segoe UI', sans-serif;
            }

                /* Tiêu đề popup */
                .popupPanel h3 {
                    margin-top: 0;
                    margin-bottom: 25px;
                    font-size: 22px;
                    font-weight: 600;
                    color: #2c3e50;
                    text-align: center;
                }

                /* Bảng input */
                .popupPanel table {
                    width: 100%;
                }

                .popupPanel td {
                    padding: 8px 0;
                    vertical-align: middle;
                    color: #333;
                    font-size: 14px;
                }

                /* Ô nhập */
                .popupPanel input[type="text"],
                .popupPanel input[type="tel"],
                .popupPanel input[type="number"] {
                    width: 100%;
                    padding: 8px 12px;
                    border: 1px solid #ccc;
                    border-radius: 6px;
                    font-size: 14px;
                    transition: border-color 0.2s ease-in-out;
                }

                    .popupPanel input[type="text"]:focus {
                        border-color: #007bff;
                        outline: none;
                    }

                /* Nút */
                .popupPanel .btn {
                    padding: 8px 16px;
                    border-radius: 6px;
                    font-size: 14px;
                    font-weight: 500;
                    margin: 6px;
                    min-width: 90px;
                    cursor: pointer;
                }

                .popupPanel .btn-success {
                    background-color: #28a745;
                    border: none;
                    color: white;
                }

                    .popupPanel .btn-success:hover {
                        background-color: #218838;
                    }

                .popupPanel .btn-secondary {
                    background-color: #6c757d;
                    border: none;
                    color: white;
                }

                    .popupPanel .btn-secondary:hover {
                        background-color: #5a6268;
                    }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <uc:Notification ID="NotifyControl" runat="server" Visible="false" />
            <div>
                <div class="headControl">
                    <div>
                        <div class="groupSearch">
                            <asp:ImageButton ID="igSearch" runat="server" ImageUrl="~/Resource/search.jpg" CssClass="imgSearch" OnClick="igSearch_Click" />
                            <asp:TextBox ID="search" runat="server" CssClass="txtSearch" placeholder="Nhập Thông tin tìm kiếm" TextMode="Search" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="boxBtnaction">
                        <asp:ImageButton ID="imgThem" runat="server" ImageUrl="~/Resource/them.jpg" CssClass="itemBtnHead" OnClick="imgThem_Click" />
                        <asp:ImageButton ID="ImageButton3" runat="server" CssClass="itemBtnHead" ImageUrl="~/Resource/excel.jpg" OnClick="ImageButton3_Click" />
                        <asp:ImageButton ID="ImageButton4" runat="server" CssClass="itemBtnHead" ImageUrl="~/Resource/dow.jpg" OnClick="ImageButton4_Click" />
                    </div>
                </div>
                <div>
                    <h1>Danh Sách Nhân Viên</h1>
                    <asp:GridView
                        ID="grvNhanVien"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="grvForm" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkExp" runat="server" CssClass="checkbox-container animated-checkbox" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="MaNV" HeaderText="Mã Nhân Viên" />
                            <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
                            <asp:BoundField DataField="PhongBan" HeaderText="Phòng Ban" />
                            <asp:BoundField DataField="SDT" HeaderText="Số Điện Thoại" />

                            <asp:TemplateField HeaderText="Hành Động">
                                <ItemTemplate>
                                    <div style="display: flex; align-items: center;">

                                        <asp:ImageButton ID="imgEdit" runat="server"
                                            ImageUrl="~/Resource/pngtree-pencil-3d-icon-png-image_8998901.png"
                                            CssClass="itemBtn"
                                            ToolTip="Chỉnh sửa"
                                            CommandArgument='<%# Eval("Id") %>' OnClick="imgEdit_Click" />

                                        <asp:ImageButton ID="imgXoa" runat="server"
                                            ImageUrl="~/Resource/xoa.jpg"
                                            CssClass="itemBtn"
                                            ToolTip="Xóa nhân viên"
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nhân viên này không?');" OnClick="imgXoa_Click" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <!-- Panel thêm nhân viên -->
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="popupPanel" Visible="false">
                        <h3>Thêm Nhân Viên</h3>
                        <div class="form-group">
                            <label for="txtMaNV">Mã Nhân Viên:</label>
                            <asp:TextBox ID="txtMaNV" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtHoTen">Họ Tên:</label>
                            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtPhongBan">Phòng Ban:</label>
                            <asp:TextBox ID="txtPhongBan" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtSDT">SĐT:</label>
                            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btn btn-success" OnClick="btnThem_Click" />
                            <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnHuy_Click" />
                        </div>
                    </asp:Panel>

                    <!-- Panel sửa nhân viên -->
                    <asp:Panel ID="pnlEditPopup" runat="server" CssClass="popupPanel" Visible="false">
                        <h3>Chỉnh Sửa Nhân Viên</h3>
                        <asp:HiddenField ID="hfEditId" runat="server" />

                        <div class="form-group">
                            <label for="txtEditMaNV">Mã Nhân Viên:</label>
                            <asp:TextBox ID="txtEditMaNV" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtEditHoTen">Họ Tên:</label>
                            <asp:TextBox ID="txtEditHoTen" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtEditPhongBan">Phòng Ban:</label>
                            <asp:TextBox ID="txtEditPhongBan" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtEditSDT">SĐT:</label>
                            <asp:TextBox ID="txtEditSDT" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" CssClass="btn btn-success" OnClick="btnCapNhat_Click" />
                            <asp:Button ID="btnHuyEdit" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnHuyEdit_Click" />
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
