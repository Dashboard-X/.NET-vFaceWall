<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Theme="adm"
    StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" CodeFile="Detail.aspx.cs"
    Inherits="adm_sc_mail_Detail" %>

<asp:Content ID="hd" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Mail | Mail Template Detail</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../tinymce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            // General options
            // Location of TinyMCE script
            script_url: '../../../tiny_mce_gzip.ashx',
            mode: "textareas",
            theme: "advanced",
            plugins: "table,inlinepopups,preview",
            // Theme options
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,|,bullist,|,undo,redo,|,link,unlink,removeformat,code,preview",
            theme_advanced_buttons2: "",
            theme_advanced_buttons3: "",
            theme_advanced_buttons4: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,

            // Skin options
            skin: "o2k7",
            skin_variant: "silver"

        });
    </script>
    <div class="top_heading">
        <h3>
            Detail of Mail Template</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        You can use this page to customize each mail template.
    </div>
    <div class="item_pad_2" id="pnl_error" runat="server">
        <div id="err">
            <asp:Label ID="lbl_error" runat="server"></asp:Label>
        </div>
    </div>
    <div class="item_pad_2">
        <div id="bx1">
            <div class="bx_hd">
                <b>Review &amp; customize mail template....</b>
            </div>
            <div class="bx_bd">
                <div class="item_pad_2">
                    <b>Key:</b>
                </div>
                <div class="item_pad_2">
                    <asp:Label ID="lbl_key" runat="server"></asp:Label>
                </div>
                <div class="item_pad_2">
                    <b>Type:</b>
                </div>
                <div class="item_pad_2">
                    <asp:Label ID="lbl_type" runat="server"></asp:Label>
                </div>
                <div class="item_pad_2">
                    <b>Description:</b>
                </div>
                <div class="item_pad_2">
                    <asp:Label ID="lbl_description" runat="server"></asp:Label>
                </div>
                <div class="item_pad_2">
                    <span class="light_gray">You can use the following tags in your mail templates to be
                        dynamically updated.
                        <br />
                        e.g [username] -> update with username of user e.g tomy234 </span>
                </div>
                <div class="item_pad_2">
                    <b>Allowable Subject Tags:</b>
                </div>
                <div class="item_pad_2">
                    <asp:Label ID="lbl_subjecttags" runat="server"></asp:Label>
                </div>
                <div class="item_pad_2">
                    <b>Subject</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_subject" Width="500px" runat="server"></asp:TextBox>
                </div>
                <div class="item_pad_2">
                    <span class="light_gray">You can use the following tags in your mail templates to be
                        dynamically updated.
                        <br />
                        e.g [username] -> update with username of user e.g tomy234 </span>
                </div>
                <div class="item_pad_2">
                    <b>Allowable Tags</b>
                </div>
                <div class="item_pad_2">
                    <asp:Label ID="lbl_tags" runat="server"></asp:Label>
                </div>
                <div class="item_pad_2">
                    <b>Mail Content</b>
                </div>
                <div class="item_pad_2">
                    <textarea id="txt_content" runat="server" rows="20" style="width: 100%"></textarea>
                </div>
                <div class="item_pad_4">
                    <div class="field_item_left">
                    </div>
                    <div class="field_item_right">
                        <asp:Button ID="btn_add" OnClick="btn_add_Click" runat="server" Text="Update" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
