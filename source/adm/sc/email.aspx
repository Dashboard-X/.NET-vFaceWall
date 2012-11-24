<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  Theme="adm" StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" CodeFile="email.aspx.cs" Inherits="adm_sc_email" %>
<asp:Content ID="hd" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Mail | Send Mail To Subscribers</title>
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
            Create And Send Mail To All Subscribers</h3>
    </div>
    <div class="separator_10px">
    </div>
   <div id="msg" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx1">
            <div class="bx_bd">
                <div class="item_pad_2">
                    Two tags available to utilitze in content, [username], [email]
                </div>
                <div class="item_pad_2">
                    <b>Subject:</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_subject" runat="server"></asp:TextBox>
                </div>
                <div class="item_pad_2">
                    <b>Body:</b>
                </div>
                <div class="item_pad_2">
                    <textarea id="txt_content" runat="server" rows="20" style="width: 100%"></textarea>
                </div>
                <div class="item_pad_4">
                    <div class="field_item_left">
                    </div>
                    <div class="field_item_right">
                        <asp:Button ID="btn_add" OnClick="btn_add_Click" runat="server" Text="Submit" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
