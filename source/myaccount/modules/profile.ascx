<%@ Control Language="C#" AutoEventWireup="true" CodeFile="profile.ascx.cs" Inherits="myaccount_modules_profile" %>
<%@ Register Src="avator_upd2.ascx" TagName="avator_upd" TagPrefix="uc1" %>
<div class="b_top">
    <asp:Button ID="btn_save" ValidationGroup="usr_gen_prf" runat="server" OnClick="btn_save_Click" />
</div>
<div id="msg" runat="server">
</div>
<div class="item">
    <div class="bx_br_bt" style="padding: 6px 5px;">
       <a onclick="ShowHidePanel('#pnl_01','#icon_01');return false;" href="#"
            class="medium-text bold nounderline"><i id="icon_01" class="icon-chevron-down"></i> <%= Resources.vsk.aboutme %></a>
    </div>
    <div id="pnl_01" class="bx_br_bt">
        <div class="pd_10">
            <div class="item_pad_2">
                <uc1:avator_upd ID="avator_upd1" runat="server" />
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.message_profile_05%>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_aboutme" onkeyup="Count_Chars(this,'#tabout_cnt',300);" runat="server"
                    TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="tabout_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.message_profile_06%>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_website" MaxLength="128" runat="server" Width="500px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="reg_web" SetFocusOnError="true" runat="server"
                    ControlToValidate="txt_website" Display="dynamic" ErrorMessage="invalid url"
                    ToolTip="Must be in format of http://www.example.com." ValidationExpression="^(https?://)?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?(([0-9]{1,3}\.){3}[0-9]{1,3}|([0-9a-z_!~*'()-]+\.)*([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\.[a-z]{2,6})(:[0-9]{1,4})?((/?)|(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$"
                    ValidationGroup="usr_gen_prf"></asp:RegularExpressionValidator>
            </div>
        </div>
    </div>
    <div class="bx_br_bt" style="padding: 6px 5px;">
      <a onclick="ShowHidePanel('#pnl_02','#icon_02');return false;" href="#"
            class="medium-text bold nounderline"><i id="icon_02" class="icon-chevron-right"></i> <%= Resources.vsk.message_profile_07 %></a>
    </div>
    <div id="pnl_02" class="bx_br_bt" style="display: none;">
        <div class="pd_10">
            <div class="item_pad_2">
                <%= Resources.vsk.firstName %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_firstname" MaxLength="50" runat="server" Width="350px"></asp:TextBox>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.lastname%>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_lastname" MaxLength="50" runat="server" Width="350px"></asp:TextBox>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.gender%>::
            </div>
            <div class="item_pad_2">
                <asp:DropDownList ID="drp_gender" runat="server" Width="100px">
                    <asp:ListItem Selected="True" Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.relationshipstatus %>:
            </div>
            <div class="item_pad_2">
                <asp:DropDownList ID="drp_rstatus" runat="server" Width="100px">
                    <asp:ListItem Selected="True" Value="Single">Single</asp:ListItem>
                    <asp:ListItem Value="Taken">Taken</asp:ListItem>
                    <asp:ListItem Value="Open">Open</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.age %>:
            </div>
            <div class="item_pad_2">
                <asp:RadioButton ID="btn_dis" runat="server" GroupName="age" />
                <%= Resources.vsk.message_profile_08 %><br />
                <asp:RadioButton ID="btn_nodis" runat="server" GroupName="age" />
                <%= Resources.vsk.message_profile_09 %>
            </div>
        </div>
    </div>
    <div class="bx_br_bt" style="padding: 6px 5px;">
       <a onclick="ShowHidePanel('#pnl_03','#icon_03');return false;" href="#"
            class="medium-text bold nounderline"><i id="icon_03" class="icon-chevron-right"></i>  <%= Resources.vsk.message_profile_10 %></a>
    </div>
    <div id="pnl_03" class="bx_br_bt" style="display: none;">
        <div class="pd_10">
            <div class="item_pad_2">
                <%= Resources.vsk.hometown %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_hometown" MaxLength="50" runat="server" Width="500px"></asp:TextBox>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.currentcity %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_curcity" MaxLength="50" runat="server" Width="500px"></asp:TextBox>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.message_profile_11 %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_zipcode" MaxLength="10" runat="server" Width="500px"></asp:TextBox>
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.country %>:
            </div>
            <div class="item_pad_2">
                <asp:DropDownList ID="drp_country" runat="server" Width="200px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="bx_br_bt" style="padding: 6px 5px;">
        <a onclick="ShowHidePanel('#pnl_04','#icon_04');return false;" href="#"
            class="medium-text bold nounderline"><i id="icon_04" class="icon-chevron-right"></i> <%= Resources.vsk.message_profile_12 %></a>
    </div>
    <div id="pnl_04" class="bx_br_bt" style="display: none;">
        <div class="pd_10">
            <div class="item_pad_2">
                <%= Resources.vsk.occupations %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_occupations" onkeyup="Count_Chars(this,'#th_cnt',300);" MaxLength="300"
                    runat="server" TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="th_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.companies %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_companies" onkeyup="Count_Chars(this,'#tcom_cnt',300);" MaxLength="300"
                    runat="server" TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="tcom_cnt">300</strong><%= Resources.vsk.charsleft %>.
            </div>
        </div>
    </div>
    <div class="bx_br_bt" style="padding: 6px 5px;">
        <a onclick="ShowHidePanel('#pnl_05','#icon_05');return false;" href="#"
            class="medium-text bold nounderline"> <i id="icon_05" class="icon-chevron-right"></i> <%= Resources.vsk.education %></a>
    </div>
    <div id="pnl_05" class="bx_br_bt" style="display: none;">
        <div class="pd_10">
            <div class="item_pad_2">
                <%= Resources.vsk.schools %>:
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_schools" MaxLength="300" onkeyup="Count_Chars(this,'#tsch_cnt',300);"
                    runat="server" TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="tsch_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
        </div>
    </div>
    <div class="bx_br_bt" style="padding: 6px 5px;">
       <a onclick="ShowHidePanel('#pnl_06','#icon_06');return false;" href="#"
            class="medium-text bold nounderline"> <i id="icon_06" class="icon-chevron-right"></i> <%= Resources.vsk.interests %></a>
    </div>
    <div id="pnl_06" class="bx_br_bt" style="display: none;">
        <div class="pd_10">
            <div class="item_pad_2">
                <asp:TextBox ID="txt_interests" MaxLength="300" runat="server" TextMode="MultiLine"
                    Width="500px" onkeyup="Count_Chars(this,'#tint_cnt',300);" Height="50px"></asp:TextBox>
                <br />
                <strong id="tint_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.message_profile_13 %>
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_movies" MaxLength="300" onkeyup="Count_Chars(this,'#tmov_cnt',300);"
                    runat="server" TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="tmov_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.message_profile_14 %>
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_musics" MaxLength="300" onkeyup="Count_Chars(this,'#tmus_cnt',300);"
                    runat="server" TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="tmus_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
            <div class="item_pad_2">
                <%= Resources.vsk.message_profile_15 %>
            </div>
            <div class="item_pad_2">
                <asp:TextBox ID="txt_books" MaxLength="300" runat="server" onkeyup="Count_Chars(this,'#tbo_cnt',300);"
                    TextMode="MultiLine" Width="500px" Height="50px"></asp:TextBox>
                <br />
                <strong id="tbo_cnt">300</strong>
                <%= Resources.vsk.charsleft %>.
            </div>
        </div>
    </div>
</div>
<div class="b_bottom">
    <asp:Button ID="btn_bsave" runat="server" ValidationGroup="usr_gen_prf" OnClick="btn_bsave_Click" />
</div>
