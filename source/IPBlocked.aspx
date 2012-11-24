<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" MasterPageFile="~/main.master"
    CodeFile="IPBlocked.aspx.cs" Inherits="IPBlocked" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register src="widgets/nav/main_nav.ascx" tagname="main_nav" tagprefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>IP Address Blocked -
        <%= Site_Settings.Website_Title %>.</title>
    <meta name="description" content="IP Address you are using has been blocked by site administrator.">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="<%= NavigationClass %>">
                <uc3:main_nav id="main_nav1" runat="server" />
            </div>
            <div class="<%= BodyClass %>">
                <div class="item">
                    <div class="module ui-corner-all">
                        <div class="heading">
                            <h3 id="cmt" runat="server">
                                <%= Resources.vsk.ipaddressblocked %>
                            </h3>
                        </div>
                        <div class="item">
                            <div class="pd_5" id="msg" runat="server">
                               
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="module_t_mrg ui-corner-all" id="Div1" runat="server">
                        <uc1:h_468x60 ID="h_468x1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
