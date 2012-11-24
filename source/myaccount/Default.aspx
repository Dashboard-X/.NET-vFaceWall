<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/main.master"
    CodeFile="Default.aspx.cs" Inherits="myaccount_Default" %>

<%@ Register Src="modules/avator_upd2.ascx" TagName="avator_upd2" TagPrefix="uc1" %>
<%@ Register Src="modules/macc_menu.ascx" TagName="macc_menu" TagPrefix="uc2" %>
<%@ Register Src="modules/nav/left_nav.ascx" TagName="left_nav" TagPrefix="uc4" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Resources.vsk.myaccount %>
        -
        <%= Site_Settings.Website_Title %>
    </title>
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div id="msg" runat="server">
    </div>
    <div class="item">
        <uc2:macc_menu ID="macc_menu1" runat="server" />
    </div>
    <div class="item_pad_2">
        <div class="msg_b">
            <table>
                <tr>
                    <td class="lft">
                        <div id="msg_l_lst">
                            <uc4:left_nav ID="left_nav1" runat="server" />
                        </div>
                    </td>
                    <td class="md">
                    </td>
                    <td class="rt">
                        <div class="pd_10">
                            <!-- main content section -->
                            <div class="item">
                                <table>
                                    <tr>
                                        <td style="padding: 5px; text-align: center; vertical-align: top; width: 17%;">
                                            <uc1:avator_upd2 ID="avator_upd21" runat="server" />
                                        </td>
                                        <td style="padding: 5px; text-align: left; vertical-align: top; width: 83%;">
                                            <div id="ovr_first" runat="server" style="line-height: 16px;">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="separator_10px">
                            </div>
                            <div class="item_pad_4">

                            </div>
                            <!-- close main content section -->
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
