var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        }, "pageLength": 2, // default selected value
        "lengthMenu": [[2, 4, 6, 8], [2, 4, 6, 8]],
        "columns": [
            { "data": "name", "width": "70%" },
            { "data": "streetAddress" },
            { "data": "city" },
            { "data": "state" },
            { "data": "phoneNumber" },
            {
               
                data: "isAuthorizedCompany",
                render: function (data) {
                    return `<div class="text-center">
                    <input type="checkbox" ${data ? "checked" : ""} onclick="return false;" />
                </div>`;
                },
                width: "10%"
            },

            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">

                        <a href="/Admin/Company/Upsert/${data}"
                        class="btn btn-info">
                        <i class="fas fa-edit"></i>
                        </a>

                        <a class="btn btn-danger"
                        onclick="Delete('/Admin/Company/Delete/${data}')">
                        <i class="fas fa-trash-alt"></i>
                        </a>

                    </div>`;
                },
                "width": "30%"
            }
        ]
    });
}

function Delete(url) {

    swal({
        title: "Want to Delete data?",
        text: "Delete information !!!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {

        if (willDelete) {

            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {

                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }

                }
            });

        }

    });

}