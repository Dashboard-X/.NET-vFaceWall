<%@ Control Language="C#" AutoEventWireup="true" CodeFile="createaccount.ascx.cs"
    Inherits="modules_createaccount" %>
<script type="text/javascript">
    function open_window(url) {
        window.open(url, null, 'height=530,width=600,top=100,left=100,channelmode=0,directories=0,fullscreen=0,location=0,menubar=0,resizable=0,scrollbars=1,status=0,titlebar=0,toolbar=0');
        return false;
    }

    function validate_agreement(obj) {

        //Executes all the validation controls associated with group1 validaiton Group1. 
        var flag = Page_ClientValidate('val_registration');
        if (flag) {
            var status = $(obj).attr('checked') ? 1 : 0;
            if (status == 0) {
                alert("Accept terms of use &amp; privacy policy before continue");
                flag = false;
            }
        }
        return flag;
    } 
</script>
<div id="mn" runat="server">
    <div class="vkbox vkbox-fixed">
        <div class="vkbox-header">
            <h3>
                <%= Resources.vsk.createaccount %></h3>
        </div>
        <asp:Panel ID="pnl_registerationsection" CssClass="pd_10" runat="server" DefaultButton="btn_register">
            <div class="vkbox-body">
                <div id="msg" runat="server">
                </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group" id="adm" runat="server">
                            <label class="control-label" for="<%=drp_acc.ClientID %>">
                                Account Type:</label>
                            <div class="controls">
                                <asp:DropDownList ID="drp_acc" Width="300px" runat="server">
                                    <asp:ListItem Value="0" Selected="True">Normal User</asp:ListItem>
                                    <asp:ListItem Value="1">Administrator</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%=lUserName.ClientID %>">
                                <%= Resources.vsk.username%>:</label>
                            <div class="controls">
                                <asp:TextBox ID="lUserName" placeholder="Enter User Name" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" CssClass="mini-text red bold" runat="server"
                                    ControlToValidate="lUserName" Display="Dynamic" ErrorMessage="required." SetFocusOnError="true"
                                    ToolTip="required." ValidationGroup="val_registration">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="UserNameRequiredFormat" CssClass="mini-text red bold"
                                    runat="server" ControlToValidate="lUserName" Display="Dynamic" EnableClientScript="true"
                                    ErrorMessage="Atleast 6 - 20 chars." ToolTip="Atleast 6 - 15 chars." SetFocusOnError="true"
                                    ValidationExpression="[a-zA-Z0-9]{6,15}" ValidationGroup="val_registration">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%=lPassword.ClientID %>">
                                Password:</label>
                            <div class="controls">
                                <asp:TextBox ID="lPassword" placeholder="Enter Password" MaxLength="20" runat="server"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="lPassword"
                                    ErrorMessage="required." ToolTip="required." Display="Dynamic" SetFocusOnError="true"
                                    ValidationGroup="val_registration" CssClass="mini-text red bold"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="lPassword"
                                    Display="Dynamic" EnableClientScript="true" ErrorMessage="Password must contain one digit with chars limit 6 to 20"
                                    SetFocusOnError="true" ValidationExpression="^(?=[^\d_].*?\d)\w(\w|[!@#$%]){6,20}"
                                    ValidationGroup="val_registration" CssClass="mini-text red bold">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%=ConfirmPassword.ClientID %>">
                                <%= Resources.vsk.confirm%>:</label>
                            <div class="controls">
                                <asp:TextBox ID="ConfirmPassword" MaxLength="20" placeholder="Confirm Password" runat="server"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="required." ToolTip="required." ValidationGroup="val_registration"
                                    SetFocusOnError="true" Display="dynamic" CssClass="mini-text red bold"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="lPassword"
                                    SetFocusOnError="true" ControlToValidate="ConfirmPassword" Display="Dynamic"
                                    ErrorMessage="Password not matched." ValidationGroup="val_registration" CssClass="mini-text red bold">
                                </asp:CompareValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%=Email.ClientID %>">
                                <%= Resources.vsk.email%>:</label>
                            <div class="controls">
                                <asp:TextBox ID="Email" placeholder="Enter Email Address" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    Display="dynamic" ErrorMessage="required." ToolTip="required." SetFocusOnError="true"
                                    ValidationGroup="val_registration" CssClass="mini-text red bold"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="EmailRequiredFormat" runat="server" ControlToValidate="Email"
                                    Display="dynamic" ErrorMessage="Invalid email." SetFocusOnError="true" ToolTip="A valid email address is required."
                                    ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                                    ValidationGroup="val_registration" CssClass="mini-text red bold">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%=drp_country.ClientID %>">
                                <%= Resources.vsk.country%>:</label>
                            <div class="controls">
                                <asp:DropDownList ID="drp_country" runat="server" AppendDataBoundItems="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" SetFocusOnError="true"
                                    Display="Dynamic" ValidationGroup="val_registration" ToolTip="required." ErrorMessage="required."
                                    ControlToValidate="drp_country" CssClass="mini-text red bold"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%=drp_country.ClientID %>">
                                <%= Resources.vsk.gender %>:</label>
                            <div class="controls">
                                <label class="radio inline">
                                    <asp:RadioButton ID="r_male" runat="server" Checked="true" GroupName="rd_gender" />Male
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton ID="r_female" runat="server" GroupName="rd_gender" />Female
                                </label>
                            </div>
                        </div>
                        <div class="controls">
                            <label class="checkbox">
                                <asp:CheckBox ID="chk_agree" runat="server" />I agree to the <a href="#" onclick="return open_window('terms_agreement.aspx');"
                                    title="Terms of Use">
                                    <%= Resources.vsk.termsofuse %></a> and <a href="#" onclick="return open_window('policy_agreement.aspx');"
                                        title="Privacy Policy">
                                        <%= Resources.vsk.privacypolicy %></a>.</label>
                        </div>
                        <div class="item_pad_4">
                            <a href="Login.aspx">
                                <%= Resources.vsk.signin %></a>
                        </div>
                        <div class="item_pad_4">
                            <a href="forgotpassword.aspx">
                                <%= Resources.vsk.forgotpassword %>?</a>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="vkbox-footer">
                <asp:Button ID="btn_register" SkinID="primarylarge" runat="server" OnClick="btn_register_Click1" />
                &nbsp;|&nbsp;<a rel="nofollow" class="btn btn-large" href="#" rel="tooltip" onclick="fb_login('<%= Config.GetUrl() %>','<%= this.Redirect_Url %>');return false;"
                    title="Login via facebook">Facebook</a>
            </div>
        </asp:Panel>
    </div>
</div>
