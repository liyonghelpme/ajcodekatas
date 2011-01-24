<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerView.aspx.cs" Inherits="AzureWebCustomers.CustomerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Customer</h2>
<div>
<asp:HyperLink ID="lnkCustomers" NavigateUrl="~/CustomerList.aspx" runat="server" Text="Customers" />
</div>
<table>
<tr>
<td>
Id
</td>
<td>
<asp:Label ID="lblId" runat="server" Text="<%# Customer.PartitionKey %>"/>
</td>
</tr>
<tr>
<td>
Name
</td>
<td>
<asp:Label ID="lblName" runat="server" Text="<%# Customer.Name %>" />
</td>
</tr>
<tr>
<td>
Address
</td>
<td>
<asp:Label ID="lblAddress" runat="server"  Text="<%# Customer.Address %>" />
</td>
</tr>
</table>
</asp:Content>
