$(document).ready(async () => {
    setTimeout(() => {
        $('.overlay').fadeOut('slow')
    }, 3000)
})
const formInput = document.getElementsByClassName('inp');
/*const password = document.getElementById('password');*/

$(document).ready(async () => {
    //$('#user-table').DataTable();
    $('#dashboard-icon').on('click', () => {
        console.log('Clicked')
        $(".side-nav").toggleClass("active");
                $(".main-view").toggleClass("shift");
        $(".harmburger1").toggleClass("smooth");
        $(".harmburger2").toggleClass("smooth");
        $(".harmburger3").toggleClass("smooth");
    })
    $('#edit').on('click', async (event) => {
        event.preventDefault();
        $('#admin_firstname').removeAttr("readonly");
        $('#admin_lastname').removeAttr("readonly");
        $('#admin_mobile').removeAttr("readonly");
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

$(document).ready(async () => {
    $('.register-artisans').on('click', async (event) => addNewArtisan(event))
    console.log('Clicked')
})

$(document).ready(async () => {
    var link = document.location.href.split('/');

    const navigations = document.querySelectorAll('.side-nav-item')
    navigations.forEach((navigation) => {
        navigation.addEventListener('click', () => {
            console.log('Navigation', navigation)
        });
    });
    //console.log('Link', list)
    link.forEach(item => {
        console.log('NAvigation', item)
    })
      console.log('Page holder');
})

$('.side-nav-item').on('click', function () {
    try {
        console.log('NAvigation', [$('.side-nav-item a')])
        console.log('Page holder');
        //this.toggleclass('active-page')
    } catch (error) {
        $(".alert-warning").text(error.message);
        $(".messages").css('display', 'flex');
        $(".warning").css('background-color', 'hsl(45, 100%, 51%)');
    }
});

//$(document).ready(async () => {
//    $('.register-artisans').on('click', async (event) => addNewArtisan(event))
//    console.log('Clicked')
//})

async function validateInput() {
    let valid = false
    //const usernameError = {};
    //const passwordError = {};
    //if (username.value === '') {
    //    usernameError.empty = 'Username / Email is empty'
    //    valid = false
    //}
    //if (password.value === '') {
    //    passwordError.empty = 'Password is empty';
    //    valid = false
    //}
    //return {
    //    valid,
    //    usernameError,
    //    passwordError
    //}
}

async function addNewArtisan(event) {
    event.preventDefault();
    const { valid, passwordError, usernameError } = await validateInput();
    const value = await validateInput();
    console.log('Validating', valid, value);
    if (!valid) {
        alert('Fields are empty');
    }
    else {
        alert('Login Successful')
    }
}