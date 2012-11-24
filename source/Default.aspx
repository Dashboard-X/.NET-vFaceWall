<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableViewState="false" CodeFile="Default.aspx.cs"
    MasterPageFile="~/main.master" Inherits="_Default" %>

<%@ Register Src="widgets/nav/home_nav.ascx" TagName="main_nav" TagPrefix="uc2" %>

<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="canonical" href="<%= Config.GetUrl() %>" />

</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div id="msg" runat="server">
    </div>
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="chnl_left_mn">
                <div class="module ui-corner-all">
                    <div class="heading">
                        <h3>.NET vFaceWall</h3>
                    </div>
                    <div class="item_pad_4">
                        <p>
                            <a href="http://www.mediasoftpro.com/net-vfacewall/">.NET vFacewall</a> is an open source script for creating next generation facebook style content sharing  post wall in your existing or standalone websites.
                            For more information  <a href="http://www.mediasoftpro.com/net-vfacewall/">click here</a>.
                        </p>
                        <p>
                            <a class="btn btn-large" href="Login.aspx">Login</a>&nbsp;<a class="btn btn-large" href="Register.aspx">Register</a>
                        </p>

                    </div>
                </div>
            </div>
            <div class="chnl_right_nav">
                <uc2:main_nav ID="main_nav1" runat="server" />
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
