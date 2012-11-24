<%@ Control Language="C#" AutoEventWireup="true" CodeFile="member_mgt.ascx.cs" Inherits="admin_secure_modules_member_mgt" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="msg" runat="server">
        </div>
        <div class="item_pad_2">
            <div class="bx_bd1">
                <div class="item_pad_4_c">
                    <asp:LinkButton ID="lnk_enable" runat="server" OnClientClick="return confirm('Are you sure you want to perform selected operation');"
                        Text="Enable Member" OnClick="lnk_enable_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnk_disable" runat="server" OnClientClick="return confirm('Are you sure you want to perform selected operation');"
                        Text="Disable Member" OnClick="lnk_disable_Click"></asp:LinkButton>
                    &nbsp;|&nbsp;
                    <asp:LinkButton ID="lnk_deletevideo" runat="server" OnClientClick="return confirm('Are you sure you want to perform selected operation');"
                        Text="Delete Member" OnClick="lnk_deletevideo_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="item_pad_2">
            User Type: &nbsp;<asp:DropDownList ID="drp_type" runat="server">
                <asp:ListItem Value="0" Selected="True">Normal Users</asp:ListItem>
                <asp:ListItem Value="1">Administrators</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btn_upd" runat="server" Text="Update" OnClick="btn_upd_Click" />
        </div>
       
        <div class="item_pad_2">
            Status:
            <asp:Label ID="lbl_status" runat="server"></asp:Label>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
