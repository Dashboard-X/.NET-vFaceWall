<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cmt.ascx.cs" Inherits="modules_comments_cmt" %>
<%@ Register Src="../pagination_v2.ascx" TagName="pagination" TagPrefix="uc1" %>
<%@ Register Src="../apagination.ascx" TagName="apagination" TagPrefix="uc2" %>
<script type="text/javascript">
    var cmtwith =0;
    $(function () {
        var twidth = $('#vcomment').width();
        cmtwith = twidth - 20;
        $('#<%= cpost.ClientID %>').width(cmtwith);
        $("#<%= cpost.ClientID %>").on('click', function (e) {
            $("#cpaction").show();
            $(this).attr('style', 'height:50px;width:' + cmtwith + 'px;');
        });
        $("#<%= cpost.ClientID %>").on('keyup', function (e) {
            Count_Chars(this, '#widget_tc_t_cnt', 500);
        });
        $("#vcomment").on({
            mouseenter: function (e) {
                CmtItemMEnter(this);
            },
            mouseleave: function (e) {
                CmtItemMLeave(this);
            }
        }, '.vskcmtcnt');

        $("#vcomment").on({
            click: function (e) {
               CmtVote(this, 1,'<%= Vote_Handler %>');
               return false;
            }
        }, '.cmt_vu');
         $("#vcomment").on({
            click: function (e) {
               CmtVote(this, 0,'<%= Vote_Handler %>');
               return false;
            }
        }, '.cmt_vd');
      
        $("#vcomment").on({
            click: function (e) {
                ReplyBox(this,'<%= Text_Caption %>');
                return false;
            }
        }, '.cmt_rep');

        $("#vcomment").on({
            click: function (e) {
                RemoveCmt(this,'<%= Remove_Handler %>','<%= Remove_Params %>');
                return false;
            }
        }, '.cmt_remove');

        $("#vcomment").on({
            click: function (e) {
                 Post(this,'<%=cpost.ClientID %>','<%= Post_Handler %>','<%= Post_Params %>',0,'msg','tlst','cpaction');
                 return false;
            }
        }, '#cmt_post');

        $('.vskcmtcnt').on({
            click: function (e) {
                var id = $(this).attr('id');
                var rid = id.replace("cmt_post_", "");
                var elid = "rep_id_" + rid;
                Post(this,elid,'<%= Post_Handler %>','<%= Post_Params %>',rid,"cmtrepmsg_" + rid,"cmtrep_" + rid,"repaction_" + rid);
              return false;
            }
        }, '.repcmt');

        $('.vskcmtcnt').on({
            click: function (e) {
                PostFlag(this,'<%= Flag_Handler %>','<%= Flag_Params %>');
                return false;
            }
        }, '.cmt_flag');

        // Load more
        $("#vcomment").on({
            click: function (e) {
                 LoadMore(this,'<%= Load_Handler %>','<%= Load_Params %>');
                 return false;
            }
        }, '#cmt_ld_more');

        $('.itm_cross a').tooltip();
    });
    
</script>
<div id="vcomment">
    <div id="msg">
    </div>
    <div id="cmt" runat="server" class="bx_br_bt item_pad_4">
    </div>
    <div class="form-horizontal">
        <fieldset>
            <div class="help-block">
                <asp:TextBox ID="cpost" SkinID="nm_txtbox" runat="server" Height="30px" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div id="cpaction" class="help-block row-fluid" style="display: none;">
                <div style="float: left; width: 50%; padding: 4px 0px;">
                    <span id="widget_tc_t_cnt">500</span> characters remaining.
                </div>
                <div style="float: right; width: 8%;">
                    <a id="cmt_post" href="#" class="btn btn-small">Post</a>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="item_pad_4">
        <div id="tlst">
        </div>
        <div id="lst" runat="server">
        </div>
        <div id="pg" runat="server">
            <uc1:pagination ID="pagination1" runat="server" />
        </div>
        <div id="apg" runat="server">
            <uc2:apagination ID="apagination1" runat="server" />
        </div>
    </div>
</div>
