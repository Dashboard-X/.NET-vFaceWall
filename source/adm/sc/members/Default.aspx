<%@ Page Language="C#" AutoEventWireup="true" Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master"
    CodeFile="Default.aspx.cs" Inherits="adm_sc_members_Default" %>

<%@ Register Src="../../../modules/item/chnl_itm.ascx" TagName="channelitem" TagPrefix="uc1" %>
<asp:Content ID="HeadContent2" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Administration | Manage Members</title>
</asp:Content>
<asp:Content ID="MainConten1t" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Member Management</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx1">
            <div class="bx_hd">
                <b>Search Records...</b>
            </div>
            <div class="bx_bd">
                <div class="item_pad_2">
                    Role: &nbsp;<asp:DropDownList ID="drp_type" runat="server">
                        <asp:ListItem Selected="true" Value="all">All</asp:ListItem>
                        <asp:ListItem Value="0">Normal Users</asp:ListItem>
                        <asp:ListItem Value="1">Administrators</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item_pad_2">
                    Select Country: &nbsp;<asp:DropDownList ID="drp_countries" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="all" Selected="true">All Countries</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item_pad_2">
                    Gender: &nbsp;<asp:DropDownList ID="drp_gender" runat="server">
                        <asp:ListItem Selected="true" Value="all">All</asp:ListItem>
                        <asp:ListItem Value="0">Male</asp:ListItem>
                        <asp:ListItem Value="1">Female</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item_pad_2">
                    Status: &nbsp;<asp:DropDownList ID="drp_isenabled" runat="server">
                        <asp:ListItem Selected="true" Value="all">All</asp:ListItem>
                        <asp:ListItem Value="1">Enabled Members</asp:ListItem>
                        <asp:ListItem Value="0">Disabled Members</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item_pad_2">
                    Search: &nbsp;<asp:TextBox ID="txt_search" runat="server" Width="624px"></asp:TextBox>
                </div>
                <div class="item_pad_2">
                    Filter: &nbsp;<asp:DropDownList ID="drp_filter" runat="server">
                        <asp:ListItem Value="0" Selected="true">All Time</asp:ListItem>
                        <asp:ListItem Value="1">Today</asp:ListItem>
                        <asp:ListItem Value="2">This Week</asp:ListItem>
                        <asp:ListItem Value="3">This Month</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item_pad_2">
                    <asp:Button ID="btn_submit" Text="Search" runat="server" OnClick="btn_submit_Click" />
                </div>
                <div class="item_pad_2_c">
                    <h3>
                        <asp:Label ID="lbl_records" runat="server"></asp:Label></h3>
                </div>
            </div>
        </div>
    </div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_hd">
                <ul id="tab00">
                    <li>
                        <asp:LinkButton ID="lnk_recent" runat="server" OnClick="lnk_recent_Click"><span>Recently Added</span></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="lnk_mostviewed" runat="server" OnClick="lnk_mostviewed_Click"><span>Most Viewed</span></asp:LinkButton></li>
                </ul>
                <div class="clear">
                </div>
            </div>
            <div class="bx_bd">
                <asp:Panel ID="pnl_main" runat="server">
                    <div class="item">
                        <asp:DataList ID="MyList" Width="100%" CssClass="tdivupper" RepeatColumns="1" RepeatDirection="Vertical"
                            runat="server" OnItemDataBound="MyList_ItemDataBound">
                            <ItemTemplate>
                                <uc1:channelitem ID="chnl1" isAdmin="true" runat="server" />
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
                                    No Members Found!</h4>
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
</asp:Content>
