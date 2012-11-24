<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewErrorDetail.aspx.cs"
    Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_Log_ViewErrorDetail" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title> <%= Site_Settings.Website_Title %> | Log | Error Detail</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Error Log Detail</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_hd">
                Detail Error Report
            </div>
            <div class="bx_bd">
                <div class="item_pad_2" id="pnl_error" runat="server">
                    <div id="err">
                        <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Panel ID="pnl_add" runat="server">
                    <div class="item_pad_2">
                        <b>ID:</b>
                    </div>
                    <div class="item_pad_2">
                        <asp:Label ID="lbl_id" runat="server"></asp:Label>
                    </div>
                    <div class="item_pad_2">
                        <b>Error Message:</b>
                    </div>
                    <div class="item_pad_2">
                        <asp:Label ID="lbl_message" runat="server"></asp:Label>
                    </div>
                    <div class="item_pad_2">
                        <b>Url:</b>
                    </div>
                    <div class="item_pad_2">
                        <asp:Label ID="lbl_url" runat="server"></asp:Label>
                    </div>
                    <div class="item_pad_2">
                        <b>Stack Trace:</b>
                    </div>
                    <div class="item_pad_2">
                        <asp:Label ID="lbl_stack" runat="server"></asp:Label>
                    </div>
                    <div class="item_pad_2">
                        <b>Report Date:</b>
                    </div>
                    <div class="item_pad_2">
                        <asp:Label ID="lbl_addeddate" runat="server"></asp:Label>
                    </div>
                    <div class="item_pad_2">
                         <asp:Button ID="btn_delete" runat="server" Text="Delete"
                                OnClick="btn_delete_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
