<%@ Page Language="C#" AutoEventWireup="true" Theme="adm" StylesheetTheme="adm" CodeFile="memberdetail.aspx.cs"
    MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_memberdetail" %>

<%@ Register Src="../modules/member_mgt.ascx" TagName="member_mgt" TagPrefix="uc4" %>

<%@ Register Src="modules/profile.ascx" TagName="profile" TagPrefix="uc1" %>
<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <title> <%= Site_Settings.Website_Title %> | Administration | Member Detail</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Member Administration</h3>
    </div>
    <div class="separator_10px">
    </div>
    <div class="item" id="main" runat="server">
     <div class="item_pad_2">
            <uc4:member_mgt ID="Member_mgt1" runat="server"></uc4:member_mgt>
        </div>
        <div class="item">
            <uc1:profile ID="profile1" runat="server" />
        </div>
       
    </div>
    
</asp:Content>
