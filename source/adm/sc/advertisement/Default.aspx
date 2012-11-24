<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    StylesheetTheme="adm" Theme="adm" MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_advertisement_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Advertisement | Manage Ads</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="item_pad_4_c">
                <img src="<%=  Config.GetUrl("images/loading.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
           <asp:Panel ID="pnl_main" runat="server">
             <div class="vkbox">
                <div class="vkbox-header">
                    <h3>
                        Manage Advertisement!</h3>
                </div>
                <div class="vkbox-body">
                    <div id="msg" runat="server">
                    </div>
                    <div class="item_pad_2_c">
                        Filter Records:
                        <asp:DropDownList ID="drp_sorttype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_filter_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="true">Non Adult</asp:ListItem>
                            <asp:ListItem Value="1">Adult</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="item_pad_2">
                        <asp:DataList ID="MyList" Width="100%" RepeatColumns="1" RepeatDirection="Vertical"
                            runat="server" OnCancelCommand="MyList_CancelCommand" OnEditCommand="MyList_EditCommand"
                            OnItemDataBound="MyList_ItemDataBound1" OnUpdateCommand="MyList_UpdateCommand">
                            <HeaderTemplate>
                                <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="width: 10%; border: none;">
                                                ID
                                            </th>
                                            <th style="width: 35%; border: none;">
                                                Name
                                            </th>
                                            <th style="width: 30%; border: none;">
                                                type
                                            </th>
                                            <th style="width: 15%; vertical-align: middle; border: none;">
                                                ---
                                            </th>
                                        </tr>
                                    </thead>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="table table-bordered table-striped">
                                    <tbody>
                                        <tr>
                                            <td style="width: 10%; border: none;">
                                                <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("adid") %>' />
                                            </td>
                                            <td style="width: 35%; border: none;">
                                                <asp:Label ID="lbl_value" runat="server" Text='<%# Eval("name") %>' />
                                            </td>
                                            <td style="width: 30%; border: none;">
                                                <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type") %>' />
                                            </td>
                                            <td style="width: 15%; vertical-align: middle; border: none;">
                                                <asp:LinkButton ID="lnk_edit" runat="server" CssClass="btn btn-primary btn-mini" CommandName="Edit"
                                                    Text="Edit"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="item_pad_2_c">
                                    <h5>
                                        ------- Edit Record --------</h5>
                                </div>
                                <div class="item">
                                    <div class="field_item_left">
                                        Name
                                    </div>
                                    <div class="field_item_right">
                                        <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("adid") %>' Visible="false" />
                                        <asp:TextBox ID="txt_name" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_name"
                                            Display="Dynamic" ErrorMessage="required." ToolTip="required." ValidationGroup="val_eaddadmin">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="field_item_left">
                                        Ad Script
                                    </div>
                                    <div class="field_item_right">
                                        <asp:TextBox ID="txt_adscript" TextMode="multiLine" Width="400px" Height="100px"
                                            runat="server" Text='<%# Eval("adscript") %>'></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_adscript"
                                            Display="Dynamic" ErrorMessage="required." ToolTip="required." ValidationGroup="val_eaddadmin">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="item_pad_4">
                                    <div class="field_item_left">
                                    </div>
                                    <div class="field_item_right">
                                        <asp:LinkButton ID="lnk_update" CssClass="btn btn-primary btn-mini" runat="server" ValidationGroup="val_eaddadmin"
                                            CommandName="update" Text="Update" OnClientClick="return confirm('Are you sure you want to update record');"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lnk_cancel" CssClass="btn btn-primary btn-mini" runat="server" CommandName="cancel"
                                            Text="Cancel"></asp:LinkButton>&nbsp;
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </EditItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div class="vkbox-footer">
                    <asp:LinkButton ID="lnk_Prev" CssClass="btn btn-primary btn-mini" runat="server"
                        Text="Prev" OnClick="lnk_Prev_Click"></asp:LinkButton>
                    <asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemDataBound="rptPages_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                Text='<%# Container.DataItem %>' runat="server"></asp:LinkButton>&nbsp;
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="lnk_Next" runat="server" CssClass="btn btn-primary btn-mini"
                        Text="Next" OnClick="lnk_Next_Click"></asp:LinkButton>
                </div>
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
                                No Advertisement Script Found!</h4>
                        </div>
                        <div class="separator_10px">
                        </div>
                        <div class="separator_10px">
                        </div>
                    </div>
                </div>
            </asp:Panel>
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
