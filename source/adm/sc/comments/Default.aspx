<%@ Page Title="" Language="C#" MasterPageFile="~/adm/sc/admin.master" StylesheetTheme="adm"
    Theme="adm" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adm_sc_comments_Default" %>

<%@ Register Src="../../../modules/comments/cmtitem.ascx" TagName="cmtitem" TagPrefix="uc2" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Administration | Manage Content Comments</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".vskcmtcnt").on({
                mouseenter: function (e) {
                    $(this).addClass("vskadm_txt");
                },
                mouseleave: function (e) {
                    $(this).removeClass("vskadm_txt");
                }
            }, '.cmtedit');

            $(".vskcmtcnt").on('click', '.cmtedit', function (e) {
                console.log(this);
                TBox(this);
            });

            $(".vskcmtcnt").on('blur', 'textarea', function (e) {
                RBox(this);
            });
        });

        function TBox(obj) {
            var id = $(obj).attr("id");
            var tid = id.replace("cmt_edit_", "cmt_tedit_");
            var input = $('<textarea />', { 'rows': '2', 'cols': '20', 'name': 'n' + tid, 'id': tid, 'class': 'vskadm_txt', 'value': decodeURIComponent($(obj).html()) });
            $(obj).parent().append(input);
            $(obj).remove();
            input.focus();
        }
        function RBox(obj) {
            var id = $(obj).attr("id");
            var cid = id.replace("cmt_tedit_", "");
            var value = $(obj).val();
            // update comment
            $.ajax({
                type: 'POST',
                url: '<%= EHandler %>',
                data: "cmt=" + encodeURIComponent(value) + "&cid=" + cid,
                async: true,
                cache: true,
                success: function (msg) {
                    switch (msg) {
                        case "p400":
                            Display_Message('#<%=msg.ClientID %>', "Access Denied!.", 1, 1);
                            break;
                        case "p100":
                            Display_Message('#<%=msg.ClientID %>', "Comment ID Not Provided!", 1, 1);
                            break;
                        case "p101":
                            Display_Message('#<%=msg.ClientID %>', "Comment Not Provided!", 1, 1);
                            break;
                        case "p102":
                            Display_Message('#<%=msg.ClientID %>', "Problem exist in comment text.", 1, 1);
                            break;
                        case "p103":
                            Display_Message('#<%=msg.ClientID %>', "Problem exist in comment text.", 1, 1);
                            break;
                        case "p104":
                            Display_Message('#<%=msg.ClientID %>', "Problem occured while processing comment!", 1, 1);
                            break;
                        default:
                            Display_Message('#<%=msg.ClientID %>', "Comment Updated!", 1, 1);
                            break;
                    }
                }
            });
            var tid = id.replace("cmt_tedit_", "cmt_edit_");
            var input = $('<p />', { 'id': tid, 'class': 'cmtedit', 'html': $(obj).val() });
            $(obj).parent().append(input);
            $(obj).remove();
        }
    </script>
    <div class="top_heading">
        <h3>
            Comment Management</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item_pad_2" id="src" runat="server">
        <div class="bx_bd">
            <div class="bx_br_bt" style="padding: 6px 5px;">
                <span id="icon_01" class="ui-icon ui-icon-triangle-1-e" style="float: left; margin-right: .3em;">
                </span><a onclick="ShowHidePanel('#pnl_01','#icon_01');return false;" href="#" class="medium-text reverse bold nounderline">
                    Search Records....</a>
            </div>
            <div id="pnl_01" class="bx_br_bt" style="display: none;">
                <div class="item_pad_2">
                    <div class="field_item_left">
                        Search:
                    </div>
                    <div class="field_item_right">
                        <asp:TextBox ID="txt_search" runat="server" SkinID="auto_srch" Width="300px"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="item_pad_2">
                    <div class="field_item_left">
                        Enable / Disable:
                    </div>
                    <div class="field_item_right">
                        <asp:DropDownList ID="drp_isenable" runat="server">
                            <asp:ListItem Selected="true" Value="2">All </asp:ListItem>
                            <asp:ListItem Value="1">Enabled</asp:ListItem>
                            <asp:ListItem Value="0">Disabled</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="item_pad_2">
                    <div class="field_item_left">
                        Approved / Unapproved:
                    </div>
                    <div class="field_item_right">
                        <asp:DropDownList ID="drp_approve" runat="server">
                            <asp:ListItem Selected="true" Value="2">All </asp:ListItem>
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Value="0">Disapproved</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="item_pad_2">
                    <div class="field_item_left">
                        Type:
                    </div>
                    <div class="field_item_right">
                        <asp:DropDownList ID="drp_type" runat="server">
                            <asp:ListItem Selected="true" Value="all">All</asp:ListItem>
                            <asp:ListItem Value="0">Videos &amp; Audio Files</asp:ListItem>
                            <asp:ListItem Value="1">Blog Post</asp:ListItem>
                            <asp:ListItem Value="2">Photos</asp:ListItem>
                            <asp:ListItem Value="3">Photo Gallery</asp:ListItem>
                            <asp:ListItem Value="11">QA</asp:ListItem>
                            <asp:ListItem Value="12">QA Answers</asp:ListItem>
                            <asp:ListItem Value="14">User Activities</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="item_pad_2">
                    <div class="field_item_left">
                        Filter:
                    </div>
                    <div class="field_item_right">
                        <asp:DropDownList ID="drp_filter" runat="server">
                            <asp:ListItem Value="0" Selected="true">All Time</asp:ListItem>
                            <asp:ListItem Value="1">Today</asp:ListItem>
                            <asp:ListItem Value="2">This Week</asp:ListItem>
                            <asp:ListItem Value="3">This Month</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="item_pad_2">
                    <div class="field_item_left">
                        Order:
                    </div>
                    <div class="field_item_right">
                        <asp:DropDownList ID="drp_order" runat="server">
                            <asp:ListItem Value="added_date desc" Selected="True">Recently Added</asp:ListItem>
                            <asp:ListItem Value="level desc">Levels</asp:ListItem>
                            <asp:ListItem Value="points desc">Most Liked / Voted</asp:ListItem>
                            <asp:ListItem Value="points asc">Most Disliked / Voted</asp:ListItem>
                            <asp:ListItem Value="username asc">User Name</asp:ListItem>
                            <asp:ListItem Value="isenabled asc">Enabled Comments</asp:ListItem>
                            <asp:ListItem Value="isenabled desc">Disable Comments</asp:ListItem>
                            <asp:ListItem Value="isapproved asc">Approved Comments</asp:ListItem>
                            <asp:ListItem Value="isapproved desc">Not Approved Comments</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="item_pad_2">
                    <div class="field_item_left">
                    </div>
                    <div class="field_item_right">
                        <asp:Button ID="btn_submit" OnClick="btn_submit_Click" runat="server" Text="Search">
                        </asp:Button>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="item_pad_2_c" id="lbl_records" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_bd">
                <asp:Panel ID="pnl_main" runat="server">
                    <div class="item_pad_4 bx_br_bt">
                        <div class="btn-group">
                            <asp:Button ID="btn_approve" runat="server" Text="Approve" OnClick="btn_approve_Click" />
                            <asp:Button ID="btn_enabled" runat="server" Text="Enable" OnClick="btn_enabled_Click" />
                            <asp:Button ID="btn_disable" runat="server" Text="Disable" OnClick="btn_disable_Click" />
                            <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click" />
                        </div>
                    </div>
                    <div class="item_pad_4_r bx_br_bt">
                        <div style="float: left; padding-left: 10px; width: 10%;" class="item">
                            <asp:CheckBox onclick="SelectAll(this)" ID="chk_inbox" runat="server" />
                            Select All
                        </div>
                        <div style="float: right; width: 80%;">
                            Page Size:
                            <asp:DropDownList ID="drp_pagesize" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drp_pagesize_SelectedIndexChanged">
                                <asp:ListItem Value="100" Selected="true">100</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                                <asp:ListItem Value="500">500</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="item">
                        <asp:DataList ID="MyList" CssClass="tdivupper" runat="server" Width="100%" OnItemDataBound="MyList_ItemDataBound"
                            RepeatDirection="Vertical" RepeatColumns="1">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="padding: 5px; width: 5%;">
                                            <asp:CheckBox ID="chk_inbox" runat="server" />
                                        </td>
                                        <td style="padding: 5px; width: 95%;">
                                            <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("commentid") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_profileid" runat="server" Text='<%# Eval("profileid") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_contentid" runat="server" Text='<%# Eval("videoid") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_isenabled" runat="server" Text='<%# Eval("isenabled") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_isapproved" runat="server" Text='<%# Eval("isapproved") %>' Visible="false"></asp:Label>
                                            <uc2:cmtitem ID="cmtitem1" isAdmin="true" runat="server" />
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
                                    No Comments Posted Yet!</h4>
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
