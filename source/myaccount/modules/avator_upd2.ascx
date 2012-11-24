<%@ Control Language="C#" AutoEventWireup="true" CodeFile="avator_upd2.ascx.cs" Inherits="myaccount_modules_avator_upd2" %>
<div id="lst" runat="server"></div>
<div id="myModal" class="modal hide fade">
    <div class="modal-header">
        <button class="close" data-dismiss="modal">
            &times;</button>
        <h3>
            Upload Profile Photo</h3>
    </div>
    <div class="modal-body">
        <div class="form-horizontal">
            <fieldset>
                <div class="control-group">
                    <label class="control-label" for="<%= ph1.ClientID %>">
                        Browse Photo:</label>
                    <div class="controls">
                        <asp:FileUpload ID="ph1" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="modal-footer">
        <a href="#" class="btn" data-dismiss="modal">Close</a>
        <asp:Button ID="btn_save" runat="server" Text="Save Changes" OnClick="btn_save_Click" />
    </div>
</div>
