<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidateAdult.aspx.cs" MasterPageFile="~/main.master"
    Inherits="ValidateAdult" %>

<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc1" %>
<%@ Register Src="ads/s_336x280.ascx" TagName="s_336x280" TagPrefix="uc2" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="item">
            <div class="module ui-corner-all">
                <div class="heading">
                    <h3 id="cmt" runat="server">
                        <%= Resources.vsk.adultwarning %>
                    </h3>
                </div>
                <div class="item">
                    <div class="pd_5">
                        <div class="item_pad_2">
                            <%= Resources.vsk.adultwarningmessage %>
                        </div>
                        <div class="item_pad_2_c">
                            <asp:LinkButton runat="server" Cssclass="big-text bold" ID="lnk_enter" OnClick="lnk_enter_Click" />
                            &nbsp;|&nbsp;
                            <asp:HyperLink ID="lnk_cancel" Cssclass="big-text bold" runat="server"></asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
