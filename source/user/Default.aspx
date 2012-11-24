<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    MasterPageFile="~/user/channel.master" Inherits="user_Default" %>

<%@ Register Src="modules/connect_v2.ascx" TagName="connect_v2" TagPrefix="uc8" %>
<%@ Register Src="modules/ch_profile.ascx" TagName="ch_profile" TagPrefix="uc9" %>
<%@ Register Src="modules/activities.ascx" TagName="activities" TagPrefix="uc6" %>
<%@ Register Src="modules/nav.ascx" TagName="nav" TagPrefix="uc12" %>
<%@ Register Src="~/user/modules/post.ascx" TagName="post" TagPrefix="uc5" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <%= this.ChannelTheme_Url%>
    <script type="text/javascript" src="<%= Config.GetUrl() %>javascript/tc.js">
    </script>
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
                <div id="uwall">
                    <div class="item">
                        <div class="module ui-corner-all">
                                <div style="float: left; width: 50%;">
                                    <h3 style="padding-top: 4px;">Recent Activities</h3>
                                </div>
                                <div style="float: right; width: 43%;">
                                    <asp:Panel ID="pnl_search" CssClass="input-append" runat="server" DefaultButton="btn_search">
                                        <asp:TextBox ID="txt_search" Width="200px" placeholder="Search User Activities" runat="server" /><asp:Button
                                            ID="btn_search" runat="server" Text="Go!" OnClick="btn_search_Click1" />
                                    </asp:Panel>
                                </div>
                                <div class="clear">
                                </div>
                        </div>
                    </div>
                    <div id="pgroup" class="item_pad" runat="server">
                            <uc5:post ID="post1" runat="server" />
                    </div>
                    <div class="item_pad">
                        <uc6:activities ID="activities1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
