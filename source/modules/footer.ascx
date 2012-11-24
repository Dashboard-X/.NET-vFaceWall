<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footer.ascx.cs" Inherits="modules_footer" %>
<div class="ajax_out_box ui-corner-all">
    <div class="hor_lst" style="padding: 10px 0px;">
        <div style="float: left; width: 70%;">
            <ul>
                <li><a href='<%=Config.GetUrl("terms.aspx") %>' class="mini-text reverse">
                    <%= Resources.vsk.termsofuse%></a></li>
                <li><a href='<%=Config.GetUrl("privacy.aspx") %>' class="mini-text reverse">
                    <%= Resources.vsk.privacypolicy%></a></li>

                <li><a href='<%=Config.GetUrl("contactus.aspx") %>' class="mini-text reverse">
                    <%= Resources.vsk.contactus%></a></li>
            </ul>
        </div>
        <div style="float: right; width: 29%;">
            <div class="item_r">
                <span class="mini-text reverse">.NET vFaceWall 1.0.0</span><br />

                <span class="mini-text reverse">Written by <a href="http://www.mediasoftpro.com/" target="_blank" class="mini-text reverse underline">www.mediasoftpro.com</a></span>
                <br />
                <a rel="license" href="http://creativecommons.org/licenses/by/3.0/"><img alt="Creative Commons License" style="border-width:0" src="http://i.creativecommons.org/l/by/3.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by/3.0/">Creative Commons Attribution 3.0 Unported License</a>.
            </div>
        </div>
        <div class="clear"></div>


    </div>

</div>

<%= Site_Settings.Site_Analytics %>