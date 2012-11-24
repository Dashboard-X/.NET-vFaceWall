var newURL = window.location.protocol + "://" + window.location.host;
$(function () {
    /* Action Module V 2.0 Related */
    $(".actionbar").on({
        mouseenter: function (e) {
            $(".iclike").addClass("ui-adv-icon-gd_hover");
            $(".iclike").removeClass("ui-adv-icon-good");
        },
        mouseleave: function (e) {
            $(".iclike").removeClass("ui-adv-icon-gd_hover");
            $(".iclike").addClass("ui-adv-icon-good");
        }
    }, '.ilike');
    $(".actionbar").on({
        mouseenter: function (e) {
            $(".idislike").addClass("ui-adv-icon-bd_hover");
            $(".idislike").removeClass("ui-adv-icon-bad");
        },
        mouseleave: function (e) {
            $(".idislike").removeClass("ui-adv-icon-bd_hover");
            $(".idislike").addClass("ui-adv-icon-bad");
        }
    }, '.idislike');
    $('#vsk_action_mod button,#vsk_action_mod a').tooltip({ placement: 'bottom' });
    $(".actionbar").on({
        click: function (e) {
            $("#shw_sign_lgn").show();
            return false;
        }
    }, '.nologincss');

    /* Video Preview Info Script */
    var tg = 0;
    $(".vdinfo").on({
        click: function (e) {
            if (tg == 0) {
                $(".vinfoico").removeClass('icon-chevron-down');
                $(".vinfoico").addClass('icon-chevron-up');
                $(".vinfotxt").html("show less");
                $(".vsminfo").hide();
                $(".vfullinfo").show();
                tg = 1;
            }
            else {
                $(".vinfoico").removeClass('icon-chevron-up');
                $(".vinfoico").addClass('icon-chevron-down');
                $(".vinfotxt").html("show more");
                $(".vsminfo").show();
                $(".vfullinfo").hide();
                tg = 0;
            }
            return false;
        }
    }, '.vinfobtn');
});
$(document).scroll(function () {
    // If has not activated (has no attribute "data-top"
    if (!$('.subnav').attr('data-top')) {
        // If already fixed, then do nothing
        if ($('.subnav').hasClass('subnav-fixed')) return;
        // Remember top position
        var offset = $('.subnav').offset()
        $('.subnav').attr('data-top', offset.top);
    }

    if ($('.subnav').attr('data-top') - $('.subnav').outerHeight() <= $(this).scrollTop())
        $('.subnav').addClass('subnav-fixed');
    else
        $('.subnav').removeClass('subnav-fixed');
});
// Show / Hide Panels
function toggle_panel(index, pnl) {
    switch (index) {
        case 1:
            $(pnl).slideDown(1000);
            break;
        case 2:
            $(pnl).hide();
            break;
    }
}
function ShowHide(index, pnl) {
    switch (index) {
        case 1:
            $(pnl).show();
            break;
        case 2:
            $(pnl).hide();
            break;
    }
}
function toggle_tw_panel(index, pnl1, pnl2) {
    switch (index) {
        case 1:
            $(pnl1).show();
            $(pnl2).hide();
            break;
        case 2:
            $(pnl2).show();
            $(pnl1).hide();
            break;
    }
}
// search autocomplete
function src_ac(clid, surl) {
    $(clid).autocomplete({
        serviceUrl: surl,
        minChars: 2,
        delimiter: /(,|;)\s*/, // regex or character
        maxHeight: 400,
        width: 280,
        zIndex: 9999,
        deferRequestBy: 0, //miliseconds
        noCache: false //default is false, set to true to disable caching
    });
}
// Count text box characters   
function Count_Chars(obj, label, maxlength) {
    var len = obj.value.length;
    if (len >= maxlength) {
        obj.value = obj.value.substr(0, maxlength);
    }
    $(label).text(maxlength - len);
}
function ShowHidePanel(pnl, icon) {
    if ($(pnl).is(':visible')) {
        $(pnl).hide();
        $(icon).removeClass('icon-chevron-down');
        $(icon).addClass('icon-chevron-right');
    }
    else {
        $(pnl).show();
        $(icon).removeClass('icon-chevron-right');
        $(icon).addClass('icon-chevron-down');
    }
}
// check /uncheck all checkboxes in list
function SelectAll(CheckBoxControl) {
    var _value = CheckBoxControl.checked;
    if (_value == true) {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('MyList') > -1)) {
                document.forms[0].elements[i].checked = true;
            }
        }
    }
    else {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('MyList') > -1)) {
                document.forms[0].elements[i].checked = false;
            }
        }
    }
}


