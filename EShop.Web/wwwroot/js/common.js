﻿function confirmDelete(url, onSuccess) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (typeof onSuccess === 'function') {
                        onSuccess(data);
                    }
                    toastr.success(data.message);
                },
                error: function (xhr, status, error) {
                    toastr.error("An error occurred: " + error);
                }
            });
        }
    });
}
