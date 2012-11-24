<%@ Control Language="C#" AutoEventWireup="true" CodeFile="search.ascx.cs" Inherits="channel_modules_search" %>
<script type="text/javascript">
    $(document).ready(function () {
        src_ac('#<%= search.ClientID %>', '<%= acomplete_serviceurl %>');
    });
</script>
<div class="module ui-corner-all psm">
    <div class="bx_br_bt item_pad_4">
        <div style="float: left; width: 41%;">
            <asp:Panel ID="pnl_search" CssClass="input-append" runat="server" DefaultButton="btn_search">
                <asp:TextBox ID="search" runat="server" placeholder="Search User" Width="200px"></asp:TextBox><asp:Button
                    ID="btn_search" runat="server" Text="Go!" OnClick="btn_search_Click" />
            </asp:Panel>
        </div>
        <div style="float: right; width: 58%; padding-top:10px;">
            <a id="asearch" runat="server">Advance Search</a>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="item_pad_4 pagination" id="filter" runat="server">
    </div>
</div>
