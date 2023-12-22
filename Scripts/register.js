let localG = []
const stateUrl = "../Data/state.json";
const lgaUrl = "../Data/lga.json";


$(document).ready(async () => {
    await loadState();
})


async function loadState() {
    const data = await fetch(stateUrl)
    let localGovernment = await fetch(lgaUrl)
    const response = await data.json();
    localGovernment = await localGovernment.json();
    localG = localGovernment.lgaList
    if (response && response.states.length > 0) {
        response.states.map(item => {
            $("#user_state").append(`<option value=${item.abrv} name=${item.abrv}>${item.name}</option>`);
        })
    }
    else {
        $("#user_state").append(`<option value="empty">No data available</option>`);
    }
}

$('#user_state').on('change', function () {
    try {
        $("#lga option").remove();
        console.log('Local Government', [this.value])
        localG[this.value].map(item => {
            $('#lga').append(`
        <option name=${item}>${item}</option>
        `)
        })
    } catch (error) {
        $(".alert-warning").text(error.message);
        $(".messages").css('display', 'flex');
        $(".warning").css('background-color', 'hsl(45, 100%, 51%)');
    }
});

function focusText(event) {
    setTimeout(() => {
        if (event.value === '') {
            $(`.${event.id}`).text(`${event.placeholder} is empty`)
        }
        else {
            $(`.${event.id}`).text('')
        }
    }, 1);
}

function checkNumberInput(event) {
    setTimeout(function () {
        if (event.value.length < 11) {
            $(`.${event.id}`).text(`${event.placeholder} should be 11 or more`)
        }
        else if (event.value.length === 11) {
            $(`.${event.id}`).text('')
        }
    }, 1)
}

function checkTextInput(event) {
    setTimeout(() => {
        if (event.value === '') {
            $(`.${event.id}`).text(`Please enter ${event.placeholder}`)
        }
        else if (event.type === 'password' && event.value.length < 8 && event.placeholder === 'Password') {
            console.log('Checking password', $('#confirmpassword').val().length)
            $(`.${event.id}`).text(`Password should be 8 characters or more`)
        }
        else if (event.type === 'password' && event.placeholder === 'Confirm Password' && $('#password').val() !== $('#confirmpassword').val()) {
        /*    if ($('#password').val().length !== $('#confirmpassword').val().length) {*/
                $(`.confirmpassword`).text(`Password do not match`)
            //}
        }
        else {
            $(`.${event.id}`).text('')
        }
    }, 1);
}
async function registerUser() {
    try {

    }
    catch (error) {

    }
}