<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/user/channel.master"
    CodeFile="Default.aspx.cs" Inherits="user_profile_Default" %>

<%@ Register Src="~/user/modules/connect_v2.ascx" TagName="connect_v2" TagPrefix="uc8" %>
<%@ Register Src="~/user/modules/nav.ascx" TagName="nav" TagPrefix="uc12" %>
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
                    <uc12:nav ID="nav1" runat="server" />
                </div>
            </div>
            <div class="chnl_right">
                <div class="module ui-corner-all" id="cprofile" runat="server">
                    
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
