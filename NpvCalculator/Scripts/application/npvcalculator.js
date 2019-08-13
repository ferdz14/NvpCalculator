$(document).ready(function () {
    const api = getBaseUrl() + "NpvCalculator/api/";
    const apiUrlCalculate = api + "Calculate";

    $("#btnAdd").on("click", function () {
        var val = parseInt($("#txtCashFlow").val() || 0);
        if (val > 0) {
            addCashFlowItem(val);
            $("#txtCashFlow").val("0");
        }
        else {
            showMessage("Error", "Invalid Cash Flow.");
        }
    })

    $("#btnCalculate").on("click", function () {
        var npvCanvas = $('#npvCanvas');
        npvCanvas.hide();

        if (isValidToCalculate()) {
            getCashFlows();

            $.ajax({
                url: apiUrlCalculate,
                type: "POST",
                data: $("form").serialize(),
                success: function (result) {
                    var npvChart = new Chart(npvCanvas, {
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
                    npvCanvas.show();
                }
            });
        }
    })

    $("#btnReset").on("click", function () {
        showMessage("Warning", "Are you sure you want to reset the NPV values?");
    })

    $("#btnYes").on("click", function () {
        window.location.reload();
    })

    function getBaseUrl() {
        var re = new RegExp(/^.*\//);
        return re.exec(window.location.href);
    }

    function showMessage(alertType, message) {
        var alertModal = $('#alertModal');

        if (alertType === "Error") {
            alertModal.find(".modal-header").removeClass('btn-warning').addClass('btn-danger');
            $('#btnOk').show();
            $('#btnYes').hide();
            $('#btnCancel').hide();
        }
        else if (alertType === "Warning") {
            alertModal.find(".modal-header").removeClass('btn-danger').addClass('btn-warning');
            $('#btnOk').hide();
            $('#btnYes').show();
            $('#btnCancel').show();
        }

        alertModal.find(".modal-title").text(alertType)
        alertModal.find(".modal-body").text(message)
        alertModal.modal('show');
    }

    function addCashFlowItem(rowValue) {
        var tbody = $("#tblCashFlows").find("tbody");
        var itemRow = `<tr><td class="col-md-9 text-center">${rowValue}</td><td class="col-md-3 text-center"><button type="button" class="btn btn-danger">Delete</button></td></tr>`;
        tbody.append(itemRow);

        var btn = tbody.find(".btn-danger")
        btn.bind("click", function () {
            $(this).closest("tr").remove();
        })
    }

    function isValidToCalculate() {
        var lowerBoundDiscountRate = parseFloat($("#LowerBoundDiscountRate").val() || 0);
        if (lowerBoundDiscountRate <= 0) {
            showMessage("Error", "Invalid Lower Bound Discount Rate.");
            return false;
        }

        var upperBoundDiscountRate = parseFloat($("#UpperBoundDiscountRate").val() || 0);
        if (upperBoundDiscountRate <= 0) {
            showMessage("Error", "Invalid Upper Bound Discount Rate.");
            return false;
        }

        var incrementRate = parseFloat($("#DiscountRateIncrement").val() || 0);
        if (incrementRate <= 0) {
            showMessage("Error", "Invalid Discount Rate Increment.");
            return false;
        }

        var initialInvestment = parseFloat($("#InitialInvestment").val() || 0);
        if (initialInvestment <= 0) {
            showMessage("Error", "Invalid Initial Investment.");
            return false;
        }

        var trCashFlows = $("#tblCashFlows").find("tbody>tr");
        if (trCashFlows.length === 0) {
            showMessage("Error", "Invalid Cash Flows.");
            return false;
        }

        return true;
    }

    function getCashFlows() {
        var cashFlows = [];
        var tableRows = $("#tblCashFlows").find("tbody>tr");

        tableRows.each(function (i, item) {
            var value = $(item).children("td:first").text();
            cashFlows.push(parseInt(value));
        })

        $("#CommaDelimetedCashFlows").val(cashFlows.join(","));
    }
})