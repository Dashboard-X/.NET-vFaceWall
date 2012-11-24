<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="policy_agreement.aspx.cs"
    Inherits="modules_policies_policy_agreement" %>

<%@ Register Src="modules/policies/privacy_policy.ascx" TagName="privacy_policy"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="pd_10">
        <uc1:privacy_policy ID="Privacy_policy1" runat="server" />
    </div>
    </form>
</body>
</html>
