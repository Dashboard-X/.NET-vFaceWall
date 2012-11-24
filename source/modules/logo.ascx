<%@ Control Language="C#" AutoEventWireup="true" CodeFile="logo.ascx.cs" Inherits="modules_logo" %>
<div style="padding: 15px 0px 0px 5px;">
    <a href='<%=Config.GetUrl() %>' title="<%= Site_Settings.Website_Title %>">
        <img src='<%=Config.GetUrl("images/logo.png") %>' alt="<%= Site_Settings.Website_Title %>" /></a>
</div>
