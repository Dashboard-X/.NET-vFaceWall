<%@ Control Language="C#" AutoEventWireup="true" CodeFile="privacyo.ascx.cs" Inherits="myaccount_modules_privacyo" %>
<div class="b_top">
    <asp:Button ID="btn_save" ValidationGroup="usr_gen_prf" runat="server" OnClick="btn_save_Click" />
</div>
<div class="item">
    <!-- main content section -->
    <div class="bx_br_bt" style="padding: 6px 5px;">
        <a href="#" class="medium-text bold nounderline">
            <i id="icon_01" class="icon-chevron-down"></i> <%= Resources.vsk.message_privacy_02%></a>
    </div>
    <div id="pnl_01" class="bx_br_bt">
        <div class="pd_10">
            <asp:CheckBox ID="chk_allow" runat="server" /><%= Resources.vsk.message_privacy_03 %>
        </div>
    </div>
    <!-- close main content section -->
    <div class="separator_10px">
    </div>
</div>
<div class="b_bottom">
    <asp:Button ID="btn_bsave" runat="server" ValidationGroup="usr_gen_prf" OnClick="btn_bsave_Click" />
</div>
