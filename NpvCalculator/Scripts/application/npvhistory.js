$(document).ready(function () {
    const apiGetHistoryChart = getBaseUrl() + "api/GetNpvHistoryByID"
    const pagingUrl = getBaseUrl() + "History/GridPager";
    $('.grid-mvc').gridmvc().ajaxify({
        getData: pagingUrl,
        getPagedData: pagingUrl
    });

    $('button[id^="btnViewChart_"]').bind("click", function () {
        var id = $(this).data('id');
        var npvHistoryCanvas = $('#npvHistoryCanvas');
        var chartModal = $("#chartView").show();

        $.ajax({
            url: apiGetHistoryChart,
            data: { id },
            success: function (result) {
                var npvChart = new Chart(npvHistoryCanvas, {
                    type: 'line',
                    data: {
                        labels: result.Labels,
                        datasets: [
                            {
                                backgroundColor: 'blue',
                                borderColor: 'blue',
                                data: result.Values
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Net Present Value'
                        }
                    }
                });

                npvChart.render();
                npvHistoryCanvas.show();
                chartModal.modal('show');
            }
        });
    });

    function getBaseUrl() {
        var re = new RegExp(/^.*\//);
        return re.exec(window.location.href);
    }
})