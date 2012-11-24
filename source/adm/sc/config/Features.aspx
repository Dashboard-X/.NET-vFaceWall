<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Features.aspx.cs" Theme="adm"
    StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_config_Features" %>

<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Configuration | Manage Website Features</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Website Features</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_hd">
                Feature Settings
            </div>
            <div class="bx_bd">
                <asp:Panel ID="pnl_add" runat="server" DefaultButton="btn_add">
                    <div class="item_pad_4">
                        <table class="tdiv">
                            <tr>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    <strong>Core Feature Settings: </strong>
                                </th>
                                <th style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <strong>on</strong>
                                </th>
                                <th style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <strong>off</strong>
                                </th>
                            </tr>
                            
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display channels:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="channels" ID="r_channels_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="channels" ID="r_channels_off" runat="server" />
                                </td>
                            </tr>
                           
                            
                            <tr>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Section Settings:
                                </th>
                                <th style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    ....
                                </th>
                                <th style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    ....
                                </th>
                            </tr>
                           
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display comments:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="comments" ID="r_comments_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="comments" ID="r_comments_off" runat="server" />
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display advertisement:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="ads" ID="r_ads_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="ads" ID="r_ads_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Email processing:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="email" ID="r_email_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="email" ID="r_email_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    New user email verification: (Enable to verify user after registration via email.)
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="email_ver" ID="r_email_ver_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="email_ver" ID="r_email_ver_off" runat="server" />
                                </td>
                            </tr>
                          
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Adult Verification | Validation:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="adult" ID="r_adult_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="adult" ID="r_adult_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Member Registration
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="registration" ID="r_registration_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="registration" ID="r_registration_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Custom Channel Theme (Enable customize different color themes for user channels)
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="mchannel" ID="r_mchannel_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="mchannel" ID="r_mchannel_off" runat="server" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Store User IP Address
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="ip" ID="r_ip_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="ip" ID="r_ip_off" runat="server" />
                                </td>
                            </tr>
                          
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display Ratings:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="rating" ID="r_rating_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="rating" ID="r_rating_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display Views:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="views" ID="r_views_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="views" ID="r_views_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display Channel Views:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="cviews" ID="r_cviews_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="cviews" ID="r_cviews_off" runat="server" />
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display Date:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="date" ID="r_date_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="date" ID="r_date_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Display User Name:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="username" ID="r_username_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="username" ID="r_username_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Authorization | Login setting for some actions:
                                </th>
                                <th style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    ....
                                </th>
                                <th style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    ....
                                </th>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Login required for rating content:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="login_rating" ID="r_login_rating_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="login_rating" ID="r_login_rating_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Login required for posting comment:
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="login_comment" ID="r_login_comment_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="login_comment" ID="r_login_comment_off" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 80%">
                                    Login required for posting advice point (like | dislike on comment):
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="login_advice" ID="r_advice_on" runat="server" />
                                </td>
                                <td style="padding: 5px; text-align: center; vertical-align: middle; width: 10%">
                                    <asp:RadioButton GroupName="login_advice" ID="r_advice_off" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="item_pad_4">
                        <asp:Button ID="btn_add" runat="server" Text="Update" OnClick="btn_add_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
