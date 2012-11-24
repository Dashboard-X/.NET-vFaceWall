<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tnav.ascx.cs" Inherits="modules_tnav" %>
<script type="text/javascript">
    $(function () {
        $('.nav-login').click(function (e) {
            e.stopPropagation();
        });
        $(".nav-login").on({
            click: function (e) {
                ProcessLogin('<%= Post_Handler %>', '<%=LoginUrl %>', '<%=IPBlockedUrl %>', '<%=RedirectUrl %>');
                return false;
            }
        }, '.vtoplogin');
    });
</script>
<div class="navbar navbar-fixed-top">
    <div class="navbar-inner-sm">
        <div class="vcnt row-fluid">
            <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
            </a>
            <ul class="nav pull-right">
                
                <li class="divider-vertical"></li>
                <%= Links %>
            </ul>
            <!-- /.nav-collapse -->
        </div>
    </div>
</div>
<div id="ft_dialog" class="modal hide fade" title="<%= Resources.vsk.facebooklogin %>">
</div>
