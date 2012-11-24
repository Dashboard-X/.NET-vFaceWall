<%@ Control Language="C#" AutoEventWireup="true" CodeFile="avator_upd.ascx.cs" Inherits="adm_sc_members_modules_avator_upd" %>
<img width="95" height="95" class="thumbnail" id="avator" runat="server" /><br />
<a data-toggle="modal" href="#myModal" class="btn btn-primary">Change</a>
<div id="myModal" class="modal hide fade">
    <div class="modal-header">
        <button class="close" data-dismiss="modal">
            &times;</button>
        <h3>
            Upload Photo</h3>
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
