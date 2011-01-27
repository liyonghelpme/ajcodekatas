<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomerList.aspx.cs" Inherits="AzureWebCustomers.CustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Customers</h2>
    <div>
    <asp:HyperLink NavigateUrl="~/CustomerNew.aspx" runat="server" ID="lnkCustomerNew">New Customer</asp:HyperLink>
    </div>
    <asp:GridView runat="server" ID="grdCustomerList" AutoGenerateColumns="False">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="PartitionKey" 
                DataNavigateUrlFormatString="CustomerView.aspx?Id={0}" 
                Text="View..." />
            <asp:BoundField DataField="Name" HeaderText="Name" />
        </Columns>
    </asp:GridView>
</asp:Content>
