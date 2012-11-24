<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dictionary.aspx.cs" Theme="adm"
    StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_screening_Dictionary" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%=  Site_Settings.Website_Title %>
        | Administration | Manage Screening Dictionary</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Screening Dictionary</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        <p>
            You can use screening dictionary to</p>
        <ul>
            <li>Restrict some username to be available in registration form, e.g administrator,
                moderator</li>
            <li>Want to screen user data continously with some words, e.g sexual, fuck or any other
                word</li>
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
                        Add New Item
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
                                    Value:
                                </div>
                                <div class="field_item_right">
                                    <asp:TextBox ID="txt_value" runat="server"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_value"
                                        Display="Dynamic" ErrorMessage="required." ToolTip="required." ValidationGroup="val_valgroup">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="item">
                                <div class="field_item_left">
                                    Type:
                                </div>
                                <div class="field_item_right">
                                    <asp:DropDownList ID="drp_type" Width="300px" runat="server">
                                        <asp:ListItem Selected="True" Value="0">Screening Words</asp:ListItem>
                                        <asp:ListItem Value="1">Restricted UserNames</asp:ListItem>
                                    </asp:DropDownList>
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
                        <b>List of Dictionary Values</b>
                    </div>
                    <div class="bx_bd2">
                        <div class="item_pad_4_c">
                            Filter Records: &nbsp;<asp:DropDownList ID="drp_filter" OnSelectedIndexChanged="drp_filter_SelectedIndexChanged"
                                AutoPostBack="true" Width="300px" runat="server">
                                <asp:ListItem Selected="True" Value="0">Screening Words</asp:ListItem>
                                <asp:ListItem Value="1">Restricted UserNames</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Panel ID="pnl_main" runat="server">
                            <div class="item">
                                <asp:DataList ID="MyList" Width="100%" RepeatColumns="1" RepeatDirection="Vertical"
                                    runat="server" CssClass="tdivupper" OnDeleteCommand="MyList_DeleteCommand" OnItemDataBound="MyList_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <td style="width: 50%; border: none;">
                                                    Value
                                                </td>
                                                <td style="width: 30%; border: none;">
                                                    Type
                                                </td>
                                                <td style="width: 20%; border: none">
                                                    ....
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <td style="width: 50%; border: none;">
                                                    <asp:Label ID="lbl_id" runat='server' Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_value" runat="server" Text='<%# Eval("value") %>' />&nbsp;
                                                </td>
                                                <td style="width: 30%; border: none;">
                                                    <asp:Label ID="lbl_type" runat='server' Text='<%# Eval("type") %>'></asp:Label>
                                                </td>
                                                <td style="width: 30%; border: none;">
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
                                            No Screening Dictionary Record Found!</h4>
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
