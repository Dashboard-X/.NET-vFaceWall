<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/adm/Login.master"
    Theme="adm" StylesheetTheme="adm" Inherits="adm_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Administration | Verify yourself</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="offset3">
        <div class="vkbox vkbox-fixed">
            <div class="vkbox-header">
                <h3>
                    Please Verify Yourself!</h3>
            </div>
            <div class="vkbox-body">
                <div id="msg" runat="server">
                </div>
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="<%= UserName.ClientID %>">
                                Login Name:</label>
                            <div class="controls">
                                <asp:TextBox ID="UserName" runat="server" MaxLength="30"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="req_usr" runat="server" ControlToValidate="UserName"
                                    Display="Dynamic" ErrorMessage="*" ValidationGroup="val_login">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="reg_usr" runat="server" ControlToValidate="UserName"
                                    Display="Dynamic" ErrorMessage="invalid" ToolTip="invalid" ValidationExpression="^[0-9a-zA-Z''-'\s]{1,20}$"
                                    ValidationGroup="val_login">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="<%= Password.ClientID %>">
                                Password:</label>
                            <div class="controls">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req_pass" runat="server" ControlToValidate="Password"
                                    ErrorMessage="*" ToolTip="*" ValidationGroup="val_login">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="vkbox-footer">
                <asp:Button ID="btn_validate" SkinID="large" ValidationGroup="val_login" runat="server"
                    Text="Login" OnClick="btn_validate_Click" />
            </div>
        </div>
    </div>
  
</asp:Content>
