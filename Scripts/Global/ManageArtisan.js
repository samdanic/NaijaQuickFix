$('.btn-unpublic-product').on("click", function (e) {
    e.preventDefault();
    var action = "unpublic";
    var id = $(this).attr("data-id");
    var api_url = "/Dashboard/UnPublicArtisan";

    AskAndAction(action, id, api_url);
});

$('.btn-public-product').on("click", function (e) {
    e.preventDefault();
    var action = "public";
    var id = $(this).attr("data-id");
    var api_url = "/Dashboard/PublicArtisan";

    AskAndAction(action, id, api_url);
});
////$('.btn-delete-product').on("click", function (e) {
////    e.preventDefault();
////    var action = "delete";
////    var id = $(this).attr("data-id");
////    var api_url = "/Admin/DeleteProduct";

//    AskAndActionCallback(action, id, api_url);
//});





//General
String.prototype.removeMVCPath = function (n) {
    var str = this;
    if (str.indexOf("~/") == 0) {
        str = str.substr(1);
    }

    return str;
};

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = c == undefined ? 0 : (isNaN(c = Math.abs(c)) ? 2 : c),
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

function isScrolledIntoView(elem) {
    var docViewTop = $(window).scrollTop();
    var docViewBottom = docViewTop + $(window).height();

    var elemTop = $(elem).offset().top;
    var elemBottom = elemTop + $(elem).height();

    return ((elemBottom >= docViewTop) && (elemTop <= docViewBottom)
        && (elemBottom <= docViewBottom) && (elemTop >= docViewTop));
}

String.prototype.replaceAll = function (find, replace) {
    var str = this;
    return str.replace(new RegExp(find.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&'), 'g'), replace);
};

// Check định dạng Email
function xfomatEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\ ".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function CheckAuthentication() {
    var isAuthenticated = $("#isAuthenticated").val();
    if (isAuthenticated == 0 || isAuthenticated == false || isAuthenticated == "false") {
        return false;
    }
    return true;
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//Action Form dùng cho các trang quản lý

function ActionData(data, api_url, redirect_url) {
    redirect_url = typeof redirect_url !== 'undefined' ? redirect_url : "";
    if (data && api_url) {
        $.ajax({
            type: "POST",
            url: api_url,
            content: "application/json;charset=utf-8",
            dataType: "json",
            data: data,
            success: function (d) {
                if (d.success == true) {
                    if (d.message) {
                        $(".alertvalidate").css("display", "none");
                        //  alert(d.message);
                    }
                    if (redirect_url) {
                        window.location = redirect_url;
                    }
                    else {
                        window.location.reload();
                    }
                }
                else {
                    if (d.message) {
                        // alert(d.message);
                        $(".alertvalidate").css("display", "block");
                        $(".alertvalidate").html(d.message);
                    }
                    else {
                        alert("Do not send the requested!");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
                $(".alertvalidate").css("display", "block");
                $(".alertvalidate").html('An error occurred, please redo. Thank you!');
                //alert('Có lỗi xảy ra, vui lòng thực hiện lại. Xin cảm ơn!');
            }
        });
    }
}

function Action(id, api_url, redirect_url) {
    ActionData({ id: id }, api_url, redirect_url);
}

function ActionForm(form_data, api_url, redirect_url) {
    ActionData(form_data, api_url, redirect_url);
}

function ActionCallback(id, api_url, callback_func) {
    if (id && api_url) {
        $.ajax({
            type: "POST",
            url: api_url,
            content: "application/json;charset=utf-8",
            dataType: "json",
            data: {
                id: id
            },
            success: function (d) {
                if (d.success == true) {
                    if (typeof callback_func == 'function') {
                        callback_func();
                    }
                    window.location.reload();
                }
                else {
                    if (d.message) {
                        alert(d.message);
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
                alert('An error occurred, please redo. Thank you!');
            }
        });
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//AskAndAction

function AskAndAction(action, id, api_url, redirect_url) {
    BootstrapDialog.show({
        cssClass: 'delete-car-dialog',
        title: 'Warning',
        message: 'Are you sure you want to be ' + action + ' this information is not?',
        buttons: [{
            label: 'No',
            cssClass: 'btn-default',
            action: function (dialog) {
                dialog.close();
            }
        },
        {
            label: 'Yes',
            cssClass: 'btn-danger',
            action: function (dialog) {
                Action(id, api_url, redirect_url);
                dialog.close();
            }
        }]
    }).setType(BootstrapDialog.TYPE_DANGER);
}

function AskAndActionData(action, data, api_url, redirect_url) {
    BootstrapDialog.show({
        cssClass: 'delete-car-dialog',
        title: 'Warning',
        message: 'Are you sure you want to be ' + action + ' this information is not?',
        buttons: [{
            label: 'No',
            cssClass: 'btn-default',
            action: function (dialog) {
                dialog.close();
            }
        },
        {
            label: 'Yes',
            cssClass: 'btn-danger',
            action: function (dialog) {
                ActionData(data, api_url, redirect_url);
                dialog.close();
            }
        }]
    }).setType(BootstrapDialog.TYPE_DANGER);
}

function AskAndActionCallback(action, id, api_url, callback_func) {
    BootstrapDialog.show({
        cssClass: 'delete-car-dialog',
        title: 'Warning',
        message: 'Are you sure you want to be ' + action + ' this information is not?',
        buttons: [{
            label: 'No',
            cssClass: 'btn-default',
            action: function (dialog) {
                dialog.close();
            }
        },
        {
            label: 'Yes',
            cssClass: 'btn-danger',
            action: function (dialog) {
                ActionCallback(id, api_url, callback_func);
                dialog.close();
            }
        }],
    }).setType(BootstrapDialog.TYPE_DANGER);
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//waitingLoadingDialog

var watingLoadingDialog = null;
function showWaitingDialog(message) {
    if (!watingLoadingDialog) {
        watingLoadingDialog = BootstrapDialog.show({
            title: 'Loading',
            message: message
        }).setType(BootstrapDialog.TYPE_PRIMARY);
    }
}

function hideWaitingDialog() {
    if (watingLoadingDialog) {
        watingLoadingDialog.close();
        watingLoadingDialog = null;
    }
}

function ShowBootstrapDialog(title, message, type, isReload) {
    isReload = typeof isReload !== 'undefined' ? isReload : false;
    BootstrapDialog.show({
        title: title,
        message: message,
        buttons: [{
            label: 'Close',
            action: function (dialogItself) {
                dialogItself.close();
            }
        }]
    }).setType(type);

    if (isReload) {
        window.location.reload();
    }
}

function ShowBootstrapDialog_Success(title, message, isReload) {
    isReload = typeof isReload !== 'undefined' ? isReload : false;
    ShowBootstrapDialog(title, message, BootstrapDialog.TYPE_SUCCESS, isReload);
}

function ShowBootstrapDialog_Failed(title, message, isReload) {
    isReload = typeof isReload !== 'undefined' ? isReload : false;
    ShowBootstrapDialog(title, message, BootstrapDialog.TYPE_WARNING, isReload);
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//show city name in url

function ShowCityNameUrl() {

}