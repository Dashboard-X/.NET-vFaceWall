<%@ Page Language="C#" AutoEventWireup="true" Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master"
    CodeFile="ManageIP.aspx.cs" Inherits="adm_sc_ip_ManageIP" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Administration | Manage IP Addresses</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage IP Address</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        <p>
            You can use this form to</p>
        <ul>
            <li>Block certain IP addresses to be used for login or signup process</li>
        </ul>
    </div>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="item_pad_4_c">
                <img src="<%=  Config.GetUrl("images/loading.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div class="item_pad_2">
                <div id="bx">
                    <div class="bx_hd">
                        Block IP Address
                    </div>
                    <div class="bx_bd">
                        <div class="item_pad_2" id="pnl_error" runat="server">
                            <div id="err">
                                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                            </div>
                        </div>
                        <asp:Panel ID="pnl_add" runat="server" DefaultButton="btn_add">
                            <div class="item">
                                <div class="field_item_left">
                                    IP Address:
                                </div>
                                <div class="field_item_right">
                                    <asp:TextBox ID="txt_value" runat="server"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_value"
                                        Display="Dynamic" ErrorMessage="required." ToolTip="required." ValidationGroup="val_addadmin">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="item_pad_4">
                                <div class="field_item_left">
                                </div>
                                <div class="field_item_right">
                                    <asp:Button ID="btn_add" ValidationGroup="val_valgroup" runat="server" Text="Add Value"
                                        OnClick="btn_add_Click" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="item_pad_2">
                <div id="bx1">
                    <div class="bx_hd">
                        <b>List of Blocked IP Address</b>
                    </div>
                    <div class="bx_bd2">
                        <asp:Panel ID="pnl_main" runat="server">
                            <div class="item">
                                <asp:DataList ID="MyList" Width="100%" RepeatColumns="1" RepeatDirection="Vertical"
                                    runat="server" CssClass="tdivupper" OnDeleteCommand="MyList_DeleteCommand">
                                    <HeaderTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <th style="width: 10%; border: none;">
                                                    ID
                                                </th>
                                                <th style="width: 20%; border: none;">
                                                    IP Address
                                                </th>
                                                <th style="width: 50%; border: none;">
                                                    Date Blocked
                                                </th>
                                                <th style="width: 20%; border: none;">
                                                    ---
                                                </th>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <td style="width: 10%; border: none;">
                                                    <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("id") %>' />
                                                </td>
                                                <td style="width: 20%; border: none;">
                                                    <asp:Label ID="lbl_value" runat="server" Text='<%# Eval("ipaddress") %>' />
                                                </td>
                                                <td style="width: 50%; border: none;">
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("date_added","{0:R}") %>' />
                                                </td>
                                                <td style="width: 20%; vertical-align: middle; border: none;">
                                                    <asp:LinkButton ID="lnk_delete" CssClass="pagination_link" runat="server" CommandName="delete"
                                                        Text="Delete" OnClientClick="return confirm('Are you sure you want to delete record permanently');"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="pagination">
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="lnk_Prev" OnClick="lnk_Prev_Click" runat="server" Text="Prev"></asp:LinkButton></li>
                                    <asp:Repeater ID="rptPages" runat="server" OnItemDataBound="rptPages_ItemDataBound"
                                        OnItemCommand="rptPages_ItemCommand">
                                        <ItemTemplate>
                                            <li id="nav" runat="server">
                                                <asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                                    Text='<%# Container.DataItem %>' runat="server"></asp:LinkButton></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <li>
                                        <asp:LinkButton ID="lnk_Next" OnClick="lnk_Next_Click" runat="server" Text="Next"></asp:LinkButton></li>
                                </ul>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnl_norecord" runat="server">
                            <div class="box">
                                <div class="box_b">
                                    <div class="separator_10px">
                                    </div>
                                    <div class="separator_10px">
                                    </div>
                                    <div class="item_pad_4_c">
                                        <h4>
                                            No Block IP Found!</h4>
                                    </div>
                                    <div class="separator_10px">
                                    </div>
                                    <div class="separator_10px">
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
