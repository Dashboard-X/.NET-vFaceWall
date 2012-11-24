<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/main.master" CodeFile="updatepass.aspx.cs"
    Inherits="updatepass" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register Src="widgets/nav/main_nav.ascx" TagName="main_nav" TagPrefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="<%= NavigationClass %>">
                <uc3:main_nav ID="main_nav1" runat="server" />
            </div>
            <div class="<%= BodyClass %>">
                <div class="item">
                    <div class="module ui-corner-all">
                        <div class="heading">
                            <h3 id="cmt" runat="server">
                                Update Password
                            </h3>
                        </div>
                        <div class="pd_5">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div class="item_pad_4_c">
                                        <img src="<%=  Config.GetUrl("images/loading.gif") %>" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="upd" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="msg" runat="server">
                                    </div>
                                    <asp:Panel ID="pnl_fpass" runat="server" DefaultButton="btn_login">
                                        <div class="item_pad_4">
                                            <div class="field_item_left">
                                                <%= Resources.vsk.username %>:
                                            </div>
                                            <div class="field_item_right">
                                                <strong><asp:Label ID="lbl_username" runat="server"></asp:Label></strong>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div class="item">
                                            <div class="field_item_left">
                                                Enter New Password:
                                            </div>
                                            <div class="field_item_right">
                                                <asp:TextBox ID="Password" MaxLength="20" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="required." ToolTip="required." Display="Dynamic" SetFocusOnError="true"
                                                    ValidationGroup="val_registration"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Password"
                                                    Display="Dynamic" EnableClientScript="true" ErrorMessage="Atleast 6-20 chars"
                                                    SetFocusOnError="true" ValidationExpression="^[\d\w]{6,20}$" ValidationGroup="val_registration">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div class="item">
                                            <div class="field_item_left">
                                                <%= Resources.vsk.confirm%>:
                                            </div>
                                            <div class="field_item_right">
                                                <asp:TextBox ID="ConfirmPassword" MaxLength="20" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                                    ErrorMessage="required." ToolTip="required." ValidationGroup="val_registration"
                                                    SetFocusOnError="true" Display="dynamic"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password"
                                                    SetFocusOnError="true" ControlToValidate="ConfirmPassword" Display="Dynamic"
                                                    ErrorMessage="Password not matched." ValidationGroup="val_registration">
                                                </asp:CompareValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div class="item_pad_4">
                                            <div class="field_item_left">
                                            </div>
                                            <div class="field_item_right">
                                                <asp:Button ID="btn_login" ValidationGroup="val_login" runat="server" OnClick="btn_login_Click" />
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </asp:Panel>
                                
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="module_t_mrg ui-corner-all">
                        <uc1:h_468x60 ID="h_468x1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
