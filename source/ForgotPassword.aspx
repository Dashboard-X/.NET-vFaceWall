<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ForgotPassword.aspx.cs"
    MasterPageFile="~/main.master" Inherits="ForgotPassword" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register Src="widgets/nav/main_nav.ascx" TagName="main_nav" TagPrefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="module ui-corner-all">
            <div id="msg" runat="server">
            </div>
            <asp:Panel ID="pnl_fpass" runat="server" DefaultButton="btn_login">
                <div class="vkbox vkbox-fixed">
                    <div class="vkbox-header">
                        <h3>
                            <%= Resources.vsk.forgotpassword %></h3>
                    </div>
                    <div class="vkbox-body">
                        <div class="form-horizontal">
                            <fieldset>
                                <div class="control-group">
                                    <label class="control-label" for="<%=UserName.ClientID %>">
                                        <%= Resources.vsk.username %>:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="UserName" placeholder="Enter User Name" MaxLength="20" runat="server"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            Display="Dynamic" ErrorMessage="required." CssClass="mini-text red bold" ToolTip="required." ValidationGroup="val_login">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="UserNameRequiredFormat" runat="server" ControlToValidate="UserName"
                                            Display="Dynamic" ErrorMessage="required" ToolTip="required." ValidationExpression="^[0-9a-zA-Z''-'\s]{1,40}$"
                                            ValidationGroup="val_login">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="item_pad_4">
                                    <a href="Login.aspx" class="underlinelink">
                                        <%= Resources.vsk.signin %>?</a>
                                </div>
                                <div class="item_pad_4">
                                    <a href="Register.aspx" class="underlinelink">
                                        <%= Resources.vsk.createaccount %></a>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="vkbox-footer">
                        <asp:Button ID="btn_login" SkinID="primarylarge" ValidationGroup="val_login" runat="server"
                            OnClick="btn_login_Click" />
                     
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnl_result" runat="server">
                <div class="module ui-corner-all">
                    <div class="item_pad_4">
                        <%= Resources.vsk.forgotpassword_sent_text %>
                    </div>
                    <div class="item_pad_4">
                        <a href="Login.aspx" class="underlinelink">
                            <%= Resources.vsk.signin %>?</a>
                    </div>
                    <div class="item_pad_4">
                        <a href="Register.aspx" class="underlinelink">
                            <%= Resources.vsk.createaccount %></a>
                    </div>
                    <div class="separator_10px">
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
