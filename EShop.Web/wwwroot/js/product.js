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
            { "data": "listPrice", "width": "5%" },
            { "data": "category.name", "width": "10%" }
        ]
    });
}
