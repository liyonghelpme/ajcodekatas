<%@ Page Title="New Customer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerNew.aspx.cs" Inherits="AzureWebCustomers.CustomerNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>New Customer</h2>
<div>
<asp:HyperLink ID="lnkCustomerList" runat="server" NavigateUrl="~/CustomerList.aspx">Customers</asp:HyperLink>
</div>
<table>
<tr>
<td>
Name
</td>
<td>
<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Address
</td>
<td>
<asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
</td>
</tr>
</table>
<asp:Button ID="btnAccept" Text="Accept" runat="server" onclick="btnAccept_Click"/>
</asp:Content>
