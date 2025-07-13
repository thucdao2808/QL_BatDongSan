<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="FormGiaoDich.aspx.cs" Inherits="Main.FormGiaoDdich" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/WebControl/NotificationControl.ascx" TagPrefix="uc" TagName="Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Giao Dịch</title>
        <style>
            @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap');

            body {
                font-family: 'Roboto', sans-serif;
                background-color: #f4f7f9;
                margin: 0;
                padding: 20px;
            }

            h1 {
                text-align: center;
                color: #2c3e50;
                margin-bottom: 25px;
                font-weight: 500;
            }

            /* --- Layout chính --- */
            .container {
                max-width: 1200px;
                margin: 0 auto;
                background-color: #ffffff;
                padding: 25px 30px;
                border-radius: 12px;
                box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
            }

            /* --- Thanh điều khiển phía trên (Tìm kiếm & Nút bấm) --- */
            .headControl {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 20px;
                padding-bottom: 20px;
                border-bottom: 1px solid #e0e0e0;
            }

            /* --- Ô tìm kiếm --- */
            .groupSearch {
                display: flex;
                align-items: center;
                background-color: #f1f1f1;
                border-radius: 20px;
                padding: 2px 5px;
            }

            .imgSearch {
                height: 20px;
                width: 20px;
                margin: 0 8px;
                opacity: 0.6;
            }

            .txtSearch {
                border: none;
                background: transparent;
                padding: 8px;
                outline: none;
                width: 280px;
                font-size: 14px;
            }

            /* --- Nhóm nút chức năng chính --- */
            .boxBtnAction {
                display: flex;
                gap: 15px; /* Khoảng cách giữa các nút */
            }

            .itemBtnHead {
                height: 36px;
                width: 36px;
                border: none;
                border-radius: 50%; /* Bo tròn nút */
                cursor: pointer;
                transition: all 0.3s ease;
                padding: 6px;
                background-color: #f0f0f0;
                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            }

                .itemBtnHead:hover {
                    transform: translateY(-2px);
                    box-shadow: 0 4px 8px rgba(0,0,0,0.15);
                }

            /* --- GridView hiện đại --- */
            .gridview-modern {
                width: 100%;
                border-collapse: collapse;
                border: none;
                font-size: 14px;
            }

            /* --- Header của GridView --- */
            .gridview-header th {
                background-color: #34495e; /* Màu xanh đậm */
                color: #ffffff;
                font-weight: 500;
                padding: 15px 12px;
                text-align: left;
                text-transform: uppercase;
                font-size: 13px;
                letter-spacing: 0.5px;
            }

                .gridview-header th:first-child {
                    border-top-left-radius: 8px;
                }

                .gridview-header th:last-child {
                    border-top-right-radius: 8px;
                }

            /* --- Các dòng trong GridView --- */
            .gridview-row td, .gridview-alt-row td {
                padding: 12px;
                border-bottom: 1px solid #ecf0f1;
                color: #555;
                transition: background-color 0.2s ease;
            }

            .gridview-alt-row {
                background-color: #f9fafb;
            }

            .gridview-modern tr:hover td {
                background-color: #e8f4fd; /* Màu xanh nhạt khi hover */
            }

            /* --- Style cho các cột đặc biệt --- */
            .col-price {
                font-weight: 500;
                color: #27ae60; /* Màu xanh lá cho giá */
                text-align: right;
            }

            .col-actions {
                text-align: center;
            }

                .col-actions .itemBtn {
                    width: 22px;
                    height: 22px;
                    margin: 0 5px;
                    opacity: 0.7;
                    transition: opacity 0.2s ease;
                }

                    .col-actions .itemBtn:hover {
                        opacity: 1;
                        transform: scale(1.1);
                    }
            /*grv*/
  
            .grvForm td, .grvForm th {
                text-align: center;
                border: 2px solid black;
                padding: 5px;
            }

            .grvForm th {
                background-color:#0056b3;
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


            .checkbox-container {
                transform: scale(1.2);
                cursor: pointer;
            }

            .itemBtn {
                width: 24px;
                height: 24px;
                margin: 0 4px;
                transition: transform 0.2s ease, box-shadow 0.2s ease;
                cursor: pointer;
            }

                .itemBtn:hover {
                    transform: scale(1.2);
                    box-shadow: 0 0 6px rgba(0, 0, 0, 0.2);
                }

            .action-buttons {
                display: flex;
                justify-content: center;
                align-items: center;
            }

            /*panel*/
            .form-panel {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                width: 700px;
                background-color: #fff;
                padding: 30px;
                border-radius: 10px;
                box-shadow: 0 8px 16px rgba(0,0,0,0.25);
                z-index: 9999;
                border: 1px solid #ddd;
            }

            .form-grid {
                display: flex;
                gap: 20px;
            }

            .form-column {
                flex: 1;
                padding: 9px;
            }

            /* Giữ nguyên các styles đã có */
            .form-group {
                margin-bottom: 15px;
            }

                .form-group label {
                    display: block;
                    font-weight: bold;
                    margin-bottom: 5px;
                    color: #333;
                }

            .form-control {
                width: 100%;
                padding: 8px 10px;
                border-radius: 5px;
                border: 1px solid #ccc;
                font-size: 14px;
            }

            .form-actions {
                text-align: right;
                margin-top: 20px;
            }

                .form-actions .btn {
                    margin-left: 5px;
                    padding: 8px 16px;
                    font-size: 14px;
                    border-radius: 5px;
                    border: none;
                    cursor: pointer;
                }

            .btn-primary {
                background-color: #007bff;
                color: white;
            }

            .btn-secondary {
                background-color: #6c757d;
                color: white;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <uc:Notification ID="NotifyControl" runat="server" Visible="false" />
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                    <h1>Danh Sách Giao Dịch</h1>
                    <asp:GridView
                        ID="grvGiaoDich"
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
                            <asp:TemplateField HeaderText="Tên BDS">
                                <ItemTemplate>
                                    <asp:Label ID="lblBDSId" runat="server" Text='<%#GetTenBDs(Convert.ToInt16(Eval("BDSId"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên Khách hàng">
                                <ItemTemplate>
                                    <asp:Label ID="lblKhachHangId" runat="server" Text='<%# GetTenKhachHang(Convert.ToInt32(Eval("KhachHangId"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nhân Viên Phụ Trách">
                                <ItemTemplate>
                                    <asp:Label ID="lblNhanVienId" runat="server" Text='<%#GetTenNhanVien(Convert.ToInt32(Eval("NhanVienId"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NgayGD" HeaderText="Ngày Giao Dịch" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="MaGD" HeaderText="Mã Mua" />
                            <asp:BoundField DataField="LoaiGD" HeaderText="Loại Giao Dịch" />
                            <asp:BoundField DataField="GiaTri" HeaderText="Giá Trị" DataFormatString="{0:N0} VNĐ" />
                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi Chú" />
                        </Columns>
                    </asp:GridView>

                    <asp:Panel ID="pnlThemGiaoDich" runat="server" CssClass="form-panel" Visible="false">
                        <div>
                            <h3 style="display: flex; justify-content: center">Thêm Giao Dịch </h3>
                        </div>
                        <div class="form-grid">
                            <div class="form-column">
                                <div class="form-group">
                                    <label>Mã giao dịch:</label>
                                    <asp:TextBox ID="txtMaGD" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Bất động sản:</label>
                                    <asp:DropDownList ID="ddlBDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Khách hàng:</label>
                                    <asp:DropDownList ID="ddlKhachHang" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Ngày giao dịch:</label>
                                    <asp:TextBox ID="txtNgayGD" runat="server" CssClass="form-control" />

                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                        TargetControlID="txtNgayGD"
                                        Format="dd/MM/yyyy" />
                                </div>
                            </div>

                            <div class="form-column">
                                <div class="form-group">
                                    <label>Nhân viên phụ trách:</label>
                                    <asp:DropDownList ID="ddlNhanVien" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Loại giao dịch:</label>
                                    <asp:TextBox ID="txtLoaiGD" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Giá trị:</label>
                                    <asp:TextBox ID="txtGiaTri" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Ghi chú:</label>
                                    <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-actions">
                            <asp:Button ID="btnLuu" runat="server" Text="Lưu" CssClass="btn btn-primary" OnClick="btnLuu_Click" />
                            <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnHuy_Click" />
                        </div>
                    </asp:Panel>


                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
