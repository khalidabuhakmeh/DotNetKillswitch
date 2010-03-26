<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DotNetKillswitch.Core.ClientSite>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Client Sites Panel</h2>

    <p class="create">
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

    <table>
        <tr>
            <th>
                Client Name
            </th>
            <th>
                Token
            </th>
            <th>
                Is Blacklisted
            </th>
            <th>
                Last Blackout
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td class=blacklist> 
                <span><img src="/content/images/<%= item.IsBlackListed.ToString() %>.png" alt="<%=item.IsBlackListed.ToString() %>" /></span>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.LastTimeBlackListed)) %>
            </td>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new {  id= item.Id }) %>                
            </td>
        </tr>
    
    <% } %>

    </table>

    <p class="create">
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

