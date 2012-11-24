<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav_v2.ascx.cs" Inherits="adm_sc_modules_nav_v2" %>
<script type="text/javascript">
    $(function () {
        $('#mega-1').dcVerticalMegaMenu({
            rowItems: '3',
            speed: 'fast',
            effect: 'show',
            direction: 'right'
        });
    });
</script>
<div class="item">
    <ul id="mega-1" class="mega-menu">
        <li><a href='<%= Config.GetUrl("adm/sc/") %>'>Home</a></li>
        <li><a href="#">My Account</a>
            <ul>
               
                <li><a href="#">Profile</a>
                    <ul>
                        <li><a id="myprofile" runat="server" href="#">My Profile</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/ChangePassword.aspx") %>'>Change Password</a></li>
                    </ul>
                </li>
            </ul>
        </li>
        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?f=3") %>'>Members</a>
            <ul>
                <li><a href="#">Registered</a>
                    <ul>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?f=1") %>'>Today</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?f=2") %>'>This Week</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?f=3") %>'>This Month</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?f=0") %>'>All Time</a></li>
                    </ul>
                </li>
                <li><a href="#">Options</a>
                    <ul>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/CreateAccount.aspx") %>'>Create Account</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?type=1") %>'>Administrators</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/members/Default.aspx?t=0") %>'>Disabled Members</a></li>
                    </ul>
                </li>
            </ul>
        </li>
        <li id="Li1" runat="server"><a href='<%=  Config.GetUrl("adm/sc/comments/Default.aspx") %>'>
            Comments</a>
            <ul>
                <li><a href='<%=  Config.GetUrl("adm/sc/comments/Default.aspx") %>'>Browse</a>
                    <ul>
                        <li><a href='<%= Config.GetUrl("adm/sc/comments/Default.aspx?f=1") %>'>Added Today</a></li>
                        <li><a href='<%= Config.GetUrl("adm/sc/comments/Default.aspx?f=2") %>'>Added This Week</a></li>
                        <li><a href='<%= Config.GetUrl("adm/sc/comments/Default.aspx?f=2") %>'>Added This Month</a></li>
                        <li><a href='<%= Config.GetUrl("adm/sc/comments/Default.aspx?f=0") %>'>All Time</a></li>
                    </ul>
                </li>
                <li><a href="#">Options</a>
                    <ul>
                        <li><a href='<%= Config.GetUrl("adm/sc/groups/Default.aspx?anp=0") %>'>UnApproved</a></li>
                        <li><a href='<%= Config.GetUrl("adm/sc/groups/Default.aspx?t=0") %>'>Disabled</a></li>
                    </ul>
                </li>
            </ul>
        </li>
        <li id="Li5" runat="server"><a href="#">Settings</a>
            <ul>
                <li><a href='<%=  Config.GetUrl("adm/sc/config/") %>'>Configurations</a>
                    <ul>
                        <li><a href='<%=  Config.GetUrl("adm/sc/config/Default.aspx") %>'>General</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/config/Features.aspx") %>'>Features</a></li>
                      
                        <li><a href='<%=  Config.GetUrl("adm/sc/config/Social.aspx") %>'>Social</a></li>
                       
                    </ul>
                </li>
                <li><a href="#">Options</a>
                    <ul>
                       
                        <li><a href='<%=  Config.GetUrl("adm/sc/mail/Default.aspx") %>' title="Manage Mail Templates">
                            Mail Templates</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/advertisement/Default.aspx") %>' title="Manage Advertisement">
                            Advertisement</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/ip/ManageIP.aspx") %>' title="Manage &amp; block ip address">
                            Block IP</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/screening/Dictionary.aspx") %>' title="Manage Screening Dictionary">
                            Dictionary</a></li>
                        <li><a href='<%=  Config.GetUrl("adm/sc/log/Err_Log.aspx") %>' title="View error log and perform action">
                            Error Log</a></li>
                    </ul>
                </li>
            </ul>
        </li>
      
        <li><a title="Send mail to all subscribers" href='<%= Config.GetUrl("adm/sc/email.aspx") %>'>Send Email</a>
        </li>
    
        <li><a href='<%=  Config.GetUrl("adm/sc/Logout.aspx") %>'>Log Out</a> </li>
    </ul>
</div>