//* Ajax Related Operations
function Ajax_Process(path, params, id, tp) {
    $.ajax({
        type: tp,
        url: path,
        data: params,
        async: true,
        cache: true,
        success: function (msg) {
            $(id).html(msg);
        }

    });
}


function Ajax_Process_Append(path, params, id, tp) {
    $.ajax({
        type: tp,
        url: path,
        data: params,
        async: true,
        cache: true,
        success: function (msg) {
            $(id).append(msg);
        }
    });
}
function Ajax_Process_PreAppend(path, params, id, tp) {
    $.ajax({
        type: tp,
        url: path,
        data: params,
        async: true,
        cache: true,
        success: function (msg) {
            $(id).prepend(msg);
        }
    });
}
function Ajax_Process_v2(path, params, id, tp, loadingid) {
    $.ajax({
        type: tp,
        url: path,
        data: params,
        async: true,
        cache: true,
        success: function (msg) {
            $(id).html(msg);
            $('#' + loadingid).html('loading');
            ShowHide(2, '#' + loadingid);
        }
    });
}
function Ajax_Process_v3(path, params, id, tp, loadingid) {
    var message = '';
    $.ajax({
        type: tp,
        url: path,
        data: params,
        async: true,
        cache: true,
        success: function (msg) {
            ShowHide(2, '#' + loadingid);
            message = msg;
        }
    });
    return message;
}

/* Process Like | Dislike */
function Process_Advice(path, params, id, actionid, actiontype) {
    //toggle_panel(1, '#shw_lgn');
    // start posting ajax
    Ajax_Process(path, params, id, "GET");
    // disable like or dislike button
    if (actiontype == 0) {
        $(actionid).removeClass("ui-adv-icon-good");
        $(actionid).removeClass("ui-adv-icon-gd_hover");
        $(actionid).addClass("ui-fixed ui-adv-icon-good");

    } else {
        $(actionid).removeClass("ui-adv-icon-bad");
        $(actionid).removeClass("ui-adv-icon-bd_hover");
        $(actionid).addClass("ui-fixed ui-adv-icon-bad");
    }
    // disable action
}
/* Process Abuse Report */
function Process_Req(path, params, id, type) {
    toggle_panel(1, '#shw_lgn');
    // start posting ajax
    Ajax_Process(path, params, id, type);
}
/* Process Ajax Request with Animation */
function Process_Req_Animate(path, params, id, type, loadingid) {
    //$(id).hide("slide", { direction: "left" }, 500);
    //$(id).show("slide", { direction: "right" }, 500);
    Process_Req(path, params, id, type, loadingid);

}

/* Process Ajax Request With Loading Message*/
function Process_Req(path, params, id, type, loadingid) {
    Display_Message(id, "Loading...", 2, 50);
    //ShowHide(1, '#' + loadingid);
    // start posting ajax
    Ajax_Process_v2(path, params, id, type, loadingid);
}

