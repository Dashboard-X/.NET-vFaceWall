<%@ Control Language="C#" AutoEventWireup="true" CodeFile="vd_sponsor.ascx.cs" Inherits="modules_ads_vd_sponsor" %>
<%@ Register Src="../../ads/v_160x600.ascx" TagName="v_160x600" TagPrefix="uc1" %>
<div id="widget" runat="server">
    <div class="<%= BoxCssClass %>">
        <div class="<%= HeadingCssClass %>">
            <h3 id="htitle" runat="server">
                <%= Resources.vsk.sponsors %>
            </h3>
        </div>
        <div id="lst" runat="server">
            <uc1:v_160x600 ID="v_160x6001" runat="server" />
        </div>
    </div>
</div>
