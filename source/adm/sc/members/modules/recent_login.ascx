<%@ Control Language="C#" AutoEventWireup="true" CodeFile="recent_login.ascx.cs"
    Inherits="adm_sc_members_modules_recent_login" %>
<div class="alt_bx" id="widget" runat="server">
    <div class="item_pad_4">
        <strong>Recent Login Activity</strong>
    </div>
    <div class="item_pad_2">
        <asp:DataList ID="MyList" Width="100%" CssClass="tdivupper" RepeatColumns="1" RepeatDirection="Vertical"
            runat="server">
            <HeaderTemplate>
                <table class="tdiv">
                    <tr>
                        <th width="40%">
                            IP Address
                        </th>
                        <th width="50%">
                            Date
                        </th>
                        <th width="10%">
                            ...
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table class="tdiv">
                    <tr>
                        <td style="width: 40%;">
                            <asp:Label ID="lbl_categoryid" runat="server" Text='<%# Eval("ipaddress") %>' />
                        </td>
                        <td style="width: 50%;">
                            <asp:Label ID="lbl_categoryname" runat="server" Text='<%# Eval("date_added") %>' />
                        </td>
                        <td style="width: 10%;">
                            ----
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>
