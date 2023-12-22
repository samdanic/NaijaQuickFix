const quickFixInput = [
    {
        name: 'Total Users',
        value: 0
    },
    {
        name: 'Newly Added',
        value: 0
    },
    {
        name: 'Last Month',
        value: 0
    },
];
const displayChart = (total, newly, month, id, type) => {
    const labels = [
        'Total Users',
        'Newly Added',
        'Last Month'
    ];

    const data = {
        labels: labels,
        datasets: [{
            label: 'Number Of Users',
            backgroundColor: ['#33A175', '#f5f5f5', '#64A0E6'],
            borderColor: ['#33A175', '#f5f5f5', '#64A0E6'],
            borderWidth: type === 'line' ? 1:0,
            data: [total, newly, month],
            hoverOffset: 3
        }]
    };

const config = {
    type: type ? type : 'doughnut',
    data: data,
    options: {},
    };
    const myChart = new Chart(
        $(`#${id}`),
        config
    );
}