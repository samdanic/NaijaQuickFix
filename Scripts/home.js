$(document).ready(function () {
    $('.next').on('click', () => {
        let currentImg = $('.active');
        let firstImg = $('.first');
        let nextImg = currentImg.next();
        if (nextImg.length) {
            currentImg.removeClass('active').css('z-index', -100);
            nextImg.addClass('active').css('z-index', 100)
        }
        else if (nextImg.length === 0) {
            currentImg.removeClass('active').css('z-index', -100);
            firstImg.addClass('active').css('z-index', 100)
            console.log('nextImg.length', typeof nextImg.length)
        }
    })
    $('.prev').on('click', () => {
        let currentImg = $('.active');
        let prevImg = currentImg.prev();
        if (prevImg.length) {
            currentImg.removeClass('active').css('z-index', -100);
            prevImg.addClass('active').css('z-index', 100)
        }
    })
    $("#register").click(function () {
        $('html,body').animate({
            scrollTop: $(".information").offset().top
        },
            'slow');
    })
})