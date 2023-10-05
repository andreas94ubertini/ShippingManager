$(document).ready(function () {


    $("#groupByDest").click(function () {
        $("#list").empty();
        $("#listTitle").empty();
        $("#listTitle").append("Spedizioni raggruppate per destinazione")
        $.ajax({
            method: 'GET',
            url: "GetListOrderedByDest",
            success: function (list) {
                $.each(list, function (i, v) {
                    var li = "<li> Totale verso " + v.Destination + ": " + v.totaleVerso + "</li>";
                    $("#list").append(li);
                })
            }
        })
    })
    $("#TotalOnDlivery").click(function () {
        $("#list").empty();
        $.ajax({
            method: 'GET',
            url: "GetOnDelivery",
            success: function (list) {
                let count = 0
                $.each(list, function (i, v) {
                    count++ 
                })
                $("#listTitle").empty();
                $("#listTitle").append("Totale delle spedizioni in consegna oggi: " + count)
            }
        })
    })
    $("#NotDelivered").click(function () {
        $("#list").empty();
        $.ajax({
            method: 'GET',
            url: "GetNotDelivery",
            success: function (list) {
                let count = 0
                $.each(list, function (i, v) {
                    count++
                })
                $("#listTitle").empty();
                $("#listTitle").append("Totale delle spedizioni non consegnate: " + count)
            }
        })
    })


})