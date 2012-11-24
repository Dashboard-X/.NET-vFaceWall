<%@ Control Language="C#" AutoEventWireup="true" CodeFile="post.ascx.cs" Inherits="user_modules_post" %>
<script type="text/javascript">
    $(function () {
        $("#uwall").on({
            paste: function (e) {
                var el = $(this);
                setTimeout(function () {
                    var text = $(el).val();
                    var reg = /(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
                    if (text.match(reg)) {
                        var url = reg.exec(text);
                        $('#prvdata').html('Processing...');
                        Ajax_Process('<%= Preview_Handler_Path %>', "u=" + url[0], '#prvdata', 'POST');
                        $(el).val('')
                        $(el).attr('style', 'height:20px;width:570px;');
                    }
                }, 100);
            }
        }, '#<%=wpost.ClientID %>');

        $("#uactivities").on({
            mouseenter: function (e) {
                PostItemMEnter(this);
            },
            mouseleave: function (e) {
                PostItemMLeave(this);
            }
        }, '.anscmt');

        $("#uwall").on({
            click: function (e) {
                ProcessData();
                return false;
            }
        }, '#cmt_post');

        $("#uactivities").on({
            click: function (e) {
                PostRemove(this, '<%= Delete_Post_Handler_Path %>', '<%= Delete_Post_Params %>');
                return false;
            }
        }, '.rempost');

        $("#<%= widget.ClientID %>").on({
            click: function (e) {
                post_show(this);
                return false;
            }
        }, '#<%= wpost.ClientID %>');
    });

    function post_show(id) {
        $("#plinks").show();
        $(id).attr('style', 'height:60px;width:590px;');
    }

    function ProcessData() {
        var cnt = $('#postcontent').html();
        var value = $('#<%=wpost.ClientID %>').val();
        var note = '';
        if (cnt != null && $('#postcontent').is(':visible')) {
            if (value != null)
                note = value;
            value = cnt;
        }
        var pval = '<%= Params %>';

        if (value == '') {
            Display_Message("#pmsg", "Please Write Something to Post.", 1, 200);
            return;
        }

        Display_Message("#pmsg", "Processing...", 0, 50);
        Ajax_Process_PreAppend('<%= Handler_Path %>', pval + "&d=" + encodeURIComponent(value) + "&note=" + note, '#tlst', 'POST');
        $('#pmsg').html('');
        $('#<%=wpost.ClientID %>').val('Write Something');
        $('#<%=wpost.ClientID %>').attr('style', 'height:20px;width:570px;');
        $("#plinks").hide();
        $("#prvdata").html('');
    }

    function PostRemove(obj, handler, params) {
        var pid = $(obj).attr('id').replace("rem_post_", "");
        $.ajax({
            type: 'GET',
            url: handler,
            data: "aid=" + pid,
            async: true,
            cache: true,
            success: function (msg) {
                switch (msg) {
                    case "p400":
                        Display_Message('#ansid_' + id, "Access Denied!.", 1);
                        break;
                    case "p100":
                        Display_Message('#ansid_' + id, "Error occured!", 1);
                        break;
                    case "p101":
                        Display_Message('#ansid_' + id, "No post information available!", 1);
                        break;
                    default:
                        Display_Message('#ansid_' + pid, "Post has been removed successfully!", 1);
                        break;
                }
            }
        });
    }

    function PostItemMEnter(obj) {
        var id = $(obj).attr('id').replace("ansid_", "");
        if (id != undefined) {
            $("#del_post_" + id).show();
        }
    }

    function PostItemMLeave(obj) {
        var id = $(obj).attr('id').replace("ansid_", "");
        if (id != undefined) {
            $("#del_post_" + id).hide();
        }
    }
</script>
<div class="module ui-corner-all" id="widget" runat="server">
    <div id="glinks" runat="server">
    </div>
    <div id="pmsg">
    </div>
    <div class="item_pad_2">
        <asp:TextBox ID="wpost" SkinID="nm_txtbox" placeholder="Write Something"
            runat="server" Width="590px" Height="20px" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div id="prvdata" class="item_pad_2">
    </div>
    <div id="plinks" class="item" style="display: none;">
        <div class="item">
            <div style="float: left; width: 60%;">
            </div>
            <div style="float: right; width: 8%;">
                <a id="cmt_post" href="#" class="btn btn-small">
                    Post</a>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</div>
