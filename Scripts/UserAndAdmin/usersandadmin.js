var isProcessing = false;

$('#btn-register').click(async (event) => await registerUser(event))
//$("body").on("click", ".btn-register", async (event) =>  await registerUser(event) )

async function registerUser(event) {
        event.preventDefault();
        console.log('Calling');
    try {
        console.log("hey");
        if (isProcessing) {
            return;
        }
        isProcessing = true
        /*    var captchaObj = $("#CaptchaCode").get(0).Captcha;*/

        // gather data required for Captcha validation
        var params = {}
        //params.CaptchaId = captchaObj.Id;
        //params.InstanceId = captchaObj.InstanceId;
        /*params.UserInput = $(".CaptchaCode").val();*/
        params.FirstName = $("#firstname").val();
        params.LastName = $("#lastname").val(); ``
        params.ConfirmPassword = $("#cfpassword").val();
        params.Password = $("#password").val();
        params.Nin = $("#nin").val();
        params.Email = $("#email").val();
        params.PhoneNo = $("#phoneno").val();
        /* params.state = $("#user_state").val();*/
        params.LGA = $("#lga").val();
        console.log(params);
        $.ajax({
            type: "POST",
            url: "/Register/Index",
            content: "application/json;charset=utf-8",
            dataType: "json",
            //  data: $(this.form).serialize(),
            data: params,
            success: function (d) {
                if (d.success) {
                    // $('.userinfo').html(d.content);
                    //$('#mylogin').modal('toggle');
                    $(".alertregister").css("display", "none")
                    //alert(d.message)
                    window.location.href = "../dashboard/index"
                    console.log("registered");
                }
                else {
                    $(".alertregister").css("display", "block")
                    $(".alertregister").html(d.message)
                    console.log("not registered")
                    /* captchaObj.ReloadImage();*/
                }
                isProcessing = false;
            }
        });
    }
    catch (error) {
alert(error.message)
    }
}