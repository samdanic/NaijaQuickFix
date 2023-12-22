const genders = [
    {
        id: "1",
        name: "Female"
    },
    {
        id: "2",
        name: "Male"
    },
]
const stateUrl = "https://nigerian-states-info.herokuapp.com/api/v1/states";



$(document).ready(async () => {
    await loadState()
})

async function loadState() {
    const data = await fetch(stateUrl)
    const response = await data.json();
    if (response && response.data.length > 0) {
        response.data.map(item => {
            console.log('Call Arrtisnas')
            $("#artisan-state").append(`<option value=${item.info.officialName}>${item.info.officialName}</option>`);
        })
    }
    else {
        $("#artisan-state").append(`<option value="empty">No data available</option>`);
    }
}