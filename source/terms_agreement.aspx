<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="terms_agreement.aspx.cs"
    Inherits="modules_policies_terms_agreement" %>

<%@ Register Src="~/modules/policies/terms_of_use.ascx" TagName="terms_of_use" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div class="pd_10">
        <uc1:terms_of_use ID="Terms_of_use1" runat="server" />
    </div>
    </form>
</body>
</html>
