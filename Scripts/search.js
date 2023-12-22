const searchResult = [
    {
        id: "1",
        name: "Artisan Name",
        des: "Welder",
    },
    {
        id: "2",
        name: "Artisan Name",
        des: "Welder",
    },
    {
        id: "3",
        name: "Artisan Name",
        des: "Welder",
    },
    {
        id: "4",
        name: "Artisan Name",
        des: "Welder",
    },
];

const output = $('#search-result');
$(document).ready(async () => {
    searchResult.map(item => {
        //console.log(item.name)
        //output.append()
        return (
            output.append(`<div class="step search-container" style="background-color: white; padding: 0px;">
                <div class="work-image">
                    <img src="./Images/b2.jpg" class="work-description-image" alt="work-description-image" />
                </div>
                <div class="artisan-name">
                    <img src="./Images/default.png" class="prev" alt="prev" />
                    <div class="full-details">
                        <span>${item.name}</span>
                        <span style="font-weight: 400; color:magenta var(--muted)">${item.name}</span>
                    </div>
                </div>
                <h2 class="job-description">I can help you plait your hair</h2>
                <div class="price-details">
                    <h6>STARTING AT</h6>
                    <h5>&nbsp;&#8358;@String.Format("{0:n}", 1000)</h5>
                </div>
            </div>`)
        )
    })

})
