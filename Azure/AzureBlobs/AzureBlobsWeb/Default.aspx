<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AzureBlobsWeb._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        File Blobs
    </h2>
    <div>
        <asp:GridView ID="grdBlobs" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Uri" HeaderText="Uri" />
            </Columns>
        </asp:GridView>
    </div>
    <div>
    Upload: <asp:FileUpload ID="fluFile" runat="server" />
    </div>
    <div>
    <asp:Button ID="btnUpdload" runat="server" Text="Upload" 
            onclick="btnUpdload_Click" />
    </div>
</asp:Content>
