<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AjClipper.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" Height="193px" TextMode="MultiLine" 
            Width="591px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Execute" 
            onclick="Button1_Click" />
    
        <br />
        <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
    
    <pre>
    <%= Result %>
    </pre>
    </div>
    </form>
</body>
</html>