/* Display processing message */
function Display_Processing(id) {
    $(id).html("<div style='padding:4px 0px;'>Processing....</div>");
}
/* Display Message */
function Display_Message_Pre(id, msg, tp) {
    switch (tp) {
        case 0:
            $(id).prepend("<div class='alert alert-error'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
        case 1:
            $(id).prepend("<div class='alert alert-success'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
        case 2:
            $(id).prepend("<div class='item_c' style='padding:20px 0px;'>" + msg + "</div>");
            break;
        case 3:
            $(id).prepend("<div class='alert alert-info'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
        case 4:
            $(id).prepend("<div class='alert'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
    }
}

/* Display Message */
function Display_Message(id, msg, tp, width) {
    switch (tp) {
        case 0:
            $(id).html("<div class='alert alert-error'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
        case 1:
            $(id).html("<div class='alert alert-success'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
        case 2:
            $(id).html("<div class='item_c' style='padding:20px 0px;'>" + msg + "</div>");
            break;
        case 3:
            $(id).html("<div class='alert alert-info'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
        case 4:
            $(id).html("<div class='alert'><button class='close' data-dismiss='alert'>×</button>" + msg + "</div>");
            break;
    }
}

function fb_login(u, redirect) {
    FB.login(function (response) {
        if (response.authResponse) {
            Process_Login(u, redirect);
        } else {
            console.log('User cancelled login or did not fully authorize.');
        }
    }, { scope: 'email,user_birthday,user_hometown,publish_stream' });
}

function Process_Login(u, redirect) {
    // build loading section
    loadingtext('#ft_dialog');
    $('#ft_dialog').modal({ show: true });
    FB.api('/me', function (response) {
        var fb_data = "uid=" + response.id + "&fn=" + response.first_name + "&ln=" + response.last_name + "&gn=" + response.gender + "&bt=" + response.birthday + "&eml=" + response.email;
        if (response.hometown != undefined)
            fb_data = fb_data + "&loc=" + response.hometown.name;
        if (response.username != undefined)
            fb_data = fb_data + "&uname=" + response.username;
        $.ajax({
            type: 'GET',
            url: u + "handlers/signup.ashx",
            data: fb_data,
            success: function (msg) {
                switch (msg) {
                    case "nouid":
                        document.location = u + "facebook/error.aspx?status=nouid";
                        break;
                    case "emailexist":
                        document.location = u + "facebook/error.aspx?status=emailexist";
                        break;
                    case "ucsuccess":
                        document.location = u + "myaccount/Default.aspx?status=success";
                        break;
                    case "csuccess":
                        document.location = u + "myaccount/Default.aspx?status=success&clg=true";
                        break;
                    case "nologinname":
                        document.location = u + "facebook/error.aspx?status=nologinname";
                        break;
                    case "ipblocked":
                        document.location = u + "IPBlocked.aspx";
                        break;
                    case "success":
                        if (redirect != "") {
                            document.location = redirect;
                        }
                        else
                            document.location = u + "myaccount/Default.aspx";
                        break;
                }
            }
        });
    });
}

function loadingtext(id) {
    var str = "";
    str += "<div class=\"modal-header\">";
    str += "<button class=\"close\" data-dismiss=\"modal\">&times;</button>";
    str += "<h3>Please wait for login via facebook.</h3>";
    str += "</div>";
    str += "<div class=\"modal-body\">";
    str += "<div class=\"item_pad_4_c\">";
    str += "<img src='..\/images\/loading\/loading7.gif\' style=\"width: 100px;   height: 100px;\" \/>";
    str += "<\/div>";
    str += "</div>";
    $(id).html(str);
}
function ShowMsg(id, message,messagetype,icontype) {
    var str = "";
    var message_class = "ui-state-error";
    var icon_message = "Alert";
    var icon_class = "ui-icon-alert";
    switch (messagetype) {
        case 0:
            message_class = "ui-state-error";
            break;
        case 1:
            message_class = "ui-state-highlight";
            break;
    }
    switch (icontype) {
        case 0:
            icon_class = "ui-icon-alert";
            icon_message = "Alert:";
            break;
        case 1:
            icon_class = "ui-icon-info";
            icon_message = "Info:";
            break;
        case 2:
            icon_class = "ui-icon-check";
            icon_message = "Success:";
            break;
    }
    str += "<div class=\"item_pad_2 ui-corner-all\"><div class=\"" + message_class + " ui-corner-all\">";
    str += "<div style=\"float:left; width:85%;\">";
    str += "<p><span class=\"ui-icon " + icon_class + "\" style=\"float: left; margin-right: .3em;\"></span>";
    str += "<strong>" + icon_message + "</strong>";
    str += " " + message + "<\/p><\/div><div style=\"float:right; width:10%; text-align:right;\"><a href=\"javascript:void(0)\" onclick=\"toggle_panel(2,'#" + id + "');\">close<\/a><\/div><div class=\"clear\"><\/div>";
    str += "<\/div><\/div>";
    $('#' + id).html(str);
}

function ConvertSize(size) {
    var csize = "";
    if (size > 1000000000) {
        csize = Math.round(size / 1000000000) + "G";
    } else if (size > 1000000) {
        csize = Math.round(size / 1000000) + "M";
    }
    else if (size > 1000) {
        csize = Math.round(size / 1000) + "K";
    }
    else {
        csize = size + "B";
    }

    return csize;
}

/* VSK 7.0 Related */
function ProcessLogin(handler, lurl, ipurl, redurl) {
    var ureg = /^[a-zA-Z0-9]{6,15}$/;
    var pa = $("#lgnsm_pass").val();
    var uname = $("#lgnsm_uname").val();
    if (!ureg.test(uname)) {
        alert("invalid user name");
        return false;
    }
    if (pa == '') {
        alert("password is required");
        return false;
    }
    var params = 'usr=' + uname + '&pas=' + pa;
    if ($('#lgnsm_rem').is(":checked")) {
        params += '&rem=1';
    }
    $.ajax({
        type: 'POST',
        url: handler,
        data: params,
        async: true,
        cache: true,
        success: function (msg) {
            switch (msg) {
                case "p400":
                    Display_Message("#lgnsm_msg", "Access Denied!.", 0, 1);
                    break;
                case "p100":
                    Display_Message("#lgnsm_msg", "Please enter username &amp; password!", 0, 1);
                    break;
                case "p101":
                    Display_Message("#lgnsm_msg", "Login failed, please try again!", 0, 1);
                    //document.location = lurl;
                    break;
                case "p102":
                    document.location = ipurl;
                    break;
                case "0":
                    document.location = redurl;
                    break;
            }
        }
    });
}

/* New Action Module */
   function ProcessLK(handler, params, action, actionid, action_box) {
         $(action_box).html("loading...");
         params = params + "&act=" + action;
         Process_Advice(handler, params, action_box, actionid, action);
         $(action_box).show();
    }
    function ActProcess(handler, params, action_box) {
        $(action_box).html("loading...");
        Process_Req(handler, params, action_box, 'GET');
        $(action_box).show();
    }
    function PlstPost(handler, params, action_box) {
        var value = $("#play_list").val();
        if (value == "") {
           Display_Message("#ply_msg", "Select playlist to add video!", 1, 1);
            return;
        }
        Ajax_Process(handler, params + "&val=" + value, action_box, "POST");
       $(action_box).show();
   }
   function FlagP(handler, params, action_box) {
        var value = $("#abuse_list").val();
        if (value == "") {
           Display_Message("#flg_msg", "Select reason for report!", 1, 1);
           return;
        }
       Ajax_Process(handler, params + "&val=" + value, action_box, "POST");
       $(action_box).show();
   }

   /* Comment App version 2.0 Js Script */
   function LoadMoreV2(obj, handler, params, tpages, pnumber, cmtloading, container, cmtmorelnk) {
       var tpages = $("#" + tpages).html();
       var cpnum = $("#" + pnumber).html();
       cpnum++;
       $("#" + cmtloading).html("loading...");
       var pval = params;
       $.ajax({
           type: 'GET',
           url: handler,
           data: pval + "&p=" + cpnum + "&tpages=" + tpages,
           async: true,
           cache: true,
           success: function (msg) {
               switch (msg) {
                   case "p400":
                       Display_Message("#" + cmtloading, "Access Denied!.", 1, 1);
                       break;
                   default:
                       $("#" + cmtloading).html('');
                       $("#" + container).append(msg);
                       if (cpnum >= tpages) {
                           $("#" + cmtmorelnk).hide();
                       }
                       $("#" + pnumber).html(cpnum);
               }
           }
       });
   }

   function LoadMore(obj, handler, params) {
       var tpages = $("#cmt_tpages").html();
       var cpnum = $("#cmt_pnum").html();
       cpnum++;
       $("#cmt_loading").html("loading...");
       var pval = params;
       $.ajax({
           type: 'GET',
           url: handler,
           data: pval + "&p=" + cpnum + "&tpages=" + tpages,
           async: true,
           cache: true,
           success: function (msg) {
               switch (msg) {
                   case "p400":
                       Display_Message("#cmt_loading", "Access Denied!.", 1, 1);
                       break;
                   default:
                       $("#cmt_loading").html('');
                       $("#cmt_load_cnt").append(msg);
                       if (cpnum >= tpages) {
                           $("#cmt_more").hide();
                       }
                       $("#cmt_pnum").html(cpnum);
               }
           }
       });
   }
   function PostFlag(obj, handler, params) {
       var id = $(obj).closest("div").attr("id");
       if (id != undefined) {
           var cid = null;
           var reg = /cmt_action_fb_(\d+)/;
           if (reg.test(id)) {
               cid = id.replace("cmt_action_fb_", "");
           } else {
               cid = id.replace("cmt_action_", "");
           }
           var pval = params;
           $.ajax({
               type: 'POST',
               url: handler,
               data: pval + "&cid=" + cid,
               async: true,
               cache: true,
               success: function (msg) {
                   switch (msg) {

                       case "p400":
                           Display_Message("#citem_" + cid, "Access Denied!.", 1, 1);
                           break;
                       case "p103":
                           Display_Message("#citem_" + cid, "Error occured while posting your comment!", 1, 1);
                           break;
                       case "p100":
                           Display_Message("#citem_" + cid, "Sign in to post flag report!", 1, 1);
                           break;
                       case "p101":
                           Display_Message("#citem_" + cid, "You can't post flag report on your own comment!.", 1, 1);
                           break;
                       case "p102":
                           Display_Message("#citem_" + cid, "You already post flag report on this comment!.", 1, 1);
                           break;
                       case "p103":
                           Display_Message("#citem_" + cid, "Flag report already posted via current ip address!.", 1, 1);
                           break;
                       default:
                           Display_Message("#citem_" + cid, "Flag report has been posted successfully!.", 0, 1);
                           $("#citem_" + cid).show();
                           break;
                   }
               }
           });
       }
   }

   function RemoveCmtV2(obj, handler, params, cmts) {
       var id = $(obj).closest("div").attr("id");
       if (id != undefined) {
           var cid = id.replace("cmt_action_", "");
           $("#citem_" + cid).hide();
           var pval = params;
           var tcmts = $('#' + cmts).html();
           $.ajax({
               type: 'POST',
               url: handler,
               data: pval + "&cid=" + cid + "&tcmt=" + tcmts,
               async: true,
               cache: true,
               success: function (msg) {
                   switch (msg) {
                       case "p400":
                           Display_Message("#citem_" + cid, "Access Denied!.", 1, 1);
                           break;
                       case "p100":
                           Display_Message("#citem_" + cid, "Error occured while posting your comment!", 1, 1);
                           break;
                       case "p101":
                           Display_Message("#citem_" + cid, "Error occured while posting your comment!", 1, 1);
                           break;
                       case "p102":
                           Display_Message("#citem_" + cid, "Sign In to remove comment.", 1, 1);
                           break;
                       default:
                           $("#citem_" + cid).hide();
                           $("#citem_" + cid).html('');
                           tcmts = tcmts - 1;
                           $('#' + cmts).html(tcmts);
                           break;
                   }
               }
           });
       }
   }

   function RemoveCmt(obj, handler, params) {
       var id = $(obj).closest("div").attr("id");
       if (id != undefined) {
           var cid = id.replace("cmt_action_", "");
           $("#citem_" + cid).hide();
           var pval = params;
           var tcmts = $('#cmt_tcmts').html();
           $.ajax({
               type: 'POST',
               url: handler,
               data: pval + "&cid=" + cid + "&tcmt=" + tcmts,
               async: true,
               cache: true,
               success: function (msg) {
                   switch (msg) {
                       case "p400":
                           Display_Message("#citem_" + cid, "Access Denied!.", 1, 1);
                           break;
                       case "p100":
                           Display_Message("#citem_" + cid, "Error occured while posting your comment!", 1, 1);
                           break;
                       case "p101":
                           Display_Message("#citem_" + cid, "Error occured while posting your comment!", 1, 1);
                           break;
                       case "p102":
                           Display_Message("#citem_" + cid, "Sign In to remove comment.", 1, 1);
                           break;
                       default:
                           $("#citem_" + cid).hide();
                           $("#citem_" + cid).html('');
                           tcmts = tcmts - 1;
                           $('#cmt_tcmts').html(tcmts);
                           break;
                   }
               }
           });
       }
   }
   function PostV2(obj, eid, handler, params, rid, msgid, container, actionbtn, cmts) {
       var value = $('#' + eid).val();
       var pval = params;
       if (value == '') {
           Display_Message("#" + msgid, "Please Write Something to Post.", 1, 200);
           return;
       }
       var tcmts = $("#" + cmts).html();
       Display_Message("#" + msgid, "Processing...", 0, 50);
       $.ajax({
           type: 'POST',
           url: handler,
           data: pval + "&val=" + encodeURIComponent(value) + "&tcmt=" + tcmts + "&rid=" + rid,
           async: true,
           cache: true,
           success: function (msg) {
               switch (msg) {
                   case "p400":
                       Display_Message("#" + msgid, "Access Denied!.", 1, 1);
                       break;
                   case "p100":
                       Display_Message("#" + msgid, "Error occured while posting your comment!", 1, 1);
                       break;
                   case "p101":
                       Display_Message("#" + msgid, "Sign In to post comment!", 1, 1);
                       break;
                   case "p102":
                       Display_Message("#" + msgid, "Please Write Something to Post.", 1, 1);
                       break;
                   case "p103":
                       Display_Message("#" + msgid, "Error occured while processing your comment, please fix it!", 1, 1);
                       break;
                   default:
                       $("#" + msgid).html('');
                       $("#" + container).prepend(msg);
                       $("#" + container).show();
                       $('#' + eid).val("");
                       if (rid == 0)
                           $("#" + actionbtn).hide();
                       tcmts++;
                       $('#' + cmts).html(tcmts);
                       break;
               }
           }
       });
   }

   function Post(obj,eid, handler, params, rid,msgid,container,actionbtn) {
        var value = $('#' + eid).val(); 
        var tcmts = $('#cmt_tcmts').html();
        var pval = params;
        if (value == '') {
            Display_Message("#" + msgid, "Please Write Something to Post.", 1, 200);
            return;
        }
        Display_Message("#" + msgid, "Processing...", 0, 50);
        $.ajax({
            type: 'POST',
            url: handler,
            data: pval + "&val=" + encodeURIComponent(value) + "&tcmt=" + tcmts + "&rid=" + rid,
            async: true,
            cache: true,
            success: function (msg) {
                switch (msg) {
                    case "p400":
                        Display_Message("#" + msgid, "Access Denied!.", 1, 1);
                        break;
                    case "p100":
                        Display_Message("#" + msgid, "Error occured while posting your comment!", 1, 1);
                        break;
                    case "p101":
                        Display_Message("#" + msgid, "Sign In to post comment!", 1, 1);
                        break;
                    case "p102":
                        Display_Message("#" + msgid, "Please Write Something to Post.", 1, 1);
                        break;
                    case "p103":
                        Display_Message("#" + msgid, "Error occured while processing your comment, please fix it!", 1, 1);
                        break;
                    default:
                        $("#" + msgid).html('');
                        $("#" + container).prepend(msg);
                        $("#" + container).show();
                        $('#' + eid).val("");
                        if (rid == 0)
                            $("#" + actionbtn).hide();
                        tcmts++;
                        $('#cmt_tcmts').html(tcmts);
                        break;
                }
            }
        });
    }
    
    function ReplyBox(obj,plholder) {
        var id = $(obj).closest("div").attr("id");
        if (id != undefined) {
            var cid = null;
            var reg = /cmt_action_fb_(\d+)/;
            if (reg.test(id)) {
                cid = id.replace("cmt_action_fb_", "");
            } else {
                cid = id.replace("cmt_action_", "");
            }
            ReplyBx("cmtrep_" + cid, cid, plholder);
            $("#cmtrep_" + cid).on({
                keyup: function (e) {
                    Count_Chars(this, '#rep_cnt_' + cid, 500);
                }
            }, '.reptxt');
        }
    }
    function CmtVote(obj, act, handler) {
        var id = $(obj).closest("div").attr("id");
        if (id != undefined) {
            var cid = null;
            var reg = /cmt_action_fb_(\d+)/;
            if (reg.test(id)) {
                cid = id.replace("cmt_action_fb_", "");
                var option = $("#cmt_lk_" + cid).html();
                if (option == "Like") {
                    act = 1;
                    $("#cmt_lk_" + cid).html('Unlike');
                }
                else {
                    act = 0;
                    $("#cmt_lk_" + cid).html('Like');
                }
            } else {
                cid = id.replace("cmt_action_", "");
            }
            var pnts = $("#cmtpts_" + cid).html();
            var ausr = $("#cmtusr_" + cid).html();
            $("#citem_" + cid).show();
            $.ajax({
                type: 'GET',
                url: handler,
                data: "ausr=" + ausr + "&pts=" + pnts + "&vt=" + act + "&id=" + cid,
                async: true,
                cache: true,
                success: function (msg) {
                    switch (msg) {
                        case "p400":
                            Display_Message('#cmsg_' + cid, "Access Denied!.", 1);
                            break;
                        case "p100":
                            Display_Message('#cmsg_' + cid, "Error occured while posting your comment!", 1);
                            break;
                        case "p101":
                            Display_Message('#cmsg_' + cid, "You can't vote your own comment!", 1);
                            break;
                        case "p102":
                            Display_Message('#cmsg_' + cid, "Sign In to post vote", 1);
                            break;
                        case "p103":
                            Display_Message('#cmsg_' + cid, "You already post vote on this comment!", 1);
                            break;
                        default:
                            $("#cmtpl_" + cid).html(msg);
                            break;
                    }
                }
            });

        }
    }

    function CmtItemMEnter(obj) {
        var id = $(obj).attr('id');
        if (id != undefined) {
            var rhandlerid = $(obj).attr('id').replace("citem", "cmt_action");
            $("#" + rhandlerid).show();
        }
    }

    function CmtItemMLeave(obj) {
        var id = $(obj).attr('id');
        if (id != undefined) {
            var rhandlerid = $(obj).attr('id').replace("citem", "cmt_action");
            $("#" + rhandlerid).hide();
        }
    }

    function ReplyBx(obj, id, plholder) {
        var strVar = "";
        strVar += "<div class=\"help-block row-fluid\">";
        strVar += "<textarea name=\"rep_name_" + id + "\" placeholder=\"" + plholder + "\" rows=\"2\" cols=\"20\" id=\"rep_id_" + id + "\" class=\"nm_txtbox span12 reptxt\" style=\"height:50px;\">";
        strVar += "<\/textarea>";
        strVar += "<\/div>";
        strVar += "<div id=\"repaction_" + id + "\" class=\"help-block row-fluid\">";
        strVar += "<div class=\"mini-text light\" style=\"float: left; width: 50%; padding: 4px 0px;\">";
        strVar += "<span id=\"rep_cnt_" + id + "\">500<\/span> characters remaining.";
        strVar += "<\/div>";
        strVar += "<div style=\"float: right; width: 8%;\">";
        strVar += "<a id=\"cmt_post_" + id + "\" href=\"#\" class=\"btn btn-small repcmt\">";
        strVar += "Post<\/a>";
        strVar += "<\/div>";
        strVar += "<div class=\"clear\">";
        strVar += "<\/div>";
        strVar += "<\/div>";
        $("#" + obj).html(strVar);
        $("#" + obj).show();
    }

    