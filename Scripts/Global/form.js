//Upload Image

var file_images = [];
var file_images_id = 0;

function removeImageFromList(image_id) {
    var index = -1;
    for (var i = 0; i < file_images.length; i++) {
        if (file_images[i].id == image_id)
            index = i;
    }

    if (index != -1) {
        file_images.splice(index, 1);

        return true;
    }

    return false;
}

$("body").on("change", ".form-upload-images input", function (event) {
    //0. get model name
    var name = $(this).attr("name");
    var multiple = $(this).attr("multiple");

    //1. load file and generate html code
    var fileList = event.target.files;
    var anyWindow = window.URL || window.webkitURL;
    var append_text = "";

    for (var i = 0; i < fileList.length; i++) {
        //get a blob to play with
        var objectUrl = anyWindow.createObjectURL(fileList[i]);

        append_text += '<div class="col-xs-3 has-bottom">'
            + '<div class="image-ratio">'
            + '<img class="modal-image img-responsive" src="' + objectUrl + '" />'
            + '<button class="overlay-btn delete-photo-btn" data-image=' + file_images_id + '>'
            + '<i class="fa fa-trash-o"></i>'
            + '</button>'
            + '</div>'
            + '</div>';

        // get rid of the blob
        window.URL.revokeObjectURL(fileList[i]);

        if (!multiple) {
            var image_id = $(this).attr("data-image");
            removeImageFromList(image_id);

            $(this).attr("data-image", file_images_id);
        }

        file_images.push({
            id: file_images_id,
            value: fileList[i],
            name: name
        });

        file_images_id++;
    }

    //2. add html codes to parent div
    if (multiple) {
        $(this).parents(".form-upload-images").find('.form-photos').append(append_text);
    }
    else {
        $(this).parents(".form-upload-images").find('.form-photos').html(append_text);
    }
    $(this).val("");

    console.log(file_images);

});

$("body").on("click", ".form-upload-images .form-photos .delete-photo-btn", function (event) {
    event.preventDefault();
    if (confirm("Do you want to delete this picture?")) {
        var image_id = $(this).attr("data-image");//image new

        if (image_id) {
            if (removeImageFromList(image_id)) {
                $(this).parent().parent().remove();//remove col-xs-3
            }
            else {
                alert("An error occurred");
            }
        }
        else {
            image_id = $(this).attr("data-id");//image old
            if (image_id) {
                $(this).parent().parent().remove();//remove col-xs-3
            }
        }
    }
});

$("body").on("click", ".form-upload-images .form-photos .modal-image", function (event) {
    var img = $(this);

    BootstrapDialog.show({
        title: 'Hình ảnh',
        cssClass: 'image-preview-dialog',
        closeByBackdrop: true,
        closable: true,
        message: function (dialog) {
            var $content = $(
                '<img src="' + img.attr("src") + '" style="width: 100%;" />');

            $('body').on('click', '.image-preview-dialog .modal-backdrop', function () {
                dialog.close();
            })

            return $content;
        },
    }).setType(BootstrapDialog.TYPE_INFO);
});

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//Form List

function SetFormListItemName() {
    $(".form-list").each(function () {
        var data_model = $(this).attr("data-model");
        $(this).find(".form-list-item").each(function (index) {
            //set index
            $(this).find(".form-item-index").html(index + 1);
            //set name
            $(this).find(".form-control").each(function () {
                var data_name = $(this).attr("data-name");
                if (!data_name) {
                    data_name = $(this).attr("name");
                    $(this).attr("data-name", data_name);
                }
                $(this).attr("name", data_model + "[" + index + "]." + data_name);
            });
        });
    });

}

SetFormListItemName();

$(".btn-add-item").on("click", function () {
    var form_list = $(this).parents(".form-list");
    var first_item = form_list.find(".form-list-item").last();
    if (first_item.length > 0) {
        //clear item input contents
        var html = first_item[0].outerHTML;
        $(html).find(".form-control").val("");
        //$(html).find(".form-upload-images .form-photos").html("");
        html = $(html).find(".form-upload-images .form-photos").html("").prevObject[0].outerHTML;
        //add to parent
        form_list.find(".form-list-item").last().after($(html));
        SetFormListItemName();
        form_list.find(".form-list-count").html(form_list.find(".form-list-item").length);
    }
});

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//Post to server
$("body").on("click", "form #btnAdd", function (event) {
    event.preventDefault();

    uploadDataToServer(this.form);
})

