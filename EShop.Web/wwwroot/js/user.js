var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/user/getall",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "25%" },
            { "data": "company.name", "width": "10%" },
            { "data": "", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return '<div class="w-75 btn-group" role="group">' +
                        '<a href="/admin/company/upsert/' + data + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>' +
                        '</div>';
                },
                "width": "25%"
            }
        ]
    });
}
