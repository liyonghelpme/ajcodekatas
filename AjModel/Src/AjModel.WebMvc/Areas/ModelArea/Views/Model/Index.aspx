<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AjModel.WebMvc.ViewModel.EntityListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= this.Model.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= this.Model.Title %></h2>
    <table>
        <tr>
            <% foreach (var property in this.Model.EntityModel.Properties)
               {
            %>
            <th>
                <%= Html.Encode(property.Name) %>
            </th>
            <%} %>
        </tr>
        <% foreach (var entity in this.Model.Entities)
           { %>
           <tr>
           <% foreach (var property in this.Model.EntityModel.Properties)
              { %>
              <td><%= property.GetValue(entity) %></td>
           <%} %>
           </tr>
        <%} %>
    </table>
</asp:Content>
