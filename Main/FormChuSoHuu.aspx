<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="FormChuSoHuu.aspx.cs" Inherits="Main.FormChuSoHuu" %>
<%@ Register Src="~/WebControl/NotificationControl.ascx" TagPrefix="uc" TagName="Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <style>
            .groupSearch {
                display: flex;
                align-items: center;
                width: 100%;
                max-width: 400px; /* hoặc giá trị khác tùy bố cục của bạn */
                border: 1px solid #ccc;
                border-radius: 8px;
                padding: 4px 8px;
                background-color: #fff;
                box-shadow: 0 2px 4px rgba(0,0,0,0.05);
                transition: border 0.2s ease-in-out;
            }

                .groupSearch:focus-within {
                    border-color: #007bff;
                    box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.2);
                }

            .imgSearch {
                width: 24px;
                height: 24px;
                object-fit: contain;
                margin-right: 8px;
                cursor: pointer;
                opacity: 0.7;
                transition: opacity 0.2s ease;
            }

                .imgSearch:hover {
                    opacity: 1;
                }

            .txtSearch {
                flex: 1;
                border: none;
                outline: none;
                font-size: 16px;
                padding: 6px 4px;
                background-color: transparent;
                color: #333;
            }

                .txtSearch::placeholder {
                    color: #aaa;
                }

            .imgSearch {
                width: 30px;
                height: 30px;
                object-fit: cover;
            }

            .headControl {
                display: flex;
                justify-content: space-around;
                padding-bottom: 15px;
            }

            .boxBtnaction {
                display: flex;
                gap: 12px; /* Khoảng cách giữa các nút */
                justify-content: flex-start; /* Hoặc center nếu bạn muốn căn giữa */
                align-items: center;
                padding: 10px;
                background-color: #f9f9f9;
                border-radius: 8px;
                box-shadow: 0 2px 6px rgba(0,0,0,0.05);
                flex-wrap: wrap; /* Tự động xuống hàng nếu không đủ chỗ */
            }

            .itemBtnHead {
                width: 40px;
                height: 40px;
                padding: 4px;
                border-radius: 8px;
                background-color: white;
                border: 1px solid #e0e0e0;
                object-fit: contain;
                transition: all 0.2s ease-in-out;
                box-shadow: 0 1px 2px rgba(0,0,0,0.1);
            }

                .itemBtnHead:hover {
                    transform: scale(1.1);
                    background-color: #f0f8ff;
                    border-color: #007bff;
                    box-shadow: 0 2px 6px rgba(0,123,255,0.3);
                    cursor: pointer;
                }

            /* grv */
  

            h2 {
                text-align: center;
                font-size: 24px;
                color: #333;
                margin-top: 20px;
            }

            .itemBtn {
                width: 28px;
                height: 28px;
                margin: 0 5px;
                padding: 3px;
                border: none;
                background-color: transparent;
                cursor: pointer;
                transition: transform 0.2s ease, box-shadow 0.2s ease;
            }

                .itemBtn:hover {
                    transform: scale(1.1);
                    box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
                }

            .gridview-actions {
                display: flex;
                justify-content: center;
                align-items: center;
            }


            /*popup them*/
            /* Nền mờ tối */
            .modal-overlay {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-color: rgba(0, 0, 0, 0.5); /* Nền mờ */
                display: flex;
                justify-content: center;
                align-items: center;
                z-index: 10000; /* Nằm đè lên tất cả */
            }

            /* Nội dung chính của popup */
            .popup-content {
                background-color: #fff;
                padding: 30px;
                border-radius: 10px;
                width: 400px;
                box-shadow: 0 0 15px rgba(0, 0, 0, 0.3);
            }

                /* Các input form đẹp hơn */
                .popup-content input[type="text"],
                .popup-content .form-control {
                    width: 100%;
                    padding: 6px 10px;
                    margin-top: 4px;
                    margin-bottom: 10px;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                }

                /* Nút bấm đẹp hơn */
                .popup-content .btn {
                    padding: 6px 14px;
                    margin-right: 10px;
                    border: none;
                    border-radius: 4px;
                    cursor: pointer;
                }

            .btn-success {
                background-color: #28a745;
                color: white;
            }

            .btn-secondary {
                background-color: #6c757d;
                color: white;
            }

            
            .gridview-table td, .gridview-table th {
                text-align: center;
                border: 2px solid black;
                padding: 5px;
            }

            .gridview-table th {
                background-color:#0056b3;
                color: #f0f8ff;
            }

            .gridview-table .checkbox-container {
                display: flex;
                justify-content: center;
                align-items: center;
            }

                .gridview-table .checkbox-container input[type="checkbox"] {
                    appearance: none;
                    width: 20px;
                    height: 20px;
                    border: 1px solid #007bff;
                    border-radius: 4px;
                    background-color: #fff;
                    cursor: pointer;
                    transition: all 0.3s ease;
                }

                    .gridview-table .checkbox-container input[type="checkbox"]:checked {
                        background-color: #007bff;
                        border-color: #0056b3;
                    }

                        .gridview-table .checkbox-container input[type="checkbox"]:checked::after {
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
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
             <uc:Notification ID="NotifyControl" runat="server" Visible="false"  />
            <div>
                <div class="headControl">
                    <div>
                        <div class="groupSearch">
                            <asp:ImageButton ID="igSearch" runat="server" ImageUrl="~/Resource/search.jpg" CssClass="imgSearch" OnClick="igSearch_Click" />
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txtSearch" placeholder="Nhập Thông tin tìm kiếm" TextMode="Search" />
                        </div>
                    </div>
                    <div class="boxBtnaction">
                        <asp:ImageButton ID="imgThem" runat="server" ImageUrl="~/Resource/them.jpg" CssClass="itemBtnHead" OnClick="imgThem_Click" />
                        <asp:ImageButton ID="ImageButton3" runat="server" CssClass="itemBtnHead" ImageUrl="~/Resource/excel.jpg" OnClick="ImageButton3_Click" />
                        <asp:ImageButton ID="ImageButton4" runat="server" CssClass="itemBtnHead" ImageUrl="~/Resource/dow.jpg" OnClick="ImageButton4_Click" />
                    </div>
                </div>
                <h2 style="text-align: center;">Danh sách Chủ Sở Hữu</h2>
                <asp:GridView ID="grvChuSoHuu" runat="server" AutoGenerateColumns="false" CssClass="gridview-table" HorizontalAlign="Center" DataKeyNames="Id">
                    <Columns>
                        <asp:TemplateField HeaderText="Chọn ">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkExp" runat="server" CssClass="checkbox-container animated-checkbox" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="MaChu" HeaderText="Mã Chủ" />
                        <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
                        <asp:BoundField DataField="SDT" HeaderText="SĐT" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa Chỉ" />
                        <asp:TemplateField HeaderText="Hành Động">
                            <ItemTemplate>
                                <div style="display: flex; align-items: center;">

                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Resource/pngtree-pencil-3d-icon-png-image_8998901.png" CommandArgument='<%# Eval("Id") %>' CssClass="itemBtn" OnClick="imgEdit_Click" />
                                    <asp:ImageButton ID="igXoa" runat="server" ImageUrl="~/Resource/xoa.jpg" CssClass="itemBtn" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa Nhà Chủ này không?');"  OnClick="igXoa_Click" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-overlay popup-panel" Visible="false">
                    <div class="popup-content">
                        <h3>Thêm Chủ Sở Hữu</h3>

                        <asp:Label runat="server" Text="Mã Chủ:" /><br />
                        <asp:TextBox ID="txtMaChu" runat="server" CssClass="form-control" /><br />

                        <asp:Label runat="server" Text="Họ Tên:" /><br />
                        <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" /><br />

                        <asp:Label runat="server" Text="SĐT:" /><br />
                        <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" /><br />

                        <asp:Label runat="server" Text="Địa Chỉ:" /><br />
                        <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" /><br />
                        <br />

                        <asp:Button ID="btnLuu" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnLuu_Click" />
                        <asp:Button ID="btnDong" runat="server" Text="Đóng" CssClass="btn btn-secondary" OnClick="btnDong_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlSua" runat="server" CssClass="modal-overlay popup-panel" Visible="false">
                    <div class="popup-content">
                        <h3>Sửa Chủ Sở Hữu</h3>

                        <asp:HiddenField ID="hdChuSoHuuId" runat="server" />

                        <asp:Label runat="server" Text="Mã Chủ:" /><br />
                        <asp:TextBox ID="txtma" runat="server" CssClass="form-control" /><br />

                        <asp:Label runat="server" Text="Họ Tên:" /><br />
                        <asp:TextBox ID="txtten" runat="server" CssClass="form-control" /><br />

                        <asp:Label runat="server" Text="SĐT:" /><br />
                        <asp:TextBox ID="txtsdteddit" runat="server" CssClass="form-control" /><br />

                        <asp:Label runat="server" Text="Địa Chỉ:" /><br />
                        <asp:TextBox ID="txtDc" runat="server" CssClass="form-control" /><br />
                        <br />

                        <asp:Button ID="btnCapNhat" runat="server" Text="Cập Nhật" CssClass="btn btn-success" OnClick="btnCapNhat_Click"  />
                        <asp:Button ID="Button1" runat="server" Text="Đóng" CssClass="btn btn-secondary" OnClick="Button1_Click" />
                    </div>
                </asp:Panel>


            </div>
        </form>
    </body>
    </html>
</asp:Content>
