<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" MasterPageFile="~/main.master"
    CodeFile="Validate.aspx.cs" Inherits="Validate" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register src="widgets/nav/main_nav.ascx" tagname="main_nav" tagprefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        |  <%= Resources.vsk.accountactivation %></title>
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
                               <%= Resources.vsk.accountactivation %>
                            </h3>
                        </div>
                        <div class="item ptext">
                            <div class="pd_5">
                                <div id="msg" runat="server">
                                </div>
                                <div class="item_pad_4">
                                    <strong>Dear
                                        <asp:Label ID="lbl_user" runat="server"></asp:Label>,</strong>
                                </div>
                                <div class="item_pad_2">
                                   <%= Resources.vsk.account_activation_message%>
                                </div>
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
