<%@ Control Language="C#" AutoEventWireup="true" CodeFile="activities.ascx.cs" Inherits="user_modules_activities" %>
<script type="text/javascript">
    $(function () {
        $("#uactivities").on({
            click: function (e) {
                var id = $(this).attr("id").replace("ans_", "");
                CmtBlock("qcmt_" + id, id, "Enter Comment");
                ldCmts(this);
                return false;
            }
        }, '.acmt');

        $("#uactivities").on({
            keyup: function (e) {
                var id = $(this).attr('id').replace("txt_", "");
                Count_Chars(this, '#cnt_' + id, 500);
                return false;
            }
        }, '.reptxt');

        $("#uactivities").on({
            mouseenter: function (e) {
                CmtItemMEnter(this);
            },
            mouseleave: function (e) {
                CmtItemMLeave(this);
            }
        }, '.vskcmtcnt');

        $("#uactivities").on({
            click: function (e) {
                var id = $(this).attr('id').replace("post_", "");
                var ausr = $("#ansusr_" + id).html();
                var params = '<%= Post_Params %>&id=' + id + '&ausr=' + ausr;
                var elid = "txt_" + id;
                PostV2(this, elid, '<%= Post_Handler %>', params, 0, "msg_" + id, "tlst_" + id, "", "cmt_tcmts_" + id);
                return false;
            }
        }, '.newcmt');

        $("#uactivities").on({
            click: function (e) {
                CmtVote(this, 1, '<%= Vote_Handler %>');
                var id = $(this).closest("div").attr("id").replace("cmt_action_", "");
                $("#cmtpl_" + id).addClass("badge badge-warning mini-text");
                return false;
            }
        }, '.cmt_vu');

        $("#uactivities").on({
            click: function (e) {
                CmtVote(this, 0, '<%= Vote_Handler %>');
                var id = $(this).closest("div").attr("id").replace("cmt_action_", "");
                $("#cmtpl_" + id).addClass("badge badge-important mini-text");
                return false;
            }
        }, '.cmt_vd');


        $("#uactivities").on({
            click: function (e) {
                ReplyBox(this, 'Enter Comment');
                return false;
            }
        }, '.cmt_rep');

        $("#uactivities").on({
            click: function (e) {
                var id = $(this).closest("div.anscmt").attr("id").replace("ansid_", "");
                var params = '<%= Remove_Params %>&id=' + id;
                RemoveCmtV2(this, '<%= Remove_Handler %>', params, "cmt_tcmts_" + id);
                return false;
            }
        }, '.cmt_remove');


        $('#uactivities').on({
            click: function (e) {
                var id = $(this).attr('id').replace("cmt_post_", "");
                var aid = $(this).closest("div.anscmt").attr("id").replace("ansid_", "");
                var ausr = $("#ansusr_" + aid).html();
                var params = '<%= Post_Params %>&id=' + aid + '&ausr=' + ausr;
                var elid = "rep_id_" + id;
                PostV2(this, elid, '<%= Post_Handler %>', params, id, "cmtrepmsg_" + id, "cmtrep_" + id, "repaction_" + id, "cmt_tcmts_" + aid);
                return false;
            }
        }, '.repcmt');

        $('#uactivities').on({
            click: function (e) {
                var aid = $(this).closest("div.anscmt").attr("id").replace("ansid_", "");
                var ausr = $("#ansusr_" + aid).html();
                var params = '<%= Flag_Params %>&id=' + aid + '&ausr=' + ausr;
                PostFlag(this, '<%= Flag_Handler %>', params);
                return false;
            }
        }, '.cmt_flag');

        // Load more
        $("#uactivities").on({
            click: function (e) {
                ldCmts(this);
                return false;
            }
        }, '.cmtldmore');


        $("#uactivities").on({
            click: function (e) {
                PVote(this, 1, '<%= ActivityLikeHandler %>', "actlk");
                return false;
            }
        }, '.actlike');
        $("#uactivities").on({
            click: function (e) {
                PVote(this, 0, '<%= ActivityLikeHandler %>', "actdlk");
                return false;
            }
        }, '.actdislike');

        $("#uactivities").on({
            click: function (e) {
                ldCmts(this);
                return false;
            }
        }, '.ldcmt');

        $("#uactivities").on({
            click: function (e) {
                var params = '<%= LoadActivityParams %>';
                LoadMoreV2(this, '<%= ActivityLoadHandler %>', params, 'cmt_tpages', 'cmt_pnum', 'cmt_loading', 'cmt_load_cnt', 'actldmore');
                return false;
            }
        }, '.actldmore');
    });

    function ldCmts(obj) {
        var aid = $(obj).closest("div.anscmt").attr("id").replace("ansid_", "");
        var tpages = $("#cmt_tpages_" + aid).html();
        if (tpages > 1)
            $("#pcmtldmore_" + aid).show();
        var ausr = $("#ansusr_" + aid).html();

        var params = '<%= Load_Params %>&id=' + aid + '&ausr=' + ausr;
        LoadMoreV2(obj, '<%= Load_Handler %>', params, 'cmt_tpages_' + aid, 'cmt_pnum_' + aid, 'cmt_loading_' + aid, 'cmt_load_cnt_' + aid, 'cmtldmore_' + aid);
    }

    function PVote(obj, act, handler, pnts) {
        var id = $(obj).closest("div").attr("id");
        if (id != undefined) {
            var lks = $("#" + pnts + "_" + id).html();
            var ausr = $("#actusr_" + id).html();
            //$("#citem_" + cid).show();
            $.ajax({
                type: 'GET',
                url: handler,
                data: "ausr=" + ausr + "&pts=" + lks + "&vt=" + act + "&id=" + id,
                async: true,
                cache: true,
                success: function (msg) {
                    switch (msg) {
                        case "p400":
                            Display_Message('#pmsg_' + id, "Access Denied!.", 1);
                            break;
                        case "p100":
                            Display_Message('#pmsg_' + id, "Error occured!", 1);
                            break;
                        case "p101":
                            Display_Message('#pmsg_' + id, "You can't like / dislike your own activity!", 1);
                            break;
                        case "p102":
                            Display_Message('#pmsg_' + id, "Sign In to post vote", 1);
                            break;
                        case "p103":
                            Display_Message('#pmsg_' + id, "You already liked / disliked this activity!", 1);
                            break;
                        default:
                            $("#" + pnts + "_" + id).html(msg);
                            break;
                    }
                }
            });

        }
    }

    function CmtBlock(obj, id, plholder) {
        var strVar = "";
        strVar += "<div class=\"item_pad_2 row-fluid\">";
        strVar += "<textarea name=\"cmtn_" + id + "\" placeholder=\"" + plholder + "\" rows=\"2\" cols=\"20\" id=\"txt_" + id + "\" class=\"nm_txtbox span12 reptxt\" style=\"height:30px;\">";
        strVar += "<\/textarea>";
        strVar += "<\/div>";
        strVar += "<div class=\"item row-fluid\">";
        strVar += "<div class=\"mini-text light\" style=\"float: left; width: 50%; padding: 4px 0px;\">";
        strVar += "<span  id=\"cnt_" + id + "\">500<\/span> characters remaining.";
        strVar += "<\/div>";
        strVar += "<div style=\"float: right; width: 8%;\">";
        strVar += "<a id=\"post_" + id + "\" href=\"#\" class=\"btn btn-small newcmt\">";
        strVar += "Post<\/a>";
        strVar += "<\/div>";
        strVar += "<div class=\"clear\">";
        strVar += "<\/div>";
        strVar += "<\/div>";
        $("#" + obj).html(strVar);
        $("#" + obj).show();
    }
</script>
<div class="module ui-corner-all" id="widget" runat="server">
    <div id="uactivities">
        <div id="tlst">
        </div>
        <div id="lst" runat="server">
        </div>
    </div>
</div>
