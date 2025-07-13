<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="FormKhachHang.aspx.cs" Inherits="Main.FormKhachHang" %>
<%@ Register Src="~/WebControl/NotificationControl.ascx" TagPrefix="uc" TagName="Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Quản Lý Khách Hàng</title>
        <style>
            body {
                font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
                background-color: #f8f9fa;
                color: #333;
                margin: 20px;
            }

            h1 {
                color: #343a40;
                margin-bottom: 20px;
                border-bottom: 2px solid #007bff;
                padding-bottom: 10px;
                font-size: 24px;
            }


            .headControl {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 25px;
                padding: 15px;
                background-color: #ffffff;
                border-radius: 8px;
                box-shadow: 0 2px 4px rgba(0,0,0,0.05);
            }

            .groupSearch {
                display: flex;
                align-items: center;
                border: 1px solid #ced4da;
                border-radius: 20px;
                padding: 0 5px;
                background-color: #fff;
            }

                .groupSearch:focus-within {
                    border-color: #80bdff;
                    box-shadow: 0 0 0 0.2rem rgba(0,123,255,.25);
                }

            .imgSearch {
                width: 20px;
                height: 20px;
                margin: 0 8px;
                opacity: 0.5;
            }

            .txtSearch {
                border: none;
                outline: none;
                padding: 10px 5px;
                background-color: transparent;
                font-size: 16px;
                width: 250px;
            }

            .boxBtnaction {
                display: flex;
                gap: 15px;
            }

            .itemBtnHead {
                width: 40px;
                height: 40px;
                border-radius: 50%;
                cursor: pointer;
                transition: transform 0.2s ease, box-shadow 0.2s ease;
                border: none;
                background-color: #f1f1f1;
                padding: 8px;
                box-shadow: 0 1px 3px rgba(0,0,0,0.1);
            }

                .itemBtnHead:hover {
                    transform: translateY(-2px);
                    box-shadow: 0 4px 8px rgba(0,0,0,0.15);
                }


            .gridview {
                width: 100%;
                border-collapse: collapse;
                background-color: #fff;
                border-radius: 8px;
                overflow: hidden; /* Bắt buộc để bo góc hoạt động */
                box-shadow: 0 4px 10px rgba(0,0,0,0.08);
            }

                /* --- Tiêu đề của GridView (Header) --- */
                .gridview th {
                    background-color: #007bff;
                    color: white;
                    padding: 15px;
                    text-align: left;
                    font-size: 16px;
                    font-weight: 600;
                    text-transform: uppercase;
                    letter-spacing: 0.5px;
                }

                /* --- Các ô dữ liệu của GridView (Cells) --- */
                .gridview td {
                    padding: 12px 15px;
                    border-bottom: 1px solid #e9ecef; /* Chỉ giữ lại đường kẻ ngang */
                    vertical-align: middle;
                }

                /* --- Các dòng của GridView (Rows) --- */
                .gridview tr {
                    transition: background-color 0.2s ease;
                }

                    /* --- Hiệu ứng Zebra-striping (dòng chẵn có màu khác) --- */
                    .gridview tr:nth-child(even) {
                        background-color: #f8f9fa;
                    }

                    /* --- Hiệu ứng khi di chuột qua dòng --- */
                    .gridview tr:hover {
                        background-color: #dee2e6;
                    }

                    /* Bỏ viền ở dòng cuối cùng */
                    .gridview tr:last-child td {
                        border-bottom: none;
                    }

            /* --- Các nút hành động trong GridView (Xem, Sửa, Xóa) --- */
            .itemBtn {
                width: 28px;
                height: 28px;
                cursor: pointer;
                margin-right: 10px;
                opacity: 0.7;
                transition: opacity 0.2s ease, transform 0.2s ease;
            }

                .itemBtn:hover {
                    opacity: 1;
                    transform: scale(1.2); /* Phóng to nhẹ khi di chuột qua */
                }

            /*Panel*/
            .form-container-Them {
                position: fixed; /* Đặt cố định trên màn hình */
                top: 50%; /* Đặt vị trí 50% từ trên */
                left: 50%; /* Đặt vị trí 50% từ trái */
                transform: translate(-50%, -50%); /* Dịch chuyển để canh giữa chính xác */
                z-index: 9999; /* Đảm bảo nằm trên cùng các phần tử khác */
                background-color: #fff;
                border-radius: 16px;
                box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
                padding: 24px;
                max-width: 800px;
                width: 90vw; /* Chiều rộng max 90% màn hình */
                max-height: 90vh; /* Chiều cao max 90% màn hình */
                overflow-y: auto; /* Tự động có scroll nếu nội dung dài */
                animation: fadeIn 0.3s ease-in-out;
            }

                /* Tiêu đề form */
                .form-container-Them h3 {
                    text-align: center;
                    margin-bottom: 24px;
                    font-size: 24px;
                    color: #333;
                }

            /* Nút đóng (exit) */
            .btnBack {
                position: absolute;
                top: 16px;
                right: 16px;
                width: 28px;
                height: 28px;
                cursor: pointer;
            }

            /* Layout dạng lưới 2 cột */
            .form-grid {
                display: grid;
                grid-template-columns: repeat(2, 1fr);
                gap: 20px;
            }

            /* Nhóm label + textbox */
            .form-group {
                display: flex;
                flex-direction: column;
            }

                /* Label */
                .form-group label {
                    font-weight: bold;
                    margin-bottom: 6px;
                    color: #444;
                }

            /* Input control */
            .form-control {
                padding: 8px 12px;
                border: 1px solid #ccc;
                border-radius: 8px;
                font-size: 14px;
                transition: border-color 0.2s ease-in-out;
            }

                .form-control:focus {
                    outline: none;
                    border-color: #007bff;
                }

            /* Ảnh preview */
            .igUpload {
                margin-top: 8px;
                max-width: 100%;
                height: auto;
                border-radius: 10px;
                border: 1px solid #ccc;
            }

            /* Nút thêm */
            .btn {
                background-color: #007bff;
                color: white;
                border: none;
                padding: 10px 20px;
                font-weight: bold;
                border-radius: 8px;
                cursor: pointer;
                transition: background-color 0.2s ease-in-out;
            }

                .btn:hover {
                    background-color: #0056b3;
                }

            /* Khu vực hành động */
            .form-actions {
                margin-top: 20px;
                text-align: center;
            }

            /* Animation khi hiển thị form */
            @keyframes fadeIn {
                from {
                    opacity: 0;
                    transform: translateY(-20px);
                }

                to {
                    opacity: 1;
                    transform: translateY(0);
                }
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
                        <asp:ImageButton ID="imgThemUser" runat="server" ImageUrl="~/Resource/them.jpg" CssClass="itemBtnHead" OnClick="imgThemUser_Click"   />
                        <asp:ImageButton ID="ImageButton3" runat="server" CssClass="itemBtnHead" ImageUrl="~/Resource/excel.jpg" OnClick="ImageButton3_Click" />
                        <asp:ImageButton ID="ImageButton4" runat="server" CssClass="itemBtnHead" ImageUrl="~/Resource/dow.jpg" OnClick="ImageButton4_Click"  />
                    </div>
                </div>
                <div>
                    <h1>Danh Sách Khách Hàng</h1>
                    <asp:GridView
                        ID="grvKhachHang"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="gridview" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn ">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkExp" runat="server" CssClass="checkbox-container animated-checkbox" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="MaKH" HeaderText="Mã Khách Hàng" />
                            <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
                            <asp:BoundField DataField="SDT" HeaderText="Số Điện Thoại" />
                            <asp:BoundField DataField="NhuCau" HeaderText="Nhu Cầu" />
                            <asp:BoundField DataField="TamGia" HeaderText="Tầm Giá" />
                            <asp:BoundField DataField="KhuVuc" HeaderText="Khu Vực" />
                            <asp:TemplateField HeaderText="Hành Động">
                                <ItemTemplate>
                            
                                        <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Resource/pngtree-pencil-3d-icon-png-image_8998901.png" CommandArgument='<%# Eval("Id") %>' CssClass="itemBtn" OnClick="imgEdit_Click" />
                                        <asp:ImageButton ID="igXoa" runat="server" ImageUrl="~/Resource/xoa.jpg" CssClass="itemBtn"      OnClientClick="return confirm('Bạn có chắc chắn muốn xóa Khách hàng này không?');"  OnClick="igXoa_Click" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="pnlThemKH" runat="server" CssClass="form-container-Them" Visible="false" DefaultButton="btnThemKH">
                     <asp:ImageButton ID="btnCloseKH" runat="server" ImageUrl="~/Resource/exit.png" CssClass="btnBack" OnClick="btnCloseKH_Click" />
                      <h3>Thêm Khách Hàng Mới</h3>
                        <div class="form-grid">

        <div class="form-group">
            <asp:Label runat="server" Text="Mã KH:" AssociatedControlID="txtMaKH" />
            <asp:TextBox ID="txtMaKH" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Họ Tên:" AssociatedControlID="txtHoTen" />
            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Số Điện Thoại:" AssociatedControlID="txtSDT" />
            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Nhu Cầu:" AssociatedControlID="txtNhuCau" />
            <asp:TextBox ID="txtNhuCau" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Tầm Giá:" AssociatedControlID="txtTG" />
            <asp:TextBox ID="txtTG" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Khu Vực:" AssociatedControlID="txtKhuVuc" />
            <asp:TextBox ID="txtKhuVuc" runat="server" CssClass="form-control" />
        </div>
    </div>

                         <div class="form-actions">
        <asp:Button ID="btnThemKH" runat="server" Text="Thêm Khách Hàng" CssClass="btn" OnClick="btnThemKH_Click" />
    </div>
                </asp:Panel>

                <asp:Panel ID="pnlSuaKH" runat="server" CssClass="form-container-Sua" Visible="false" DefaultButton="btnSuaKH">
    <asp:ImageButton ID="btnCloseSuaKH" runat="server" ImageUrl="~/Resource/exit.png" CssClass="btnBack" Onclick="btnCloseSuaKH_Click" />
    <h3>Sửa Thông Tin Khách Hàng</h3>
    <div class="form-grid">
        <asp:HiddenField ID="hfKh" runat="server" />
        <div class="form-group">
            <asp:Label runat="server" Text="Mã KH:" AssociatedControlID="txtMaKH_Sua" />
            <asp:TextBox ID="txtMaKH_Sua" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Họ Tên:" AssociatedControlID="txtHoTen_Sua" />
            <asp:TextBox ID="txtHoTen_Sua" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Số Điện Thoại:" AssociatedControlID="txtSDT_Sua" />
            <asp:TextBox ID="txtSDT_Sua" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Nhu Cầu:" AssociatedControlID="txtNhuCau_Sua" />
            <asp:TextBox ID="txtNhuCau_Sua" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Tầm Giá:" AssociatedControlID="txtTG_Sua" />
            <asp:TextBox ID="txtTG_Sua" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Khu Vực:" AssociatedControlID="txtKhuVuc_Sua" />
            <asp:TextBox ID="txtKhuVuc_Sua" runat="server" CssClass="form-control" />
        </div>

    </div>

    <div class="form-actions">
        <asp:Button ID="btnSuaKH" runat="server" Text="Cập Nhật" CssClass="btn" OnClick="btnSuaKH_Click"  />
    </div>
</asp:Panel>


            </div>
        </form>
    </body>
    </html>
</asp:Content>