function uploadDataToServer(form) {
    if ($(form).attr("adding") == "true") {
        return;
    }
    $(form).attr("adding", "true");

    //var postAddLink = $("#postAddLink").val();
    var postAddLink = $(form).attr("action");

    //check validation
    if (typeof checkDataValidation == 'function') {
        if (checkDataValidation(form) == false)
            return;
    }

    var isUploadImage = false;
    isUploadImage = $(form).find(".form-upload-images").length > 0;

    //callback_func
    var callback_func = null;
    if (typeof postFormCompleted == 'function')
        callback_func = postFormCompleted;
    else {
        var callback_name = $(form).attr("callback");
        if (callback_name) {
            callback_func = window[callback_name];
        }
    }

    //post to server
    if (isUploadImage) {
        //Nếu có tồn tại Image thì post Image cùng với Form Data đến postAddLink và return (không ActionForm nữa)
        SendRequestToServer(form, postAddLink, callback_func);
    }
    else {
        ActionForm($(form).serialize(), postAddLink, null, callback_func);
    }
}

function SendRequestToServer(form, url, callback_func) {
    function uploadProgress(evt) {
        if (evt.lengthComputable) {
            var progress = Math.round(evt.loaded * 100 / evt.total);
            $(form).find(".form-progress-bar #uploadProgress").css("width", progress + '%');
        }
    }

    function uploadComplete(evt) {
        $(form).attr("adding", "false");
        $(form).find(".form-progress-bar #uploadProgress").css("width", '100%');

        console.log("Upload photos completed!");

        var d = jQuery.parseJSON(evt.srcElement.response);
        if (d.success == false) {//failed
            if (d.message) {
                //alert(d.message);
                $(".alertsuccess").css("display", "none");
                $(".alerterror").css("display", "block");
                $(".alerterror").html(d.message);
            }
            else {
                alert("Do not send the requested!");
            }
        }
        else {//success
            if (d.message) {
                // alert(d.message);
                $(".alerterror").css("display", "none");
                $(".alertsuccess").css("display", "block");
                $(".alertsuccess").html(d.message);
            }
            if (d.reload) {
                window.location.reload();
            }
            else {
                if (callback_func)
                    callback_func();
            }
        }
    }

    function uploadFailed(evt) {
        alert("Error uploading picture! Please redo");

        console.log("There was an error attempting to upload the file.");
    }

    var fd = new FormData();
    var text_fields = $(form).serializeArray();

    for (var i in file_images) {
        fd.append(file_images[i].name, file_images[i].value);
    }
    for (var i in text_fields) {
        fd.append(text_fields[i].name, text_fields[i].value);
    }
    var multiple = $(".form-upload-images");
    //alert(multiple.length);
    for (var j = 0; j < multiple.length; j++) {
        // alert(multiple.eq(j).find("input").eq(0).attr("multiple"));
        if (multiple.eq(j).find("input").eq(0).attr("multiple") === "multiple") {
            var imgcurrent = multiple.eq(j).find('.form-photos').eq(0).find('.imgCurrent');
            for (var i = 0; i < imgcurrent.length; i++) {
                var name = imgcurrent.eq(i).attr("data-name");
                console.log(name);
                fd.append(name, imgcurrent.eq(i).attr('data-id'));
                //alert($('.directory-photos').find('.imgCurrent').eq(i).attr('data-id'));
            }
        }
    }
    //var imgcurrent = $('.form-photos').find('.imgCurrent');
    //for (var i = 0; i < imgcurrent.length; i++) {
    //    var n = $('.form-photos').find('.imgCurrent').eq(i).attr('data-name');
    //    alert(n);
    //    fd.append("pastFile", $('.form-photos').find('.imgCurrent').eq(i).attr('data-id'));
    //    //alert($('.directory-photos').find('.imgCurrent').eq(i).attr('data-id'));
    //}
    // fd.append("file", $("#file")[0].files[0]);
    // fd.append("locationfile", $("#locationfile")[0].files[0]);

    var xhr = new XMLHttpRequest();
    xhr.upload.addEventListener("progress", uploadProgress, false);
    xhr.addEventListener("load", uploadComplete, false);
    xhr.addEventListener("error", uploadFailed, false);

    xhr.open("POST", url);
    xhr.send(fd);

    //add form progress bar
    var upload_progress_content = "<div class='form-progress-bar'>"
        + "<div class='form-group upload-progress-container'>"
        + "<div style='width: 100%; margin-top: 20px;'>"
        + "<div class='progress progress-bar-success active'>"
        + "<div id='uploadProgress' class='progress-bar' role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' style='width: 0%'>"
        + "</div>"
        + "</div>"
        + "</div>"
        + "</div>"
        + "</div>";
    var upload_progress_container = $(form).attr("progress-bar-container");
    if (upload_progress_container) {
        $(upload_progress_container).html(upload_progress_content);
    }
    else {
        upload_progress_container = $(form).find(".form-progress-bar");
        if (upload_progress_container.length > 0)
            upload_progress_container.remove();
        $(form).append(upload_progress_content);
    }


}
