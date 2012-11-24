<%@ Page Language="C#" AutoEventWireup="true"  EnableViewState="false" MasterPageFile="~/main.master" CodeFile="Busy.aspx.cs" Inherits="Busy" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register Src="widgets/nav/main_nav.ascx" TagName="main_nav" TagPrefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        - Server is Busy.</title>
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="<%= NavigationClass %>">
                <uc3:main_nav ID="main_nav1" runat="server" />
            </div>
            <div class="<%= BodyClass %>">
                <div class="item">
                    <div class="module ui-corner-all">
                        <div class="heading">
                            <h3 id="cmt" runat="server">
                                Server is Busy
                            </h3>
                        </div>
                        <div class="item">
                            <div class="pd_5">
                                Server is too busy at this time, please try again after few minutes.
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="module_t_mrg ui-corner-all">
                        <uc1:h_468x60 ID="h_468x1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
