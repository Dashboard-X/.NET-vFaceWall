<%@ Control Language="C#" AutoEventWireup="true" CodeFile="advsearch.ascx.cs" Inherits="channel_modules_advsearch" %>
<div id="msg" runat="server">
</div>
<div class="module ui-corner-all" id="main" runat="server">
    <div class="vkbox-header">
        <h3 id="cmt" runat="server">
        </h3>
    </div>
    <div class="pd_5">
        <div class="form-horizontal">
            <fieldset>
                <div class="control-group">
                    <label class="control-label" for="<%=txt_term.ClientID %>">
                        Term:</label>
                    <div class="controls">
                        <asp:TextBox ID="txt_term" placeholder="Enter Search Term" MaxLength="100" runat="server"
                            Width="85%"></asp:TextBox>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="<%=drp_country.ClientID %>">
                        Country:</label>
                    <div class="controls">
                        <asp:DropDownList ID="drp_country" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Any</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="<%=drp_gender.ClientID %>">
                        Gender:</label>
                    <div class="controls">
                        <asp:DropDownList ID="drp_gender" runat="server">
                            <asp:ListItem Value="" Selected="True">Any</asp:ListItem>
                            <asp:ListItem Value="male">Male</asp:ListItem>
                            <asp:ListItem Value="female">Female</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <label class="checkbox">
                            <asp:CheckBox ID="chk_onlyphoto" runat="server" />
                            Have Photo
                        </label>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="<%=drp_order.ClientID %>">
                        Order By:</label>
                    <div class="controls">
                        <asp:DropDownList ID="drp_order" runat="server">
                            <asp:ListItem Value="register_date desc, views desc" Selected="True">Recent</asp:ListItem>
                            <asp:ListItem Value="views desc, register_date desc">Most Viewed</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="vkbox-footer">
        <asp:Button ID="btn_update" SkinID="primarylarge" runat="server" ValidationGroup="val_upload"
            OnClick="btn_post_Click" />
    </div>
</div>
