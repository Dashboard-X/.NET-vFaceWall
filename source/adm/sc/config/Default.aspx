<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Theme="adm"
    StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" CodeFile="Default.aspx.cs"
    Inherits="adm_sc_config_Default" %>

<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Configuration | Manage Website General Settings</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Website General Settings</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_hd">
                General Settings
            </div>
            <div class="bx_bd">
                <asp:Panel ID="pnl_add" runat="server" DefaultButton="btn_add">
                    <div class="item_pad_4">
                        <table class="tdiv">
                            <tr>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 30%">
                                    <strong>General Settings: </strong>
                                </th>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <strong>....</strong>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Title (optional)</strong>: It can only be used in RSS
                                        or ATOM Feeds</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Title (website):
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_title" runat="server" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Description (optional)</strong>: It can only be used
                                        in RSS or ATOM Feeds.</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Description (website):
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_description" TextMode="MultiLine" runat="server" Height="50"
                                        Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Page Title Caption (optional)</strong>: It can concatenate
                                        with original page title to display on all pages of website.<br />
                                        e.g "Video Starter Kit" will display on page titles like "<strong>Page Title - Video
                                            Starter Kit</strong>".</span>
                                </td>
                            </tr>
                          
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Admin email:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_email" runat="server" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Admin email display name:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_emaildisplayname" runat="server" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Database Type</strong>:</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Database type:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_databasetype" runat="server" Width="180px">
                                        <asp:ListItem Value="0" Text="Sql Server 2005 or later" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="MySQL"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Sql Server 2000 (not recommended)"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Screen Content</strong>: Choose whether screening content
                                        with dictionary values highlight only or replace / restrict it.
                                        <br />
                                        e.g screen word "<strong>book</strong>" to "<strong>b**k</strong>.</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Screen content:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_screening" runat="server" Width="180px">
                                        <asp:ListItem Value="0" Text="Screen" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Screen & Replace"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Content Approval</strong>: It set whether posting content
                                        automatically approve after posting or moderator review require before appear on
                                        website.</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Content approval:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_approvaltype" runat="server" Width="180px">
                                        <asp:ListItem Value="1" Text="Automatic" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Manual"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Rating Type</strong>: Set rating type to display rating
                                        on listings.</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Spam Count:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_abusereport_count" MaxLength="3" runat="server" Width="50px"></asp:TextBox>
                                    (before auto disable content)
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_abusereport_count"
                                        Display="Dynamic" ErrorMessage="required" ToolTip="required." ValidationGroup="val_config">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_abusereport_count"
                                        ErrorMessage="must be numeric" SetFocusOnError="true" Display="dynamic" EnableClientScript="true"
                                        ValidationGroup="val_config" ValidationExpression="(\d+)" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Data Cache Duration</strong>: (in minutes) Use to cache
                                        listing data for specified time in minutes. Enter (0) will disable data cache.<br />
                                        Note: This will only effect cache used in data listings (main content listings)
                                        not on user controls (e.g display listing on home page).</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Data Cache Duration:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_cache" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="req_cache" runat="server" ControlToValidate="txt_cache"
                                        Display="Dynamic" ErrorMessage="required" ToolTip="required." ValidationGroup="val_config">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="reg_cache" runat="server" ControlToValidate="txt_cache"
                                        ErrorMessage="must be numeric" SetFocusOnError="true" Display="dynamic" EnableClientScript="true"
                                        ValidationGroup="val_config" ValidationExpression="(\d+)" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Listing Size Settings</strong>: Display number of records
                                        in each page</span>
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Channel Page Size:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_channel_psize" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_channel_psize"
                                        Display="Dynamic" ErrorMessage="required" ToolTip="required." ValidationGroup="val_config">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_channel_psize"
                                        ErrorMessage="must be numeric" SetFocusOnError="true" Display="dynamic" EnableClientScript="true"
                                        ValidationGroup="val_config" ValidationExpression="(\d+)" />
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Website Layout</strong>: Set website navigation on
                                        left or right side or both side .</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Website Layout:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_navigationside" runat="server" Width="180px">
                                        <asp:ListItem Value="0" Text="Left Side (two column)" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Right Side (two column)"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Both Side (three column)"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Website Template:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:DropDownList ID="drp_template" runat="server" Width="180px">
                                        <asp:ListItem Value="100" Text="None" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Default Web 2.0 Style"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Template 2"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Template 3"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Template 4"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Template 5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Max Char Url Length</strong>: 0 for no restriction.
                                        (restrict number of title characters to be used in urls)</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Dynamic Url Max Length:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_max_url_length" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_max_url_length"
                                        Display="Dynamic" ErrorMessage="required" ToolTip="required." ValidationGroup="val_config">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_max_url_length"
                                        ErrorMessage="must be numeric" SetFocusOnError="true" Display="dynamic" EnableClientScript="true"
                                        ValidationGroup="val_config" ValidationExpression="(\d+)" />
                                </td>
                            </tr>
                            
                           
                            <tr>
                                <th style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Site Settings
                                </th>
                                <th style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    ----
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; text-align: left; vertical-align: middle; width: 100%">
                                    <span class="small_text"><strong>Place tracking script e.g Google Analytic Script.</strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: right; vertical-align: middle; width: 30%">
                                    Tracking Script:
                                </td>
                                <td style="padding: 5px; text-align: left; vertical-align: middle; width: 70%">
                                    <asp:TextBox ID="txt_tracking" TextMode="MultiLine" runat="server" Height="50" Width="500px"></asp:TextBox>
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
