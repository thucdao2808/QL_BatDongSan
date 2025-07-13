<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportingServer.aspx.cs" Inherits="Main.ReportServer.ReportingServer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" 
    TagPrefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

             <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="800px" ProcessingMode="Remote" />
        </div>
    </form>
</body>
</html>
