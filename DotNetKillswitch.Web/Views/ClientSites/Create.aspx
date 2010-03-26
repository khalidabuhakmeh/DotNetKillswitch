<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DotNetKillswitch.Core.ClientSite>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <fieldset>
    <legend>Client Site</legend>
    <% using (Html.BeginForm()) {%>
            <div class="editor-label">
                <span><%= Html.LabelFor(model => model.Name) %>:</span>
                <%= Html.TextBoxFor(model => model.Name)%>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
               <label>Blacklist ?</label>
                <%= Html.CheckBoxFor(model => model.IsBlackListed) %>
                <%= Html.ValidationMessageFor(model => model.IsBlackListed) %>
            </div>            
            <p>
                <input type="submit" value="Create" class="submit" />
            </p>
    <% } %>
    </fieldset>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

