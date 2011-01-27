<%@ Page Title="Update Customer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerUpdate.aspx.cs" Inherits="AzureWebCustomers.CustomerUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Update Customer</h2>
<div>
<asp:HyperLink ID="lnkCustomerList" runat="server" NavigateUrl="~/CustomerList.aspx">Customers</asp:HyperLink>
&nbsp;&nbsp;
<asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "CustomerUpdate.aspx?Id=" + Request["Id"] %>' runat="server" Text="Update" />
</div>
<table>
<tr>
<td>
Id
</td>
<td>
<asp:Label ID="lblId" runat="server" Text="<%# Customer.PartitionKey %>"></asp:Label>
</td>
</tr>
<tr>
<td>
Name
</td>
<td>
<asp:TextBox ID="txtName" runat="server" Text="<%# Customer.Name %>"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Address
</td>
<td>
<asp:TextBox ID="txtAddress" runat="server" Text="<%# Customer.Address %>"></asp:TextBox>
</td>
</tr>
</table>
<asp:Button ID="btnAccept" Text="Accept" runat="server" onclick="btnAccept_Click"/>
</asp:Content>
