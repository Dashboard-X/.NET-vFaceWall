<%@ Control Language="C#" AutoEventWireup="true" CodeFile="channels_v2.ascx.cs" Inherits="modules_list_channels_v2" %>
<%@ Register Src="../item/list_nav_ch.ascx" TagName="list_nav" TagPrefix="uc2" %>
<%@ Register Src="~/modules/pagination_v2.ascx" TagName="pagination" TagPrefix="uc3" %>
<%@ Register Src="~/modules/list_stat.ascx" TagName="list_stat" TagPrefix="uc4" %>
<div class="module ui-corner-all">
    <div class="heading">
        <div style="float: left; width: 64%;">
            <h1 class="lsttitle" id="lbl_status" runat="server">
            </h1>
        </div>
        <div style="float: right; width: 35%;">
            <asp:Panel ID="pnl_search" CssClass="input-append item_r" runat="server" DefaultButton="btn_search">
                <asp:TextBox ID="txt_search" Width="150px" runat="server" /><asp:Button ID="btn_search"
                    runat="server" Text="Go!" OnClick="btn_search_Click1" />
            </asp:Panel>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="nmtabs">
        <div class="pd_5 item_r" id="lstat" runat="server">
            <uc4:list_stat ID="list_stat1" runat="server" />
        </div>
        <div class="item" runat="server" id="isnav">
            <uc2:list_nav ID="list_nav1" runat="server" />
        </div>
        <div id="lst" style="padding: 5px;" runat="server">
        </div>
        <div class="item" runat="server" id="pg">
            <uc3:pagination ID="pagination1" runat="server" />
        </div>
    </div>
</div>
