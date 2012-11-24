<%@ Page Language="C#" AutoEventWireup="true" Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master"
    CodeFile="Social.aspx.cs" Inherits="adm_sc_config_Social" %>

<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Configuration | Manage Website Social Settings</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Website Social Settings</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_hd">
                Social Settings
            </div>
            <div class="bx_bd">
                <asp:Panel ID="pnl_add" runat="server" DefaultButton="btn_add">
                    <div class="item_pad_4">
                        <table class="tdiv">
                            <tr>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 30%">
                                    <strong>Social Settings: </strong>
                                </th>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <strong>....</strong>
                                </th>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Facebook Url:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_facebookurl" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Twitter Url:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_twitterurl" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Feedburner Url:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_feedburnerurl" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Facebook App ID:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_fbappid" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Facebook App Secret Key:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_fbsecretkey" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Addthis Widget Pub ID:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_addthispubid" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Twitter UID:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_twitter_uid" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Share Buttons:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_share" runat="server" Width="150px">
                                        <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="false">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>OG Meta Tags</strong>: Enable og meta tags in order
                                        to extend page sharing on facebook and other social networks.</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Show OG Meta Tags:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_og" runat="server" Width="150px">
                                        <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="false">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="item_pad_4">
                        <asp:Button ID="btn_add" runat="server" Text="Update" ValidationGroup="val_config"
                            OnClick="btn_add_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
