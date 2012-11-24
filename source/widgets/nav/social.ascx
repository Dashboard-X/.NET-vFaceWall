<%@ Control Language="C#" AutoEventWireup="true" CodeFile="social.ascx.cs" Inherits="widgets_nav_social" %>
<div class="item_pad" id="widget" runat="server">
    <div class="module ui-corner-all">
        <div class="item_pad_4">
            <fb:like-box href="<%= Social_Settings.Facebook_url %>" width="300" show_faces="true"
                border_color="fff" stream="false" header="true"></fb:like-box>
        </div>
        <div class="item_pad_4_c">
            <a href="<%= Social_Settings.Facebook_url %>" target="_blank" title="<%= Resources.vsk.becomefanonfacebook %>">
                <img src="<%= Config.GetUrl("images/fb.png") %>" alt="<%= Resources.vsk.becomefanonfacebook %>" /></a>
            <a href="<%= Social_Settings.Twitter_url %>" target="_blank" title="<%= Resources.vsk.followusontwitter %>">
                <img src="<%= Config.GetUrl("images/tw.png") %>" alt="<%= Resources.vsk.followusontwitter %>" /></a>
        </div>
    </div>
</div>
