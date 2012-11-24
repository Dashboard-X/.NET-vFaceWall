<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" EnableViewState="false"
    MasterPageFile="~/main.master" Inherits="ContactUs" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register Src="widgets/nav/main_nav.ascx" TagName="main_nav" TagPrefix="uc3" %>
<%@ Register Src="modules/comments/cmt.ascx" TagName="cmt" TagPrefix="uc4" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="<%= BodyClass %>">
                <div class="item">
                    <div class="module ui-corner-all">
                        <div class="heading">
                            <h3 id="cmt" runat="server">
                                <%= Resources.vsk.contactus %>.
                            </h3>
                        </div>
                        <div class="item">
                            <div class="pd_5 ptext">
                                Place contact us information here..
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="<%= NavigationClass %>">
                <uc3:main_nav ID="main_nav1" runat="server" />
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
