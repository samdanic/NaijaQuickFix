$(document).ready(async () => {
    $('.navContainer').on('click', () => {
        $(".navContainer").toggleClass("active-nav");
        $(".harmburger").toggleClass("smooth");
        $(".harmburger1").toggleClass("off");
        $(".harmburger2").toggleClass("close1");
        $(".menu-container").toggleClass("active");
    })
})

$(document).ready(async () => {
    setTimeout(() => {
        $('.overlay').fadeOut('slow')
    }, 5000)
})