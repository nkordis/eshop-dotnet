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
            { "data": "role", "width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    var lockUnlockButton = lockout > today
                        ? `<a class="btn btn-danger text-white lock-unlock-btn" data-id="${data.id}" style="cursor:pointer; width:100px;"><i class="bi bi-lock-fill"></i> Lock</a>`
                        : `<a class="btn btn-success text-white lock-unlock-btn" data-id="${data.id}" style="cursor:pointer; width:100px;"><i class="bi bi-unlock-fill"></i> UnLock</a>`;

                    return `<div class="text-center">
                                ${lockUnlockButton}
                                <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger text-white permission-btn" style="cursor:pointer; width:150px;"><i class="bi bi-pencil-square"></i> Permission</a>
                            </div>`;
                },
                "width": "25%"
            }
        ],
        "createdRow": function (row, data, dataIndex) {
            $(row).find('.lock-unlock-btn').on('click', function () {
                LockUnlock($(this).data('id'));
            });
        }
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}
