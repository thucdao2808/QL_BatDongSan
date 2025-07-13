<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Navigation.Master" CodeBehind="FormThongKe.aspx.cs" Inherits="Main.FormThongKe" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>THỐNG KÊ</title>
        <style>
            <style >
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                padding: 20px;
                margin: 0;
            }

            .container {
                max-width: 1200px;
                margin: auto;
                background: #fff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 8px rgba(0, 0, 0, 0.1);
            }

            h2 {
                margin-bottom: 20px;
            }

            .form-group {
                margin-bottom: 15px;
            }

                .form-group label {
                    display: block;
                    margin-bottom: 5px;
                }

            .form-control {
                width: 100%;
                padding: 8px 12px;
                box-sizing: border-box;
                border: 1px solid #ccc;
                border-radius: 4px;
            }

            .row {
                display: flex;
                justify-content:space-between;
                flex-wrap: wrap;
                margin: 0 -10px;
            }

            .col-3, .col-4, .col-6, .col-12 {
                padding: 0 10px;
            }

            .col-3 {
                width: 25%;
            }

            .col-4 {
                width: 33.3333%;
            }

            .col-6 {
                width: 50%;
            }

            .col-12 {
                width: 100%;
            }

            .text-right {
                text-align: right;
            }

            .btn {
                padding: 8px 16px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                margin-left: 10px;
            }

            .btn-primary {
                background-color: #007bff;
                color: white;
            }

            .btn-success {
                background-color: #28a745;
                color: white;
            }

            .card {
                background: #fff;
                border: 1px solid #ddd;
                border-radius: 6px;
                margin-bottom: 20px;
                overflow: hidden;
            }

            .card-header {
                background-color: #eee;
                padding: 10px 15px;
                font-weight: bold;
            }

            .card-body {
                padding: 15px;
            }

            .stat-card {
                text-align: center;
                border-left: 5px solid #007bff;
            }

            .stat-number {
                font-size: 24px;
                font-weight: bold;
                margin-bottom: 8px;
            }

            .stat-label {
                font-size: 14px;
                color: #777;
            }

            .table {
                width: 100%;
                border-collapse: collapse;
                margin-top: 10px;
            }

                .table th, .table td {
                    padding: 10px;
                    border: 1px solid #ccc;
                    text-align: left;
                }

            .table-hover tbody tr:hover {
                background-color: #f1f1f1;
            }

            @media screen and (max-width: 768px) {
                .col-3, .col-4, .col-6 {
                    width: 100%;
                }

                .btn {
                    display: block;
                    margin-top: 10px;
                }

                .text-right {
                    text-align: center;
                }
            }
        </style>

      

    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container mt-4">
                <h2 class="mb-4">Báo Cáo & Thống Kê Giao Dịch</h2>
                <div class="card mb-4">
                    <div class="card-header">
                        <i class="fas fa-filter"></i>Bộ Lọc Báo Cáo
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtTuNgay">Từ Ngày</label>
                                    <asp:TextBox ID="txtTuNgay" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtDenNgay">Đến Ngày</label>
                                    <asp:TextBox ID="txtDenNgay" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="ddlNhanVien">Nhân Viên</label>
                                    <asp:DropDownList ID="ddlNhanVien" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="ddlLoaiGD">Loại Giao Dịch</label>
                                    <asp:DropDownList ID="ddlLoaiGD" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button ID="btnXemBaoCao" runat="server" Text="Xem Báo Cáo" CssClass="btn btn-primary" OnClick="btnXemBaoCao_Click" />
                                <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất ra Excel" CssClass="btn btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlKetQua" runat="server" Visible="false">
                    <div class="row mb-4">
                        <div class="col-md-4">
                            <div class="card stat-card">
                                <div class="card-body">
                                    <div class="stat-number text-success">
                                        <asp:Label ID="lblTongDoanhThu" runat="server" Text="0 VNĐ"></asp:Label>
                                    </div>
                                    <div class="stat-label">Tổng Doanh Thu</div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="card stat-card">
                                <div class="card-body">
                                    <div class="stat-number text-primary">
                                        <asp:Label ID="lblTongSoGiaoDich" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="stat-label">Tổng Số Giao Dịch</div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="card stat-card">
                                <div class="card-body">
                                    <div class="stat-number text-info">
                                        <asp:Label ID="lblBdsDaGiaoDich" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="stat-label">BĐS Đã Giao Dịch</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card mb-4">
                        <div class="card-header">Danh Sách Giao Dịch Chi Tiết</div>
                        <div class="card-body">
                            <asp:GridView ID="gvGiaoDich" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="Không có dữ liệu nào phù hợp.">
                                <Columns>
                                    <asp:BoundField DataField="MaGD" HeaderText="Mã GD" />
                                    <asp:BoundField DataField="NgayGD" HeaderText="Ngày Giao Dịch" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="TenKhachHang" HeaderText="Khách Hàng" />
                                    <asp:BoundField DataField="TenNhanVien" HeaderText="Nhân Viên Phụ Trách" />
                                    <asp:BoundField DataField="GiaTri" HeaderText="Giá Trị" DataFormatString="{0:N0} VNĐ" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-lg-6">
                            <div class="card mb-4">
                                <div class="card-header">Doanh Thu Theo Nhân Viên</div>
                                <div class="card-body">
                                    <asp:Chart ID="chartDoanhThuNhanVien" runat="server" Height="300px" Width="300px">
                                        <Series>
                                            <asp:Series Name="DoanhThu" ChartType="Column"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="card mb-4">
                                <div class="card-header">Tỷ Trọng Giao Dịch Theo Loại BĐS</div>
                                <div class="card-body">
                                    <asp:Chart ID="chartTyTrongBDS" runat="server" Height="300px" Width="300px">
                                        <Series>
                                            <asp:Series Name="TyTrong" ChartType="Pie" IsValueShownAsLabel="true" Label="#PERCENT{P0}"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                        <Legends>
                                            <asp:Legend Name="Default" />
                                        </Legends>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
