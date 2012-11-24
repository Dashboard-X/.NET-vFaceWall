<%@ Page Language="C#" AutoEventWireup="true" Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master"
    CodeFile="Err_Log.aspx.cs" Inherits="adm_sc_Log_Err_Log" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Log | Error Log</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Error Logs</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        You can use this form to
        <ul>
            <li>Trace errors generated while user browse your website and perform action against
                errors.</li>
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
            <div class="item_pad_2" id="pnl_error" runat="server">
                <div id="err">
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                </div>
            </div>
            <div class="item_pad_4_c">
                <asp:Button ID="btn_errorlog" OnClick="btn_delete_Click" runat="server" Text="Clear Log" />
            </div>
            <div class="item_pad_2">
                <div id="bx1">
                    <div class="bx_hd">
                        <b>List of error log</b>
                    </div>
                    <div class="bx_bd2">
                        <asp:Panel ID="pnl_main" runat="server">
                            <div class="item">
                                <asp:DataList ID="MyList" Width="100%" RepeatColumns="1" RepeatDirection="Vertical"
                                    runat="server" CssClass="tdivupper" OnDeleteCommand="MyList_DeleteCommand" OnItemDataBound="MyList_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <th style="width: 50%; border: none;">
                                                    Description
                                                </th>
                                                <th style="width: 25%; border: none;">
                                                    Date
                                                </th>
                                                <th style="width: 25%; border: none;">
                                                    ...
                                                </th>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <td style="width: 50%; border: none;">
                                                    <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                                    <asp:Label ID="lbl_value" runat="server" Text='<%# Eval("description") %>' />
                                                </td>
                                                <td style="width: 25%; border: none;">
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("added_date") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; vertical-align: middle; border: none;">
                                                    <asp:HyperLink ID="lnk_detail" runat="server" Text="Detail"></asp:HyperLink>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="lnk_delete" runat="server" CommandName="delete" Text="Delete"
                                                        OnClientClick="return confirm('Are you sure you want to delete record permanently');"></asp:LinkButton>
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
                                            No Record Found!</h4>
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
