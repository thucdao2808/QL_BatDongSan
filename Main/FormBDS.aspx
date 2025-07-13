<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="FormBDS.aspx.cs" Inherits="Main.RoleUser" %>

<%@ Register Src="~/WebControl/NotificationControl.ascx" TagPrefix="uc" TagName="Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>QUẢN LÝ BẤT ĐỘNG SẢN</title>
        <link rel="stylesheet" type="text/css" href="Contents/FormBDS.css" />
        <style>
            .imgExport {
                width: 30px;
                height: 30px;
            }

            .igUpload {
                width: 100px;
                height: 100px;
                object-fit: cover;
            }

            .boxbtn {
                display: flex;
            }

            .itemBtn {
                width: 30px;
                height: 30px;
                border-radius: 4px;
                background-color: white;
                border: 1px solid #e0e0e0;
                object-fit: contain;
                transition: all 0.2s ease-in-out;
                box-shadow: 0 1px 2px rgba(0,0,0,0.1);
            }

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

            .BorderGreen {
                border: 2px solid #00ff21;
                padding-left: 5px;
                padding-right: 5px;
            }

            .BorderYellow {
                border: 2px solid #0094ff;
                padding-left: 5px;
                padding-right: 5px;
            }

            .BorderOrange {
                border: 2px solid #ff6a00;
                padding-left: 5px;
                padding-right: 5px;
            }

            .form-container-Them {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 9999;
                background-color: #ffffff;
                padding: 30px 40px;
                border-radius: 12px;
                box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
                width: 600px;
                max-width: 95vw;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

                .form-container-Them h3 {
                    text-align: center;
                    font-size: 22px;
                    margin-bottom: 20px;
                    color: #333;
                }

            /* Grid 2 cột */
            .form-grid {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 20px;
            }

            /* Label và input trong cột */
            .form-group {
                display: flex;
                flex-direction: column;
                margin-bottom: 10px;
            }

                .form-group label {
                    font-weight: 600;
                    margin-bottom: 5px;
                    color: #555;
                }

                .form-group .form-control {
                    padding: 8px 10px;
                    border: 1px solid #ccc;
                    border-radius: 6px;
                    font-size: 14px;
                }

            /* Button căn giữa */
            .form-actions {
                margin-top: 25px;
                text-align: center;
            }

                .form-actions .btn {
                    padding: 10px 30px;
                    font-size: 16px;
                    background-color: #007bff;
                    color: white;
                    border: none;
                    border-radius: 8px;
                    transition: background-color 0.3s ease;
                }

                    .form-actions .btn:hover {
                        background-color: #0056b3;
                        cursor: pointer;
                    }

            .btnBack {
                width: 20px;
                height: 20px;
                display: flex;
                justify-content: flex-end;
            }

            .igUploadShow {
                width: 250px;
                height: 250px;
                object-fit: cover;
            }


            .btnBack {
                position: absolute;
                top: 20px;
                right: 20px;
                cursor: pointer;
                width: 32px;
                height: 32px;
            }

            .form-container-Xem {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 9999;
                background-color: #ffffff;
                padding: 30px 40px;
                border-radius: 12px;
                box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
                width: 700px;
                height: 600px;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

                .form-container-Xem h3 {
                    text-align: center;
                    font-size: 24px;
                    margin-bottom: 20px;
                    color: #333;
                }

            .form-groupShow {
                display: flex;
                flex-direction: row;
                margin-bottom: 15px;
            }

                .form-groupShow label {
                    text-align: center;
                    align-items: center;
                    font-weight: 900;
                    margin-bottom: 5px;
                    color: #555;
                }

                .form-groupShow .form-control-show {
                    padding: 8px 10px;
                    border: 1px solid green;
                    border-radius: 6px;
                    font-size: 14px;
                    background-color: #f9f9f9;
                }



            .btnBack {
                position: absolute;
                top: 20px;
                right: 20px;
                cursor: pointer;
                width: 32px;
                height: 32px;
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
                    border: 2px solid #007bff;
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
                        }

            .titleShow {
                font-size: 15px;
                font-style: italic;
                font-weight: bold;
                padding-right: 10px;
                align-items: center;
            }
            /*search*/

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
        </style>


    </head>
    <body>

        <form id="form1" runat="server">
            <uc:Notification ID="NotifyControl" runat="server" Visible="false" />
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
                <asp:GridView ID="grvFormBDS" runat="server" CssClass="grvForm" AutoGenerateColumns="false" EnableViewState="true" HorizontalAlign="Center" DataKeyNames="Id">
                    <Columns>
                        <asp:TemplateField HeaderText="Chọn ">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkExp" runat="server" CssClass="checkbox-container animated-checkbox" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="MaBDS" HeaderText="Mã Đất" />
                        <asp:BoundField DataField="Loai" HeaderText="Phân Loại" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa Chỉ" />
                        <asp:BoundField DataField="Gia" HeaderText="Giá" />
                        <asp:BoundField DataField="DienTich" HeaderText="Diện Tích" />
                        <asp:TemplateField HeaderText="Tình trạng">
                            <ItemTemplate>
                                <asp:Label ID="lblTinhTrang" runat="server" Text='<%# Bind("TinhTrang")%>' CssClass='<%# Eval("TinhTrang").ToString().ToLower() == "đã bán".ToLower() ? "BorderGreen" : (Eval("TinhTrang").ToString().ToLower() == "đang bán".ToLower() ? "BorderYellow" : "BorderOrange") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chủ Sở Hữu ">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#LoadChuSoHuuName(Convert.ToInt32(Eval("ChuSoHuuId"))) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Hành Động">
                            <ItemTemplate>
                                <div style="display: flex; align-items: center;">
                                    <asp:ImageButton ID="imgXem" runat="server" ImageUrl="~/Resource/ege.png" CssClass="itemBtn" CommandArgument='<%# Eval("Id") %>' OnClick="imgXem_Click" />

                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Resource/pngtree-pencil-3d-icon-png-image_8998901.png" CommandArgument='<%# Eval("Id") %>' CssClass="itemBtn" OnClick="imgEdit_Click" />
                                    <asp:ImageButton ID="igXoa" runat="server" ImageUrl="~/Resource/xoa.jpg" CssClass="itemBtn" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa BDS này không?');"  OnClick="igXoa_Click" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Panel ID="pnlThemBDS" runat="server" CssClass="form-container-Them" Visible="false" DefaultButton="btnThem">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resource/exit.png" CssClass="btnBack" OnClick="ImageButton1_Click" />
                    <h3>Thêm Bất Động Sản Mới</h3>
                    <div class="form-grid">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Mã Đất:" AssociatedControlID="txtMaBDS" />
                            <asp:TextBox ID="txtMaBDS" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Phân Loại:" AssociatedControlID="txtLoai" />
                            <asp:TextBox ID="txtLoai" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Địa Chỉ:" AssociatedControlID="txtDiaChi" />
                            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Giá:" AssociatedControlID="txtGia" />
                            <asp:TextBox ID="txtGia" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="Upload Ảnh:" AssociatedControlID="FileUpload1" />
                            <asp:FileUpload ID="FileUpload1" runat="server" onchange="previewImage(this)" />
                            <asp:Image ID="imgPreview" runat="server" Visible="true" CssClass="igUpload" ClientIDMode="Static" />

                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="Diện Tích:" AssociatedControlID="txtDienTich" />
                            <asp:TextBox ID="txtDienTich" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Tình Trạng:" AssociatedControlID="ddlTinhTrang" />
                            <asp:DropDownList ID="ddlTinhTrang" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Đang bán" Value="Đang bán" />
                                <asp:ListItem Text="Đã bán" Value="Đã bán" />
                                <asp:ListItem Text="Đặt cọc" Value="Đặt cọc" />
                            </asp:DropDownList>
                        </div>

                        <div class="form-group" style="grid-column: span 2;">
                            <asp:Label runat="server" Text="Chủ Sở Hữu:" />
                            <asp:DropDownList ID="ddlChuSoHuu" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-actions">
                        <asp:Button ID="btnThem" runat="server" Text="Thêm mới" CssClass="btn" OnClick="btnThem_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlXemChiTietBDS" runat="server" CssClass="form-container-Xem" Visible="false">
                    <asp:HiddenField ID="IdBDS" runat="server" />

                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resource/xoa.jpg" CssClass="btnBack" OnClick="ImageButton2_Click" />

                    <h3>Chi Tiết Bất Động Sản</h3>

                    <div class="form-groupShow">
                        <div>
                            <asp:Label runat="server" Text="Ảnh:" CssClass="titleShow" />
                            <asp:Image ID="Image1" runat="server" CssClass="igUploadShow" />
                        </div>
                        <div>
                            <asp:Label ID="lblHoten" runat="server" Text="" CssClass="titleShow" /></br>
                    <asp:Label ID="lblSoDienThoai" runat="server" CssClass="titleShow" Text="" />
                        </div>
                    </div>

                    <div class="form-grid">
                        <div class="form-groupShow">
                            <asp:Label runat="server" Text="Mã Đất:" CssClass="titleShow" />
                            <asp:Label ID="lblMaBDS" runat="server" CssClass="form-control-show" />
                        </div>

                        <div class="form-groupShow">
                            <asp:Label runat="server" Text="Phân Loại:" CssClass="titleShow" />
                            <asp:Label ID="lblLoai" runat="server" CssClass="form-control-show" />
                        </div>

                        <div class="form-groupShow">
                            <asp:Label runat="server" Text="Địa Chỉ:" CssClass="titleShow" />
                            <asp:Label ID="lblDiaChi" runat="server" CssClass="form-control-show" />
                        </div>

                        <div class="form-groupShow">
                            <asp:Label runat="server" Text="Giá:" CssClass="titleShow" />
                            <asp:Label ID="lblGia" runat="server" CssClass="form-control-show" />
                        </div>

                        <div class="form-groupShow">
                            <asp:Label runat="server" Text="Diện Tích:" CssClass="titleShow" />
                            <asp:Label ID="lblDienTich" runat="server" CssClass="form-control-show" />
                        </div>

                        <div class="form-groupShow">
                            <asp:Label runat="server" Text="Tình Trạng:" CssClass="titleShow" />
                            <asp:Label ID="lblTinhTrang" runat="server" CssClass="form-control-show" />
                        </div>

                        <div class="form-groupShow" style="grid-column: span 2;">
                            <asp:Label runat="server" Text="Chủ Sở Hữu:" CssClass="titleShow" />
                            <asp:Label ID="lblChuSoHuu" runat="server" CssClass="form-control-show" />
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlSuaBDS" runat="server" CssClass="form-container-Them" Visible="false" DefaultButton="btnSua">
                    <asp:HiddenField ID="hfBDS" runat="server" />
                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Resource/exit.png" CssClass="btnBack" OnClick="ImageButton5_Click" />
                    <h3>Sửa Thông Tin Bất Động Sản</h3>
                    <div class="form-grid">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Mã Đất:" AssociatedControlID="txtMaBDS" />
                            <asp:TextBox ID="txtMaBDSedit" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Phân Loại:" AssociatedControlID="txtLoai" />
                            <asp:TextBox ID="txtLoaiedit" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Địa Chỉ:" AssociatedControlID="txtDiaChi" />
                            <asp:TextBox ID="txtDiaChiedit" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Giá:" AssociatedControlID="txtGia" />
                            <asp:TextBox ID="txtGiaedit" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Upload Ảnh mới (nếu có):" AssociatedControlID="FileUpload2" />
                            <asp:FileUpload ID="FileUpload2" runat="server" onchange="previewImage(this)" />
                            <asp:Image ID="Image2" runat="server" Visible="true" CssClass="igUpload" ClientIDMode="Static" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Diện Tích:" AssociatedControlID="txtDienTich" />
                            <asp:TextBox ID="txtDienTichedit" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" Text="Tình Trạng:" AssociatedControlID="ddlTinhTrang" />
                            <asp:DropDownList ID="ddlTinhTrangedit" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group" style="grid-column: span 2;">
                            <asp:Label runat="server" Text="Chủ Sở Hữu:" />
                            <asp:DropDownList ID="ddlChuSoHuuedit" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-actions">
                        <asp:Button ID="btnSua" runat="server" Text="Sửa" CssClass="btn" OnClick="btnSua_Click" />
                    </div>
                </asp:Panel>



            </div>
        </form>
        <script type="text/javascript">
            function previewImage(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        var img = document.getElementById('<%= imgPreview.ClientID %>');
                        img.src = e.target.result;
                        img.style.display = 'block'; // Hiện ảnh nếu đang ẩn
                    }

                    reader.readAsDataURL(input.files[0]);
                }
            }
        </script>
    </body>
    </html>
</asp:Content>
