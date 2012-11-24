<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/user/channel.master" CodeFile="Default.aspx.cs" Inherits="user_settings_Default" %>
<%@ Register Src="~/user/modules/connect_v2.ascx" TagName="connect_v2" TagPrefix="uc8" %>
<%@ Register Src="~/user/modules/ch_profile.ascx" TagName="ch_profile" TagPrefix="uc9" %>
<%@ Register Src="~/user/modules/nav.ascx" TagName="nav" TagPrefix="uc12" %>

<%@ Register src="../modules/themes.ascx" tagname="themes" tagprefix="uc1" %>

<%@ Register src="../modules/cmodules.ascx" tagname="cmodules" tagprefix="uc2" %>

<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <%= this.ChannelTheme_Url%>
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div id="msg" runat="server">
    </div>
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="chnl_left">
                <div class="item">
                    <uc8:connect_v2 ID="connect_v21" runat="server" />
                </div>
                <div class="item">
                    <uc9:ch_profile ID="ch_profile1" runat="server" />
                </div>
                <div class="item">
                    <uc12:nav ID="nav1" runat="server" />
                </div>
            </div>
            <div class="chnl_right">
                <uc1:themes ID="themes1" runat="server" />
                <div class="item_pad">
                    <uc2:cmodules ID="cmodules1" runat="server" />
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
