<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" MasterPageFile="~/main.master"
    Inherits="Login" %>

<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc2" %>
<%@ Register Src="widgets/nav/main_nav.ascx" TagName="main_nav" TagPrefix="uc3" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="module ui-corner-all">
            <div class="vkbox vkbox-fixed">
                <div class="vkbox-header">
                    <h3>
                        <%= Resources.vsk.signin %></h3>
                </div>
                <div class="vkbox-body">
                    <div id="msg" runat="server">
                    </div>
                    <div class="form-horizontal">
                        <fieldset>
                            <div class="control-group">
                                <label class="control-label" for="<%=lUserName.ClientID %>">
                                    User Name:</label>
                                <div class="controls">
                                    <asp:TextBox ID="lUserName" Width="200px" placeholder="Enter User Name" MaxLength="15"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="lUserName"
                                        Display="Dynamic" ErrorMessage="*" ToolTip="*" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="val_login" CssClass="mini-text red bold">
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
                                    <asp:TextBox ID="lPassword" Width="200px" MaxLength="15" placeholder="Enter Password"
                                        runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="lPassword"
                                        ErrorMessage="*" ToolTip="*" ValidationGroup="val_login" SetFocusOnError="true"
                                        Display="dynamic" CssClass="mini-text red bold"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <label class="checkbox">
                                        <asp:CheckBox ID="chk_remember" runat="server" />
                                        <%= Resources.vsk.rememberme %></label>
                                </div>
                            </div>
                            <div class="item_pad_4">
                                <a href="Register.aspx">
                                    <%= Resources.vsk.createaccount %></a>
                            </div>
                            <div class="item_pad_4">
                                <a href="forgotpassword.aspx">
                                    <%= Resources.vsk.forgotpassword %>?</a>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="vkbox-footer">
                    <asp:Button ID="btn_login" SkinID="primarylarge" runat="server" ValidationGroup="val_login"
                        OnClick="btn_login_Click1" />&nbsp;|&nbsp;<a rel="nofollow" class="btn btn-large"
                            href="#" rel="tooltip" onclick="fb_login('<%= Config.GetUrl() %>','<%= this.Redirect_Url %>');return false;"
                            title="Login via facebook">Facebook</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
