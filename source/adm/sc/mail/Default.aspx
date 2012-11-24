<%@ Page Language="C#" AutoEventWireup="true" Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master"
    CodeFile="Default.aspx.cs" Inherits="adm_sc_mail_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Mail | Manage Mail Templates</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Mail Templates</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        You can customize email templates that can be use to send mails to user or site
        administrator on different actions.
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
            <div class="item_pad_2">
                <div class="item_pad_2_c">
                    <asp:HyperLink ID="lnk_c" runat="server" NavigateUrl="~/adm/sc/mail/Create.aspx"
                        Text="Create New Template" CssClass="xxmedium-text bold"></asp:HyperLink>
                    Filter Records:
                    <asp:DropDownList ID="drp_sorttype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_filter_SelectedIndexChanged">
                        <asp:ListItem Value="all">All Templates</asp:ListItem>
                        <asp:ListItem Value="general">General</asp:ListItem>
                        <asp:ListItem Value="video">Videos</asp:ListItem>
                        <asp:ListItem Value="blog">Blog Posts</asp:ListItem>
                        <asp:ListItem Value="photo">Photos &amp; Galleries</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="bx1">
                    <div class="bx_hd">
                        <b>List of Mail Templates</b>
                    </div>
                    <div class="bx_bd2">
                        <asp:Panel ID="pnl_main" runat="server">
                            <div class="item">
                                <asp:DataList ID="MyList" Width="100%" RepeatColumns="1" RepeatDirection="Vertical"
                                    runat="server" CssClass="tdivupper" OnItemDataBound="MyList_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <th style="width: 20%; border: none;">
                                                    Key
                                                </th>
                                                <th style="width: 50%; border: none;">
                                                    Description
                                                </th>
                                                <th style="width: 10%; border: none;">
                                                    Type
                                                </th>
                                                <th style="width: 10%; border: none;">
                                                    ...
                                                </th>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table class="tdiv">
                                            <tr>
                                                <td style="width: 20%; border: none;">
                                                    <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%# Eval("id") %>' />
                                                    <asp:Label ID="lbl_templatekey" runat="server" Text='<%# Eval("templatekey") %>' />
                                                </td>
                                                <td style="width: 50%; border: none;">
                                                    <asp:Label ID="lbl_description" runat="server" Text='<%# Eval("description") %>' />
                                                </td>
                                                <td style="width: 10%; border: none;">
                                                    <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type") %>' />
                                                </td>
                                                <td style="width: 10%; vertical-align: middle; border: none;">
                                                    <asp:HyperLink ID="lnk_detail" runat="server" Text="Detail"></asp:HyperLink>
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
