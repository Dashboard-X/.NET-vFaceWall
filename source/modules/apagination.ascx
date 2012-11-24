<%@ Control Language="C#" AutoEventWireup="true" CodeFile="apagination.ascx.cs" Inherits="modules_apagination" %>
<script type="text/javascript">
    $(function () {
        $('#<%= plnks.ClientID %>').on({
            click: function (e) {
                ProcessPagination(this);
                return false;
            }
        }, '.pagination-css');
    });

    function ProcessPagination(obj) {
        var id = $(obj).attr('id');
        if (id != undefined) {
            var pid = null;
            if (id.indexOf("pl_") != -1) {
                pid = id.replace("pl_", "");
            }
            else if (id.indexOf("pn_") != -1) {
                pid = id.replace("pn_", "");
            }
            else if (id.indexOf("pg_") != -1) {
                pid = id.replace("pg_", "");
            }
            else if (id.indexOf("pp_") != -1) {
                pid = id.replace("pp_", "");
            }
            else if (id.indexOf("p_") != -1) {
                pid = id.replace("p_", "");
            }
            if (pid != null) {
                $("#<%= LoadStatusContainer %>").html('loading....');
                var tpages = $("#p_tpages").html();
                var psize = $("#p_psize").html();
                var pval = '<%= LoadParams %>';
                $.ajax({
                    type: 'GET',
                    url: '<%= LoadHandler %>',
                    data: pval + "&p=" + pid + "&tpages=" + tpages,
                    async: true,
                    cache: true,
                    success: function (msg) {
                        switch (msg) {
                            case "p400":
                                Display_Message("#<%= LoadStatusContainer %>", "Access Denied!.", 1, 1);
                                break;
                            default:
                                $("#<%= LoadStatusContainer %>").html('');
                                $("#<%= LoadContainer %>").html(msg);
                                // current page status
                                $("#p_pnum").html(pid);
                                Attach_Post_Events("<%= LoadContainer %>");
                                // load pagination links
                                LoadPaginationLinks(pid, tpages);
                        }
                    }
                });
            }
        }
    }

    function LoadPaginationLinks(obj, tpages) {
        $("#<%= plnks.ClientID %>").html('loading....');
        var pval = '<%= PaginationParams %>';
        $.ajax({
            type: 'GET',
            url: '<%= PaginationHandler %>',
            data: pval + "&p=" + obj + "&tpages=" + tpages,
            async: true,
            cache: true,
            success: function (msg) {
                $("#<%= plnks.ClientID %>").html(msg);
            }
        });
    }
</script>
<div id="plnks" class="pagination" runat="server">
</div>