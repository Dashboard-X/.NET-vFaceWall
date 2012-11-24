<%@ Control Language="C#" AutoEventWireup="true" CodeFile="macc.ascx.cs" Inherits="myaccount_modules_macc" %>
<div class="item">
    <div id="msg" runat="server">
    </div>
    <!-- main content section -->
    <div class="bx_br_bt" style="padding: 6px 5px;">
        <a href="#" class="medium-text bold nounderline"> <i id="icon_01" class="icon-chevron-down"></i> <%= Resources.vsk.changepassword %></a>
    </div>
    <div id="pnl_01" class="bx_br_bt">
        <div class="pd_10">
            <div class="item_pad_2">
                User Name: <strong><span id="usr" runat="server"></span></strong>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.verifyoldpassword %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_opassword" TextMode="Password" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txt_opassword"
                    ErrorMessage="required" ToolTip="required" Display="Dynamic" SetFocusOnError="true"
                    ValidationGroup="val_chpass"></asp:RequiredFieldValidator>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.newpassword %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_npassword" TextMode="Password" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_npassword"
                    ErrorMessage="required" ToolTip="required" Display="Dynamic" SetFocusOnError="true"
                    ValidationGroup="val_chpass"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_npassword"
                    Display="Dynamic" EnableClientScript="true" ErrorMessage="Atleast 6-20 chars"
                    SetFocusOnError="true" ValidationExpression="^[\d\w]{6,20}$" ValidationGroup="val_chpass">
                </asp:RegularExpressionValidator>
            </div>
            <div class="item_pad_2">
                 <%= Resources.vsk.retypenewpassword%>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_confirmpassword" TextMode="Password" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txt_confirmpassword"
                    ErrorMessage="required" ToolTip="required" ValidationGroup="val_chpass" SetFocusOnError="true"
                    Display="dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_npassword"
                    SetFocusOnError="true" ControlToValidate="txt_confirmpassword" Display="Dynamic"
                    ErrorMessage="Password not matched" ValidationGroup="val_chpass">
                </asp:CompareValidator>
            </div>
            <div class="item_pad_2">
                <asp:Button ID="btn_change" ValidationGroup="val_chpass" runat="server"
                    OnClick="btn_change_Click" />
            </div>
        </div>
    </div>
    <!-- close main content section -->
    <div class="separator_10px">
    </div>
</div>
