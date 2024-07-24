$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/product/getall",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "30%" },
            { "data": "size", "width": "5%" },
            { "data": "listPrice", "width": "10%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return '<div class="w-75 btn-group" role="group">' +
                        '<a href="/admin/product/upsert/' + data + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>' +
                        '<a href="/admin/product/delete/' + data + '" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>' +
                        '</div>';
                },
                "width": "25%"
            }
        ]
    });
}
