﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/getall",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "30%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "20%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return '<div class="w-75 btn-group" role="group">' +
                        '<a href="/admin/order/details?orderId=' + data + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>' +
                        '</div>';
                },
                "width": "25%"
            }
        ]
    });
}
