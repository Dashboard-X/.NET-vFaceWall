<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tsearch.ascx.cs" Inherits="modules_tsearch" %>
<div class="module ui-corner-all">
    <div id="lst" style="padding: 5px;" runat="server">
        <div class="input-append">
            <asp:TextBox ID="search" placeholder="Search Tags or Topics or Keywords" runat="server"
                Width="500px"></asp:TextBox><asp:Button ID="btn_search" runat="server" Text="<%$ Resources:vsk,search %>" OnClick="btn_search_Click" />
        </div>
    </div>
</div>
