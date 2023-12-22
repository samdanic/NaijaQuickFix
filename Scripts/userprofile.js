const header = $('.user_nav')
$(document).ready(async () => {
    window.onscroll = function () { stckyHeader() };
    var header = document.getElementById("user_nav");
    var sticky = header.offsetTop;
    function stckyHeader() {
        if (window.pageYOffset > sticky) {
            header.classList.add("sticky");
        } else {
            header.classList.remove("sticky");
        }
    }
    $('.harmburger').on('click', () => {
        $(".side-nav").toggleClass("active");
        $(".harmburger1").toggleClass("smooth");
        $(".harmburger2").toggleClass("smooth");
        $(".harmburger3").toggleClass("smooth");
    })
    $('#edit').on('click', async (event) => {
        event.preventDefault();
        $('#firstname').removeAttr("readonly");
        $('#lastname').removeAttr("readonly");
        $('#email').removeAttr("readonly");
        $('#mobile').removeAttr("readonly");
        $('#password').removeAttr("readonly");
        $('#ConfirmPassword').removeAttr("readonly");
        
    })
    $("#edit").click(function () {
        $('html,body').animate({
            scrollTop: $(".edit-view").offset().top
        },
            'slow');
        $('#edit').css("display", "none");
        $(".update-admin").css("display", "block");
    })

    $(".update-admin").click(function () {
        $('html,body').animate({
            scrollTop: $("#edit").offset().top
        },
            'slow');
        $('#edit').css("display", "block");
        $(".update-admin").css("display", "none");
    })

})