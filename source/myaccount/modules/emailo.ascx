<%@ Control Language="C#" AutoEventWireup="true" CodeFile="emailo.ascx.cs" Inherits="myaccount_modules_emailo" %>

<script type="text/javascript">
    $(function() {
        $('#ctl00_MC_emailo1_r_nosend').click(function() {
            $('#ctl00_MC_emailo1_chk_lst').removeClass('item');
            $('#ctl00_MC_emailo1_chk_lst').addClass('ui-state-disabled');
            
            $('#ctl00_MC_emailo1_chk_lst :input').attr('disabled', true);
            //$('#ctl00_MC_chk_ccomment').attr('disabled', true);
        
        });
        
         $('#ctl00_MC_emailo1_r_send').click(function() {
            $('#ctl00_MC_emailo1_chk_lst').removeClass('ui-state-disabled');
            $('#ctl00_MC_emailo1_chk_lst').addClass('item');
            
            $('#ctl00_MC_emailo1_chk_lst :input').removeAttr('disabled');
            //$('#ctl00_MC_chk_ccomment').removeAttr('disabled');
        });
    });
    
  
</script>
<div id="msg" runat="server"></div>
<div class="b_top">
    <asp:Button ID="btn_save" ValidationGroup="usr_gen_prf" runat="server"
        OnClick="btn_save_Click" />
</div>
<div class="item">
    <div class="bx_br_bt" style="padding: 6px 5px;">
        <a onclick="ShowHidePanel('#pnl_01','#icon_01');return false;" href="#"
            class="medium-text bold nounderline"><i id="icon_01" class="icon-chevron-down"></i> <%= Resources.vsk.emailAddress%></a>
    </div>
    <div id="pnl_01" class="bx_br_bt">
        <div class="pd_10">
            <div class="item_pad_2">
                <%= Resources.vsk.message_emailoptions_05 %>
            </div>
            <div class="item_pad_2">
                <strong><%= Resources.vsk.current %> <%= Resources.vsk.emailAddress%>:</strong> <span id="cemail" runat="server"></span>
            </div>
            <div class="item_pad_2">
                New <%= Resources.vsk.emailAddress%>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_nemail" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txt_nemail"
                    Display="dynamic" ErrorMessage="required" ToolTip="required" SetFocusOnError="true"
                    ValidationGroup="val_emlopt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailRequiredFormat" runat="server" ControlToValidate="txt_nemail"
                    Display="dynamic" ErrorMessage="Invalid email." SetFocusOnError="true" ToolTip="A valid email address is required."
                    ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                    ValidationGroup="val_emlopt">
                </asp:RegularExpressionValidator>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.accountpassword %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_pass" runat="server" MaxLength="50" TextMode="Password" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txt_pass"
                    ErrorMessage="required" ToolTip="required" Display="Dynamic" SetFocusOnError="true"
                    ValidationGroup="val_emlopt"></asp:RequiredFieldValidator>
            </div>
            <div class="item_pad_2">
                <asp:Button ID="btn_send" ValidationGroup="val_emlopt" runat="server" Text="Send Confirmation"
                    OnClick="btn_send_Click" />
            </div>
        </div>
    </div>
    <div class="bx_br_bt" style="padding: 6px 5px;">
       <a onclick="ShowHidePanel('#pnl_02','#icon_02');" href="#"
            class="medium-text bold nounderline"><i id="icon_02" class="icon-chevron-right"></i> <%= Resources.vsk.message_emailoptions_06 %></a>
    </div>
    <div id="pnl_02" class="bx_br_bt" style="display: none;">
        <div class="pd_10">
            <div class="item_pad_2">
                <asp:RadioButton ID="r_send" runat="server" GroupName="seml" />&nbsp;<%= Resources.vsk.message_emailoptions_07 %>
            </div>
            <div id="chk_lst" runat="server" style="padding-left: 10px; line-height: 20px;">
                <asp:CheckBox ID="chk_vcomment" runat="server" />&nbsp;<%= Resources.vsk.message_emailoptions_08 %><br />
                <asp:CheckBox ID="chk_ccomment" runat="server" />&nbsp;<%= Resources.vsk.message_emailoptions_09 %><br />
                <asp:CheckBox ID="chk_pmsg" runat="server" />&nbsp;<%= Resources.vsk.message_emailoptions_10 %><br />
                <asp:CheckBox ID="chk_finvite" runat="server" />&nbsp;<%= Resources.vsk.message_emailoptions_11 %><br />
                <asp:CheckBox ID="chk_subscribe" runat="server" />&nbsp;<%= Resources.vsk.message_emailoptions_12 %><br />
            </div>
            <div class="item_pad_2">
                <asp:RadioButton ID="r_nosend" runat="server" GroupName="seml" />&nbsp;<%= Resources.vsk.message_emailoptions_13 %>
            </div>
        </div>
    </div>
    <!-- close main content section -->
</div>
<div class="separator_10px">
</div>
<div class="b_bottom">
    <asp:Button ID="btn_bsave" runat="server" ValidationGroup="usr_gen_prf"
        OnClick="btn_bsave_Click" />
</div>
