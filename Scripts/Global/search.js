$(".btn-search").on("click", function (e) {
    e.preventDefault();
    $('.overlay').fadeIn('slow')
    $.ajax({
        type: "POST",
        url: "/Home/Search",
        content: "application/json;charset=utf-8",
        dataType: "json",
        data: $(this.form).serialize(),
        success: function (d) {
            if (d.success) {
                setTimeout(() => {
                    $('.overlay').fadeOut('slow')
                }, 3000)
                setTimeout(() => {
                    $('.home-view').fadeOut('slow');
                    $('.categories').fadeOut('slow');
                    $('.information').fadeOut('slow');
                    $('#sym-result').html(d.content);
                }, 3100)
            }
            else {
                $('#myError').modal('toggle');
                $('.data-found').text('Unable to process request');
            }
        }
    });
})