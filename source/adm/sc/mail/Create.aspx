<%@ Page Title="" ValidateRequest="false" Theme="adm" StylesheetTheme="adm" Language="C#"
    MasterPageFile="~/adm/sc/admin.master" AutoEventWireup="true" CodeFile="Create.aspx.cs"
    Inherits="adm_sc_mail_Create" %>

<asp:Content ID="hd" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Mail | Create Mail Template</title>
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
            Create New Mail Template</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item_pad_2">
        <p>
            You can use this page to generate new mail template that can be used for sending
            mails when any event occurs within website, e.g sending mail when new comment posted.
        </p>
        <p>
            <b>Main Procedure:</b><br />
            i: Template key (unique key) will be generated for each mail template, once template
            created. You can use template key to retrieve mail template information from database.
            using script in code as
            <br />
            System.Collections.Generic.List<struct_mailtemplates> lst = MailTemplateBLL.Get_Record(TemplateKey);
           <br />
           ii: Both subject and content tags to be assigned to dynamically adjust dynamic values with tags. tags should be separated with comma, see example below
           <br />
           subject tags: [username]
           content tags: [username],[password],[validationurl]
           <br />
           Note within template you can use these tags rather than actual values which will be replaced with actual values within script. e.g you will use
           <br />
           Dear [username],
           <br />
           will be translated to
           <br />
           Dear tigerwood, (actual username) at time of template processing.
           <br />
            Once template created successfully, next step is to create a function that should
            be call when any event fires, e.g registration completed. Most events are already
            written but you can write mail template for areas where you need to implement mail
            process. Sample code for mail function below.
        </p>
        <p>
            <br />
            <strong>private void MailTemplateProcess(string emailaddress, string username, string
                password,string key) </strong>
        </p>
        <p>
            <strong>{ </strong>
        </p>
        <p>
            <strong>System.Collections.Generic.List</strong><struct_mailtemplates><strong> 
            lst = MailTemplateBLL.Get_Record(&quot;USRREG&quot;);</strong></p>
        <p>
            <strong>&nbsp;if (lst.Count &gt; 0) </strong>
        </p>
        <p>
            <strong>{ </strong>
        </p>
        <p>
            <strong>//// list of subject keywords </strong>
        </p>
        <p>
            <strong>string subject_tags = &quot;\\[username\\]&quot;; </strong>
        </p>
        <p>
            <strong>string subject_values = username; </strong>
        </p>
        <p>
            <strong>//// prepare subject </strong>
        </p>
        <p>
            <strong>string subject = MailProcess.Process_Template(lst[0].Subject, subject_tags,
                subject_values);</strong></p>
        <p>
            <strong>&nbsp;//// list of content keywords </strong>
        </p>
        <p>
            <strong>string content_tags = &quot;\\[username\\],\\[password\\],\\[key_url\\]&quot;;</strong></p>
        <p>
            <strong>&nbsp;string content_values = username + &quot;,&quot; + password + &quot;,&quot;
                + key; </strong>
        </p>
        <p>
            <strong>string contents = MailProcess.Process_Template(lst[0].Contents, content_tags,
                content_values); </strong>
        </p>
        <p>
            <strong>MailProcess.Send_Mail(emailaddress, subject, contents); </strong>
        </p>
        <p>
            <strong>} </strong>
        </p>
        <p>
            }</p>
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item_pad_2">
        <div id="bx1">
            <div class="bx_hd">
                <b>Create New Template</b>
            </div>
            <div class="bx_bd">
                <div class="item_pad_2">
                    <b>Template Key:</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_key" runat="server" MaxLength="10"></asp:TextBox>
                    e.g USRREG01
                </div>
                <div class="item_pad_2">
                    <b>Type:</b>
                </div>
                <div class="item_pad_2">
                    <asp:DropDownList ID="drp_sorttype" runat="server">
                        <asp:ListItem Value="all">All Templates</asp:ListItem>
                        <asp:ListItem Value="general">General</asp:ListItem>
                        <asp:ListItem Value="video">Videos</asp:ListItem>
                        <asp:ListItem Value="blog">Blog Posts</asp:ListItem>
                        <asp:ListItem Value="photo">Photos &amp; Galleries</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item_pad_2">
                    <b>Description:</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_description" MaxLength="100" runat="server" TextMode="MultiLine"
                        Width="350" Height="50"></asp:TextBox>
                </div>
                <div class="item_pad_2">
                    <b>Subject Tags:</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_subjecttags" runat="server"></asp:TextBox>
                    e.g [username],[recieverusername] etc
                </div>
                <div class="item_pad_2">
                    <b>Subject:</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_subject" runat="server"></asp:TextBox>
                    e.g Message has been sent to [username]
                </div>
                <div class="item_pad_2">
                    <b>Content Tags:</b>
                </div>
                <div class="item_pad_2">
                    <asp:TextBox ID="txt_contenttags" runat="server"></asp:TextBox>
                    e.g [username],[recieverusername] etc
                </div>
                <div class="item_pad_2">
                    <b>Mail Content:</b>
                </div>
                <div class="item_pad_2">
                    <textarea id="txt_content" runat="server" rows="20" style="width: 100%"></textarea>
                </div>
                <div class="item_pad_4">
                    <div class="field_item_left">
                    </div>
                    <div class="field_item_right">
                        <asp:Button ID="btn_add" OnClick="btn_add_Click" runat="server" Text="Add Template" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
