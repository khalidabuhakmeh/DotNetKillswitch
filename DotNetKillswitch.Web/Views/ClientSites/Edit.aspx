<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DotNetKillswitch.Core.ClientSite>" %>
<%@ Import Namespace="DotNetKillswitch.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Client Site: &quot;<%: Model.Name %>&quot;</h2>

    <div class="clientSiteInfo">
          <% using (Html.BeginForm("Remove", "ClientSites")){%>
                <%= Html.HiddenFor(model => model.Id) %>     
                <input type="submit" value="Remove" class="remove" title="Remove Client Site" />
           <%}%>
            <div class="token">
                <label>Client Site Token</label>
                <%: Model.Id %>
            </div>
            <div class="date">
               <label>Last Time Blacklisted</label>
                <%= Html.BlaklistDate(Model.LastTimeBlackListed) %>
            </div>
    </div>

    <fieldset>
    <legend>Client Site</legend>
    <% using (Html.BeginForm()) {%>
            <%= Html.HiddenFor(model => model.Id) %>     
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
                <input type="submit" value="Save" class="submit" />
            </p>
    <% } %>
    </fieldset>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

